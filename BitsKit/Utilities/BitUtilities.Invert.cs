namespace BitsKit.Utilities;

public static partial class BitUtilities
{
    /// <summary>
    /// Inverts a specific range of bits of an integer
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte InvertBits(sbyte value, int bitOffset, int bitCount)
    {
        return (sbyte)InvertBits((byte)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short InvertBits(short value, int bitOffset, int bitCount)
    {
        return (short)InvertBits((ushort)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int InvertBits(int value, int bitOffset, int bitCount)
    {
        return (int)InvertBits((uint)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long InvertBits(long value, int bitOffset, int bitCount)
    {
        return (long)InvertBits((ulong)value, bitOffset, bitCount);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte InvertBits(byte value, int bitOffset, int bitCount)
    {
        ValidateArgs(bitOffset, bitCount, 8);

        return (byte)InvertBitsBase(value, bitOffset, bitCount, 8);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort InvertBits(ushort value, int bitOffset, int bitCount)
    {
        ValidateArgs(bitOffset, bitCount, 16);

        return (ushort)InvertBitsBase(value, bitOffset, bitCount, 16);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint InvertBits(uint value, int bitOffset, int bitCount)
    {
        ValidateArgs(bitOffset, bitCount, 32);

        return InvertBitsBase(value, bitOffset, bitCount, 32);
    }

    /// <inheritdoc cref="InvertBits(sbyte, int, int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong InvertBits(ulong value, int bitOffset, int bitCount)
    {
        ValidateArgs(bitOffset, bitCount, 64);

        if (bitCount == 0)
            return value;
        if (bitCount == 64 && bitOffset == 0)
            return ~value;

        return value ^ (~(ulong.MaxValue << bitCount) << bitOffset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint InvertBitsBase(uint value, int bitOffset, int bitCount, int maxBits)
    {
        if (bitCount == 0)
            return value;
        if (bitCount == maxBits && bitOffset == 0)
            return ~value;

        return value ^ (~(uint.MaxValue << bitCount) << bitOffset);
    }
}
