using Microsoft.CodeAnalysis;

namespace BitsKit.Generator.Models;

/// <summary>
/// A model representing an enum bit-field
/// </summary>
internal sealed class EnumFieldModel : BitFieldModel
{
    public INamedTypeSymbol? EnumType { get; }

    public EnumFieldModel(AttributeData attributeData) : base(attributeData)
    {
        switch(attributeData.ConstructorArguments.Length)
        {
            case 1: // padding constructor
                BitCount = (byte)attributeData.ConstructorArguments[0].Value!;
                break;
            case 3: // enum constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
                EnumType = attributeData.ConstructorArguments[2].Value as INamedTypeSymbol;
                break;
            default:
                return;
        }

        ReturnType = EnumType?.ToDisplayString();
        FieldType = EnumType?.EnumUnderlyingType?.SpecialType.ToBitFieldType();

        if (string.IsNullOrEmpty(Name))
            FieldType = BitFieldType.Padding;
    }

    public override bool HasCompilationIssues(SourceProductionContext context, TypeSymbolProcessor processor)
    {
        return DiagnosticValidator.IsNotEnumType(context, this, processor.TypeSymbol.Name) |
               base.HasCompilationIssues(context, processor);
    }

    protected override string GetGetterTemplate()
    {
        return string.Format(StringConstants.ExplicitGetterTemplate, GetterSource(), ReturnType);
    }

    protected override string GetSetterTemplate()
    {
        return string.Format(StringConstants.ExplicitSetterTemplate, SetterSource(), FieldType);
    }
}
