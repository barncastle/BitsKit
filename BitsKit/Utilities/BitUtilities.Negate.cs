namespace BitsKit.Utilities;

public static partial class BitUtilities
{
    /// <summary>
    /// Negates a specific range of bits within an integral value
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte NegateBits(sbyte value, int bitOffset, int bitCount)
    {
        return (sbyte)NegateBits((byte)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short NegateBits(short value, int bitOffset, int bitCount)
    {
        return (short)NegateBits((ushort)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int NegateBits(int value, int bitOffset, int bitCount)
    {
        return (int)NegateBits((uint)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long NegateBits(long value, int bitOffset, int bitCount)
    {
        return (long)NegateBits((ulong)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte NegateBits(byte value, int bitOffset, int bitCount)
    {
        return (byte)NegateBitsBase(value, bitOffset, bitCount, 8);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort NegateBits(ushort value, int bitOffset, int bitCount)
    {
        return (ushort)NegateBitsBase(value, bitOffset, bitCount, 16);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint NegateBits(uint value, int bitOffset, int bitCount)
    {
        return NegateBitsBase(value, bitOffset, bitCount, 32);
    }

    /// <inheritdoc cref="NegateBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong NegateBits(ulong value, int bitOffset, int bitCount)
    {
        if (bitCount == 0)
            return value;

        if (bitCount == 64 && bitOffset == 0)
            return ~value;

        return value ^ (~(ulong.MaxValue << bitCount) << bitOffset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint NegateBitsBase(uint value, int bitOffset, int bitCount, int maxBits)
    {
        if (bitCount == 0)
            return value;

        if (bitCount == maxBits && bitOffset == 0)
            return ~value;

        return value ^ (~(uint.MaxValue << bitCount) << bitOffset);
    }
}
