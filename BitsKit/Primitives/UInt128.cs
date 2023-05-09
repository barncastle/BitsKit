using System.Buffers.Binary;

#if !NET7_0_OR_GREATER

/// <summary>
/// Represents a 128-bit unsigned integer.
/// <remark>Polyfill for pre-NET7_0</remark>
/// </summary>
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
internal readonly struct UInt128
{
    public static UInt128 MinValue => new(0, 0);
    public static UInt128 MaxValue => new(0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF);

#if BIGENDIAN
    private readonly ulong _upper;
    private readonly ulong _lower;
#else
    private readonly ulong _lower;
    private readonly ulong _upper;
#endif

    public UInt128(ulong upper, ulong lower)
    {
        _lower = lower;
        _upper = upper;
    }

    public static implicit operator UInt128(ulong value) => new(0, value);

    public static explicit operator ulong(UInt128 value) => value._lower;

    public static bool operator ==(UInt128 left, UInt128 right) => left._lower == right._lower && left._upper == right._upper;

    public static bool operator !=(UInt128 left, UInt128 right) => left._lower != right._lower || left._upper != right._upper;

    public static UInt128 operator &(UInt128 left, UInt128 right) => new(left._upper & right._upper, left._lower & right._lower);

    public static UInt128 operator |(UInt128 left, UInt128 right) => new(left._upper | right._upper, left._lower | right._lower);

    public static UInt128 operator ^(UInt128 left, UInt128 right) => new(left._upper ^ right._upper, left._lower ^ right._lower);

    public static UInt128 operator ~(UInt128 value) => new(~value._upper, ~value._lower);

    public static UInt128 operator <<(UInt128 value, int shiftAmount)
    {
        shiftAmount &= 0x7F;

        if (shiftAmount == 0)
            return value;

        if ((shiftAmount & 0x40) != 0)
            return new(value._lower << shiftAmount, 0);

        ulong upper = value._upper << shiftAmount | value._lower >> (64 - shiftAmount);
        ulong lower = value._lower << shiftAmount;

        return new(upper, lower);
    }

    public static UInt128 operator >>(UInt128 value, int shiftAmount)
    {
        shiftAmount &= 0x7F;

        if (shiftAmount == 0)
            return value;

        if ((shiftAmount & 0x40) != 0)
            return new(0, value._upper >> shiftAmount);

        ulong upper = value._upper >> shiftAmount;
        ulong lower = value._lower >> shiftAmount | value._upper << (64 - shiftAmount);
        
        return new(upper, lower);
    }

    public override bool Equals(object? obj) => obj is UInt128 other && other == this;

    public override int GetHashCode() => HashCode.Combine(_lower, _upper);
}

#endif

internal static class UInt128Helper
{
    /// <summary>
    /// Reverses an unsigned 128-bit integral value - performs an endianness swap
    /// <remark>Not publically available until NET8_0.</remark>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 ReverseEndianness(UInt128 value)
    {
        return new(
            BinaryPrimitives.ReverseEndianness((ulong)(value >> 00)),
            BinaryPrimitives.ReverseEndianness((ulong)(value >> 64))
        );
    }
}
