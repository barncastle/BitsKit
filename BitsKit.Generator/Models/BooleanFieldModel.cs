using Microsoft.CodeAnalysis;

namespace BitsKit.Generator.Models;

/// <summary>
/// A model representing a boolean bit-field
/// </summary>
internal sealed class BooleanFieldModel : BitFieldModel
{
    public BooleanFieldModel(AttributeData attributeData) : base(attributeData)
    {
        if (attributeData.ConstructorArguments.Length != 1)
            return;

        Name = (string)attributeData.ConstructorArguments[0].Value!;
        BitCount = 1;
        FieldType = BitFieldType.Boolean;
        ReturnType = typeof(bool);
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
