namespace BitsKit.BitFields;

/// <summary>
/// Modifiers that change the generated source code of a bit-field
/// </summary>
[Flags]
public enum BitFieldModifiers
{
    /// <summary>
    /// Generates a public settable property
    /// </summary>
    None = 0,
    /// <summary>
    /// Specifies the property is public
    /// </summary>
    Public = 0x01,
    /// <summary>
    /// Specifies the property is internal
    /// </summary>
    Internal = 0x02,
    /// <summary>
    /// Specifies the property is private
    /// </summary>
    Private = 0x04,
    /// <summary>
    /// Specifies the property is readonly
    /// </summary>
    ReadOnly = 0x08,
#if NET6_0_OR_GREATER
    /// <summary>
    /// Specifies the property is init only
    /// </summary>
    InitOnly = 0x10,
#endif
#if NET7_0_OR_GREATER
    /// <summary>
    /// Specifies the property is required
    /// </summary>
    Required = 0x20,
#endif
}
