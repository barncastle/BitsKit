namespace BitsKit.BitFields;

/// <summary>
/// A marker attribute to declare if an object contains 
/// bit-fields and their default bit order
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
public sealed class BitObjectAttribute : Attribute
{
    /// <summary>
    /// Defines the default bit-field bit order
    /// </summary>
    public BitOrder DefaultOrder { get; }

    public BitObjectAttribute(BitOrder defaultBitOrder)
    {
        DefaultOrder = defaultBitOrder;
    }
}
