﻿using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Text;
using BitsKit.Generator.Models;

namespace BitsKit.Generator;

internal sealed class TypeSymbolProcessor
{
    public INamedTypeSymbol TypeSymbol => _typeSymbol;
    public TypeDeclarationSyntax TypeDeclaration => _typeDeclaration;
    public IReadOnlyList<BitFieldModel> Fields => _fields;
    public BaseNamespaceDeclarationSyntax? Namespace => _typeDeclaration.Parent as BaseNamespaceDeclarationSyntax;

    private readonly INamedTypeSymbol _typeSymbol;
    private readonly TypeDeclarationSyntax _typeDeclaration;
    private readonly BitOrder _defaultBitOrder;
    private readonly List<BitFieldModel> _fields = new();

    public TypeSymbolProcessor(INamedTypeSymbol typeSymbol, TypeDeclarationSyntax typeDeclaration, AttributeData attribute)
    {
        _typeSymbol = typeSymbol;
        _typeDeclaration = typeDeclaration;
        _defaultBitOrder = (BitOrder)attribute.ConstructorArguments[0].Value!;
    }

    public void GenerateCSharpSource(StringBuilder sb)
    {
        sb.AppendIndentedLine(1,
            StringConstants.TypeDeclarationTemplate,
            _typeDeclaration.Modifiers,
            _typeDeclaration.Keyword.Text,
            (_typeDeclaration as RecordDeclarationSyntax)?.ClassOrStructKeyword.Text,
            _typeDeclaration.Identifier.Text)
          .AppendIndentedLine(1, "{");

        foreach (BitFieldModel field in _fields)
            field.GenerateCSharpSource(sb);

        sb.RemoveLastLine()
          .AppendIndentedLine(1, "}")
          .AppendLine();
    }

    public int EnumerateFields()
    {
        _fields.Clear();

        foreach (IFieldSymbol field in _typeSymbol.GetMembers().OfType<IFieldSymbol>())
        {
            if (!IsValidFieldSymbol(field))
                continue;

            BackingFieldType backingType = field.Type.ToDisplayString() switch
            {
                "System.Memory<byte>" => BackingFieldType.Memory,
                "System.ReadOnlyMemory<byte>" => BackingFieldType.Memory,
                "System.Span<byte>" => BackingFieldType.Span,
                "System.ReadOnlySpan<byte>" => BackingFieldType.Span,
                "byte[]" => BackingFieldType.Span,
                "byte*" => BackingFieldType.Pointer,
                _ when IsSupportedIntegralType(field) => BackingFieldType.Integral,
                _ => BackingFieldType.Invalid
            };

            if (backingType == BackingFieldType.Invalid)
                continue;

            CreateBitFieldModels(field, backingType);
        }

        return _fields.Count;
    }

    public bool ReportCompilationIssues(SourceProductionContext context)
    {
        bool hasCompilationIssues = false;

        if (DiagnosticValidator.IsNotPartial(context, _typeDeclaration, _typeSymbol.Name) |
            DiagnosticValidator.IsNested(context, _typeDeclaration, _typeSymbol.Name))
            hasCompilationIssues = true;

        foreach (BitFieldModel field in _fields)
        {
            if (field.HasCompilationIssues(context, this))
                hasCompilationIssues = true;
        }

        return hasCompilationIssues;
    }

    private void CreateBitFieldModels(IFieldSymbol backingField, BackingFieldType backingType)
    {
        int offset = 0;

        foreach (AttributeData attribute in backingField.GetAttributes())
        {
            string? attributeType = attribute.AttributeClass?.ToDisplayString();

            BitFieldModel? bitField = attributeType switch
            {
                StringConstants.BitFieldAttributeFullName => new IntegralFieldModel(attribute),
                StringConstants.BooleanFieldAttributeFullName => new BooleanFieldModel(attribute),
                StringConstants.EnumFieldAttributeFullName => new EnumFieldModel(attribute),
                _ => null
            };

            if (bitField == null)
                continue;

            bitField.BackingField = backingField;
            bitField.BackingFieldType = backingType;
            bitField.BitOffset = offset;
            bitField.BitOrder = _defaultBitOrder;

            // padding fields are not generated
            if (bitField is not { FieldType: BitFieldType.Padding })
            {
                // invert the bit order if necessary
                if (bitField.ReverseBitOrder)
                    bitField.BitOrder ^= BitOrder.MostSignificant;

                // integrals inherit their field type from their backing field
                if (backingType == BackingFieldType.Integral)
                    bitField.FieldType = backingField.Type.SpecialType.ToBitFieldType();

                // add to list of fields to generate
                _fields.Add(bitField);
            }

            offset += bitField.BitCount;
        }
    }

    private static bool IsValidFieldSymbol(IFieldSymbol member) => member is
    {
        CanBeReferencedByName: true,
        IsConst: false,
        IsStatic: false,
        IsImplicitlyDeclared: false,
        Type: { }
    };

    private static bool IsSupportedIntegralType(IFieldSymbol field) => field.Type?.SpecialType switch
    {
        SpecialType.System_Byte or
        SpecialType.System_SByte or
        SpecialType.System_Int16 or
        SpecialType.System_UInt16 or
        SpecialType.System_Int32 or
        SpecialType.System_UInt32 or
        SpecialType.System_Int64 or
        SpecialType.System_UInt64 or
        SpecialType.System_IntPtr or
        SpecialType.System_UIntPtr => true,
        _ => false,
    };
}
