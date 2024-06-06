using Microsoft.CodeAnalysis;

namespace BitsKit.Generator.Models;

/// <summary>
/// A model representing a boolean bit-field
/// </summary>
internal sealed class BooleanFieldModel : BitFieldModel
{
    public BooleanFieldModel(AttributeData attributeData, TypeSymbolProcessor typeSymbol) : base(attributeData, typeSymbol)
    {
        switch (attributeData.ConstructorArguments.Length)
        {
            case 1: // boolean constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                break;
            case 0: // padding constructor
                break;
            default:
                return;
        }

        BitCount = 1;
        FieldType = BitFieldType.Boolean;
        ReturnType = typeof(bool).FullName;

        if (string.IsNullOrEmpty(Name))
            FieldType = BitFieldType.Padding;
    }

    protected override string GetGetterTemplate()
    {
        string template = BackingFieldType == BackingFieldType.Integral ?
            StringConstants.BooleanGetterTemplate :
            StringConstants.BooleanSpanGetterTemplate;

        return string.Format(template, GetterSource());
    }

    protected override string GetSetterTemplate()
    {
        string template = BackingFieldType == BackingFieldType.Integral ?
            StringConstants.BooleanSetterTemplate :
            StringConstants.BooleanSpanSetterTemplate;

        return string.Format(template, SetterSource(), FieldType);
    }
}
