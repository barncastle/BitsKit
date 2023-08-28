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
        if (attributeData.ConstructorArguments.Length != 3)
            return;

        Name = (string)attributeData.ConstructorArguments[0].Value!;
        BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
        EnumType = attributeData.ConstructorArguments[2].Value as INamedTypeSymbol;

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
        return string.Format(StringConstants.EnumGetterTemplate, GetterSource(), ReturnType);
    }

    protected override string GetSetterTemplate()
    {
        return string.Format(StringConstants.EnumSetterTemplate, SetterSource(), FieldType);
    }
}
