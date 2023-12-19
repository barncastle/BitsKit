namespace BitsKit.BitFields;

/// <summary>
/// An attribute that declares a boolean bit-field
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
public sealed class BooleanFieldAttribute : BitFieldAttribute
{
    /// <summary>
    /// Constructor for padding bit-fields
    /// </summary>
    public BooleanFieldAttribute() : base(1)
    {
    }

    /// <summary>
    /// Constructor for boolean bit-fields
    /// </summary>
    public BooleanFieldAttribute(string name) : base(name, 1)
    {
    }
}
