namespace BitsKit.Utilities;

public static partial class BitUtilities
{
    /// <summary>
    /// Reverses the order of all bits in an integral value. 
    /// This is functionally equivalent to reversing both the bit order and endianness
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte ReverseBits(sbyte value)
    {
        return (sbyte)UInt8BitReverseTable[value];
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short ReverseBits(short value)
    {
        return (short)ReverseBits((ushort)value);
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ReverseBits(int value)
    {
        return (int)ReverseBits((uint)value);
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ReverseBits(long value)
    {
        return (long)ReverseBits((ulong)value);
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte ReverseBits(byte value)
    {
        return UInt8BitReverseTable[value];
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ReverseBits(ushort value)
    {
        uint temp = value;

        // swap odd and even bits
        temp = ((temp >> 1) & 0x5555) | ((temp & 0x5555) << 1);
        // swap consecutive pairs
        temp = ((temp >> 2) & 0x3333) | ((temp & 0x3333) << 2);
        // swap nibbles 
        temp = ((temp >> 4) & 0x0F0F) | ((temp & 0x0F0F) << 4);
        // swap bytes
        temp = ((temp >> 8) & 0x00FF) | ((temp & 0x00FF) << 8);

        return (ushort)temp;
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ReverseBits(uint value)
    {
        // swap odd and even bits
        value = ((value >> 1) & 0x55555555) | ((value & 0x55555555) << 1);
        // swap consecutive pairs
        value = ((value >> 2) & 0x33333333) | ((value & 0x33333333) << 2);
        // swap nibbles 
        value = ((value >> 4) & 0x0F0F0F0F) | ((value & 0x0F0F0F0F) << 4);
        // swap bytes
        value = ((value >> 8) & 0x00FF00FF) | ((value & 0x00FF00FF) << 8);
        // swap 2-byte long pairs
        value = (value >> 16) | (value << 16);

        return value;
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ReverseBits(ulong value)
    {
        // two uint reverses is significantly faster
        ulong hi = ReverseBits((uint)(value >> 0x00));
        ulong lo = ReverseBits((uint)(value >> 0x20));

        return (hi << 32) | lo;
    }

    /// <inheritdoc cref="ReverseBits(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static UInt128 ReverseBits(UInt128 value)
    {
        return new(
            ReverseBits((ulong)(value >> 0x00)),
            ReverseBits((ulong)(value >> 0x40))
        );
    }
}
