using Microsoft.CodeAnalysis;

namespace BitsKit.Generator.Models;

/// <summary>
/// A model representing an integral bit-field
/// </summary>
internal sealed class IntegralFieldModel : BitFieldModel
{
    public IntegralFieldModel(AttributeData attributeData) : base(attributeData)
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
            case 3: // Memory backed constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
                FieldType = (BitFieldType)attributeData.ConstructorArguments[2].Value!;
                break;
            default:
                return;
        }

        if (string.IsNullOrEmpty(Name))
            FieldType = BitFieldType.Padding;
    }

    protected override string GetGetterTemplate()
    {
        return string.Format(StringConstants.IntegralGetterTemplate, GetterSource());
    }

    protected override string GetSetterTemplate()
    {
        return string.Format(StringConstants.IntegralSetterTemplate, SetterSource());
    }
}
