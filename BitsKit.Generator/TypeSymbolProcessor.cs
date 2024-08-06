using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Text;
using BitsKit.Generator.Models;
using System.Linq;

namespace BitsKit.Generator;

internal sealed class TypeSymbolProcessor
{
    public INamedTypeSymbol TypeSymbol { get; }
    public TypeDeclarationSyntax TypeDeclaration { get; }
    public IReadOnlyList<BitFieldModel> Fields => _fields;
    public BaseNamespaceDeclarationSyntax? Namespace { get; }
    public bool IsInlineArray { get; }

    private readonly BitOrder _defaultBitOrder;
    private readonly List<BitFieldModel> _fields = [];

    public TypeSymbolProcessor(INamedTypeSymbol typeSymbol, TypeDeclarationSyntax typeDeclaration, AttributeData attribute)
    {
        TypeSymbol = typeSymbol;
        TypeDeclaration = typeDeclaration;
        Namespace = TypeDeclaration.Parent as BaseNamespaceDeclarationSyntax;
        IsInlineArray = HasInlineArrayAttribute();

        _defaultBitOrder = (BitOrder)attribute.ConstructorArguments[0].Value!;
    }

    public void GenerateCSharpSource(StringBuilder sb)
    {
        sb.AppendIndentedLine(1,
            StringConstants.TypeDeclarationTemplate,
            TypeDeclaration.Modifiers,
            TypeDeclaration.Keyword.Text,
            (TypeDeclaration as RecordDeclarationSyntax)?.ClassOrStructKeyword.Text,
            TypeDeclaration.Identifier.Text)
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

        foreach (IFieldSymbol field in TypeSymbol.GetMembers().OfType<IFieldSymbol>())
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

                _ when field.Type.IsSupportedIntegralType() => IsInlineArray ? 
                    BackingFieldType.InlineArray : 
                    BackingFieldType.Integral,

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

        if (DiagnosticValidator.IsNotPartial(context, TypeDeclaration, TypeSymbol.Name) |
            DiagnosticValidator.IsNested(context, TypeDeclaration, TypeSymbol.Name))
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
                StringConstants.BitFieldAttributeFullName => new IntegralFieldModel(attribute, this),
                StringConstants.BooleanFieldAttributeFullName => new BooleanFieldModel(attribute, this),
                StringConstants.EnumFieldAttributeFullName => new EnumFieldModel(attribute, this),
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

                // allow inline arrays to infer their type
                if (backingType == BackingFieldType.InlineArray)
                    bitField.FieldType ??= backingField.Type.SpecialType.ToBitFieldType();

                // add to list of fields to generate
                _fields.Add(bitField);
            }

            offset += bitField.BitCount;
        }
    }

    private bool HasInlineArrayAttribute()
    {
        return (int?)TypeSymbol
            .GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == StringConstants.InlineArrayAttributeFullName)?
            .ConstructorArguments[0].Value > 0;
    }

    private static bool IsValidFieldSymbol(IFieldSymbol member) => member is
    {
        CanBeReferencedByName: true,
        IsConst: false,
        IsStatic: false,
        IsImplicitlyDeclared: false,
        Type: { }
    };
}
