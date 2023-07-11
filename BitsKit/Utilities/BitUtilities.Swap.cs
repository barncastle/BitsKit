namespace BitsKit.Utilities;

public static partial class BitUtilities
{
    /// <summary>
    /// Swaps the position of two <paramref name="bitCount"/> sized ranges of bits 
    /// within an integral value
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte SwapBits(sbyte value, int offsetA, int offsetB, int bitCount)
    {
        return (sbyte)SwapBits((uint)value, offsetA, offsetB, bitCount);
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short SwapBits(short value, int offsetA, int offsetB, int bitCount)
    {
        return (short)SwapBits((uint)value, offsetA, offsetB, bitCount);
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SwapBits(int value, int offsetA, int offsetB, int bitCount)
    {
        return (int)SwapBits((uint)value, offsetA, offsetB, bitCount);
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long SwapBits(long value, int offsetA, int offsetB, int bitCount)
    {
        return (long)SwapBits((ulong)value, offsetA, offsetB, bitCount);
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SwapBits(byte value, int offsetA, int offsetB, int bitCount)
    {
        return (byte)SwapBits((uint)value, offsetA, offsetB, bitCount);
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort SwapBits(ushort value, int offsetA, int offsetB, int bitCount)
    {
        return (ushort)SwapBits((uint)value, offsetA, offsetB, bitCount);
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SwapBits(uint value, int offsetA, int offsetB, int bitCount)
    {
        uint x = ((value >> offsetA) ^ (value >> offsetB)) & ((1u << bitCount) - 1);
        return value ^ ((x << offsetA) | (x << offsetB));
    }

    /// <inheritdoc cref="SwapBits(sbyte, int, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong SwapBits(ulong value, int offsetA, int offsetB, int bitCount)
    {
        ulong x = ((value >> offsetA) ^ (value >> offsetB)) & ((1UL << bitCount) - 1);
        return value ^ ((x << offsetA) | (x << offsetB));
    }
}
