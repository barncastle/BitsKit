namespace BitsKit.BitFields;

/// <summary>Attribute that defines each bitfield to be generated.</summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public sealed class BitFieldAttribute : Attribute
{
    /// <summary>
    /// The name of the bit-field that is being declared
    /// <para>
    /// Null or empty will generate padding bits
    /// </para>
    /// </summary>
    public string? Name { get; }
    /// <summary>
    /// The number of bits that this bit-field will occupy
    /// </summary>
    public byte Size { get; }
    /// <summary>
    /// The primitive type of this bit-field
    /// <para>
    /// This is only used for boolean and memory backed bit-fields
    /// </para>
    /// </summary>
    public BitFieldType? FieldType { get; }
    /// <summary>
    /// Uses the opposite bit order to the object's <see cref="BitObjectAttribute.DefaultOrder"/>
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
