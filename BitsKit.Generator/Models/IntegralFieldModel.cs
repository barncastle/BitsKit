using Microsoft.CodeAnalysis;

namespace BitsKit.Generator.Models;

/// <summary>
/// A model representing an integral bit-field
/// </summary>
internal sealed class IntegralFieldModel : BitFieldModel
{
    private bool IsTypeCast => this is
    {
        BackingFieldType: BackingFieldType.Integral,
        ReturnType.Length: > 0
    };

    public IntegralFieldModel(AttributeData attributeData, TypeSymbolProcessor typeSymbol) : base(attributeData, typeSymbol)
    {
        switch (attributeData.ConstructorArguments.Length)
        {
            case 1: // Padding constructor
                BitCount = (byte)attributeData.ConstructorArguments[0].Value!;
                break;
            case 2: // Integral backed constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
                break;
            case 3: // Memory backed OR Type Cast constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
                FieldType = (BitFieldType)attributeData.ConstructorArguments[2].Value!;
                break;
            default:
                return;
        }

        if (FieldType is not null)
            ReturnType = FieldType.ToString();

        if (string.IsNullOrEmpty(Name))
            FieldType = BitFieldType.Padding;
    }

    protected override string GetGetterTemplate()
    {
        if (IsTypeCast)
            return string.Format(StringConstants.ExplicitGetterTemplate, GetterSource(), ReturnType);

        return string.Format(StringConstants.IntegralGetterTemplate, GetterSource());
    }

    protected override string GetSetterTemplate()
    {
        if (IsTypeCast)
            return string.Format(StringConstants.ExplicitSetterTemplate, SetterSource(), FieldType);

        return string.Format(StringConstants.IntegralSetterTemplate, SetterSource());
    }
}
