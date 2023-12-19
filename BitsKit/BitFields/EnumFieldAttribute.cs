namespace BitsKit.BitFields;

public sealed class EnumFieldAttribute : BitFieldAttribute
{
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
