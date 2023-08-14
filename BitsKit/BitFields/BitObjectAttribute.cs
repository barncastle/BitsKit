namespace BitsKit.BitFields;

/// <summary>
/// An attribute that declares an object contains bit-fields
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
public sealed class BitObjectAttribute : Attribute
{
    /// <summary>
    /// Defines the default bit order for the object
    /// </summary>
    public BitOrder DefaultOrder { get; } = BitOrder.LeastSignificant;

    public BitObjectAttribute(BitOrder defaultBitOrder)
    {
        DefaultOrder = defaultBitOrder;
    }
}
