namespace BitsKit.Generator;

internal static class StringConstants
{
    public const string BitFieldAttributeFullName = "BitsKit.BitFields.BitFieldAttribute";
    public const string BitFieldModifiersFullName = "BitsKit.BitFields.BitFieldModifiers";
    public const string BitFieldTypeFullName = "BitsKit.BitFields.BitFieldType";
    public const string BitObjectAttributeFullName = "BitsKit.BitFields.BitObjectAttribute";
    public const string BitOrderFullName = "BitsKit.BitFields.BitOrder";

    /// <summary>
    /// Generated code header
    /// </summary>
    public const string Header = """
    #nullable enable
    #pragma warning disable IDE0005 // Using directive is unnecessary
    #pragma warning disable IDE0005_gen // Using directive is unnecessary
    #pragma warning disable CS8019  // Unnecessary using directive.
    #pragma warning disable IDE0161 // Convert to file-scoped namespace
    
    using System;
    using System.Runtime.InteropServices;
    using BitsKit.Primitives;

    """;

    /// <summary>
    /// Template for the type declaration
    /// <para>
    /// {0} = Modifiers<br/>
    /// {1} = Keyword<br/>
    /// {2} = Record ClassOrStructKeyword<br/>
    /// {3} = Identifier 
    /// </para>
    /// </summary>
    public const string TypeDeclarationTemplate = "{0} {1} {2} {3}";

    /// <summary>
    /// Template for a property declaration
    /// </summary>
    public const string PropertyTemplate = "{0} {1} {2} {3} ";
    /// <summary>
    /// Template for an unsafe property declaration
    /// </summary>
    public const string UnsafePropertyTemplate = "{0} unsafe {1} {2} {3} ";

    /// <summary>
    /// Getter template for reading integral bits
    /// <para>
    /// {0} = Source
    /// </para>
    /// </summary>
    public const string IntegralGetterTemplate = "get => BitPrimitives.Read{{0}}{{1}}({0}, {{3}}, {{4}});";
    /// <summary>
    /// Getter template for reading a boolean from an integral
    /// <para>
    /// {0} = Source
    /// </para>
    /// </summary>
    public const string BooleanGetterTemplate = "get => BitPrimitives.Read{{0}}{{1}}({0}, {{3}}, 1) == 1;";
    /// <summary>
    /// Getter template for reading a boolean from a span
    /// <para>
    /// {0} = Source
    /// </para>
    /// </summary>
    public const string BooleanSpanGetterTemplate = "get => BitPrimitives.ReadBit{{1}}({0}, {{3}});";

    /// <summary>
    /// Setter template for writing integral bits
    /// <para>
    /// {0} = Source
    /// </para>
    /// </summary>
    public const string IntegralSetterTemplate = "{{0}} => BitPrimitives.Write{{1}}{{2}}({0}, {{4}}, value, {{5}});";
    /// <summary>
    /// Setter template for writing a boolean to an integral
    /// <para>
    /// {0} = Source<br/>
    /// {1} = FieldType
    /// </para>
    /// </summary>
    public const string BooleanSetterTemplate = "{{0}} => BitPrimitives.Write{{1}}{{2}}({0}, {{4}}, ({1})(value ? 1 : 0), 1);";
    /// <summary>
    /// Setter template for writing a boolean to a span
    /// <para>
    /// {0} = Source
    /// </para>
    /// </summary>
    public const string BooleanSpanSetterTemplate = "{{0}} => BitPrimitives.WriteBit{{2}}({0}, {{4}}, value);";
}
