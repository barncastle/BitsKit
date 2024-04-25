namespace BitsKit.BitFields;

/// <summary>
/// An attribute that declares an Enum typed bit-field
/// </summary>
public sealed class EnumFieldAttribute : BitFieldAttribute
{
    /// <summary>
    /// The Enum type of this bit-field
    /// </summary>
    public Type EnumType { get; }

    /// <summary>
    /// Constructor for padding bit-fields
    /// </summary>
    public EnumFieldAttribute(byte size) : base(size)
    {
        EnumType = typeof(object);
    }

    /// <summary>
    /// Constructor for enum bit-fields
    /// </summary>
    public EnumFieldAttribute(string name, byte size, Type enumType) : base(name, size)
    {
        EnumType = enumType;
    }
}
