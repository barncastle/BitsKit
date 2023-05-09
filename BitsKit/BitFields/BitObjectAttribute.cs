namespace BitsKit.BitFields;

/// <summary>
/// A marker attribute declaring an object contains bit-fields
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
public sealed class BitObjectAttribute : Attribute
{
    /// <summary>
    /// Defines the default bit order for the type
    /// </summary>
    public BitOrder DefaultOrder { get; }

    public BitObjectAttribute(BitOrder defaultBitOrder)
    {
        DefaultOrder = defaultBitOrder;
    }
}
