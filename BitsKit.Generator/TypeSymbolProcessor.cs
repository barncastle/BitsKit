using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Text;

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

    private void CreateBitFieldModels(IFieldSymbol field, BackingFieldType backingType)
    {
        int offset = 0;

        foreach (AttributeData attribute in field.GetAttributes())
        {
            string? attributeType = attribute.AttributeClass?.ToDisplayString();

            if (!IsBitFieldAttribute(attributeType))
                continue;

            BitFieldModel bitField = new(attribute, attributeType!)
            {
                BackingField = field,
                BackingFieldType = backingType,
                BitOffset = offset,
                BitOrder = _defaultBitOrder,
            };

            // padding fields are not generated
            if (bitField is not { FieldType: BitFieldType.Padding })
            {
                // invert the bit order if necessary
                if (bitField.ReverseBitOrder)
                    bitField.BitOrder ^= BitOrder.MostSignificant;

                // integrals inherit their field type from their backing field
                if (backingType == BackingFieldType.Integral)
                    bitField.FieldType = field.Type.SpecialType.ToBitFieldType();

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

    private static bool IsBitFieldAttribute(string? attributeClass) => attributeClass is
        StringConstants.BitFieldAttributeFullName or
        StringConstants.BooleanFieldAttributeFullName;
}
