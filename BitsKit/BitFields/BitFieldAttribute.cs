namespace BitsKit.BitFields;

/// <summary>
/// An attribute that declares a bit-field
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class BitFieldAttribute : Attribute
{
    /// <summary>
    /// The name of the bit-field that is being declared
    /// <remark>
    /// <para>
    /// Null or empty will generate padding bits
    /// </para>
    /// </remark>
    /// </summary>
    public string? Name { get; }
    /// <summary>
    /// The number of bits that this bit-field will occupy
    /// </summary>
    public byte Size { get; }
    /// <summary>
    /// The primitive type of this bit-field
    /// <remark>
    /// <para>
    /// This is only used for boolean and memory backed bit-fields
    /// </para>
    /// </remark>
    /// </summary>
    public BitFieldType? FieldType { get; }
    /// <summary>
    /// Indicates if the bit-field uses the object's default bit order or not
    /// </summary>
    public bool ReverseBitOrder { get; set; }
    /// <summary>
    /// The modifiers to be applied during the generation of this bit-field
    /// </summary>
    public BitFieldModifiers Modifiers { get; set; } = BitFieldModifiers.Public;

    /// <summary>
    /// Constructor for padding bit-fields
    /// </summary>
    public BitFieldAttribute(byte size)
    {
        Size = size;
    }

    /// <summary>
    /// Constructor for integeral backed bit-fields
    /// </summary>
    public BitFieldAttribute(string name, byte size)
    {
        Name = name;
        Size = size;
    }

    /// <summary>
    /// Constructor for memory backed bit-fields
    /// </summary>
    public BitFieldAttribute(string name, byte size, BitFieldType fieldType)
    {
        Name = name;
        Size = size;
        FieldType = fieldType;
    }
}
