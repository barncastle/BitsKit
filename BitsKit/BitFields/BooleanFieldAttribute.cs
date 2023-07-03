namespace BitsKit.BitFields;

/// <summary>
/// An attribute that declares a boolean bit-field
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
public sealed class BooleanFieldAttribute : BitFieldAttribute
{
    public BooleanFieldAttribute(string name) : base(name, 1, BitFieldType.Boolean)
    {
    }
}
