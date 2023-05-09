using BitsKit.Utilities;

namespace BitsKit.Primitives;

public partial class BitPrimitives
{
    /// <summary>Reverses the bit order of each byte within a primitive value</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte ReverseBitOrder(sbyte value)
    {
        return BitUtilities.ReverseBits(value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short ReverseBitOrder(short value)
    {
        return (short)ReverseBitOrder((ulong)(ushort)value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ReverseBitOrder(int value)
    {
        return (int)ReverseBitOrder((ulong)(uint)value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ReverseBitOrder(long value)
    {
        return (long)ReverseBitOrder((ulong)value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint ReverseBitOrder(nint value)
    {
        if (IntPtr.Size == 8)
            return (nint)ReverseBitOrder((long)value);
        else
            return ReverseBitOrder((int)value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte ReverseBitOrder(byte value)
    {
        return BitUtilities.ReverseBits(value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ReverseBitOrder(ushort value)
    {
        uint temp = value;

        // swap odd and even bits
        temp = ((temp >> 1) & 0x5555) | ((temp & 0x5555) << 1);
        // swap consecutive pairs
        temp = ((temp >> 2) & 0x3333) | ((temp & 0x3333) << 2);
        // swap nibbles 
        temp = ((temp >> 4) & 0x0F0F) | ((temp & 0x0F0F) << 4);

        return (ushort)temp;
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ReverseBitOrder(uint value)
    {
        // swap odd and even bits
        value = ((value >> 1) & 0x55555555) | ((value & 0x55555555) << 1);
        // swap consecutive pairs
        value = ((value >> 2) & 0x33333333) | ((value & 0x33333333) << 2);
        // swap nibbles 
        value = ((value >> 4) & 0x0F0F0F0F) | ((value & 0x0F0F0F0F) << 4);

        return value;
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ReverseBitOrder(ulong value)
    {
        // 32 bit operations have higher throughput than 64 bit

        ulong lo = ReverseBitOrder((uint)(value >> 0x00));
        ulong hi = ReverseBitOrder((uint)(value >> 0x20));

        return (hi << 32) | lo;
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint ReverseBitOrder(nuint value)
    {
        if (IntPtr.Size == 8)
            return (nuint)ReverseBitOrder((ulong)value);
        else
            return ReverseBitOrder((uint)value);
    }

    /// <inheritdoc cref="ReverseBitOrder(sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static UInt128 ReverseBitOrder(UInt128 value)
    {
        return new(
            ReverseBitOrder((ulong)(value >> 0x40)),
            ReverseBitOrder((ulong)(value >> 0x00))
        );
    }
}
