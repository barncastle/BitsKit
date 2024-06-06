using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Text;
using BitsKit.Generator.Models;

namespace BitsKit.Generator;

internal sealed class TypeSymbolProcessor(INamedTypeSymbol typeSymbol, TypeDeclarationSyntax typeDeclaration, AttributeData attribute)
{
    public INamedTypeSymbol TypeSymbol => typeSymbol;
    public TypeDeclarationSyntax TypeDeclaration => typeDeclaration;
    public IReadOnlyList<BitFieldModel> Fields => _fields;
    public BaseNamespaceDeclarationSyntax? Namespace => typeDeclaration.Parent as BaseNamespaceDeclarationSyntax;

    private readonly BitOrder _defaultBitOrder = (BitOrder)attribute.ConstructorArguments[0].Value!;
    private readonly List<BitFieldModel> _fields = [];

    public void GenerateCSharpSource(StringBuilder sb)
    {
        sb.AppendIndentedLine(1,
            StringConstants.TypeDeclarationTemplate,
            typeDeclaration.Modifiers,
            typeDeclaration.Keyword.Text,
            (typeDeclaration as RecordDeclarationSyntax)?.ClassOrStructKeyword.Text,
            typeDeclaration.Identifier.Text)
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

        foreach (IFieldSymbol field in typeSymbol.GetMembers().OfType<IFieldSymbol>())
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
                _ when field.Type.IsSupportedIntegralType() => BackingFieldType.Integral,
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

        if (DiagnosticValidator.IsNotPartial(context, typeDeclaration, typeSymbol.Name) |
            DiagnosticValidator.IsNested(context, typeDeclaration, typeSymbol.Name))
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
}
