namespace BitsKit.BitFields;

public sealed class EnumFieldAttribute : BitFieldAttribute
{
    public Type EnumType { get; }

    public EnumFieldAttribute(string name, byte size, Type enumType) : base(name, size)
    {
        EnumType = enumType;
    }
}
