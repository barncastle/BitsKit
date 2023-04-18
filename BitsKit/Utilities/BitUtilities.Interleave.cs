namespace BitsKit.Utilities;

public static partial class BitUtilities
{
    /// <summary>
    /// Interleaves the bits of two integers (see Morton codes)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short InterleaveBits(sbyte a, sbyte b)
    {
        return (short)InterleaveBits((byte)a, (byte)b);
    }

    /// <inheritdoc cref="InterleaveBits(sbyte, sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int InterleaveBits(short a, short b)
    {
        return (int)InterleaveBits((ushort)a, (ushort)b);
    }

    /// <inheritdoc cref="InterleaveBits(sbyte, sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long InterleaveBits(int a, int b)
    {
        return (long)InterleaveBits((uint)a, (uint)b);
    }

    /// <inheritdoc cref="InterleaveBits(sbyte, sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort InterleaveBits(byte a, byte b)
    {
        uint x = a;
        x = (x | (x << 04)) & 0x0F0F;
        x = (x | (x << 02)) & 0x3333;
        x = (x | (x << 01)) & 0x5555;

        uint y = b;
        y = (y | (y << 04)) & 0x0F0F;
        y = (y | (y << 02)) & 0x3333;
        y = (y | (y << 01)) & 0x5555;

        return (ushort)(x | (y << 1));
    }

    /// <inheritdoc cref="InterleaveBits(sbyte, sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint InterleaveBits(ushort a, ushort b)
    {
        uint x = a;
        x = (x | (x << 08)) & 0x00FF00FF;
        x = (x | (x << 04)) & 0x0F0F0F0F;
        x = (x | (x << 02)) & 0x33333333;
        x = (x | (x << 01)) & 0x55555555;

        uint y = b;
        y = (y | (y << 08)) & 0x00FF00FF;
        y = (y | (y << 04)) & 0x0F0F0F0F;
        y = (y | (y << 02)) & 0x33333333;
        y = (y | (y << 01)) & 0x55555555;

        return x | (y << 1);
    }

    /// <inheritdoc cref="InterleaveBits(sbyte, sbyte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong InterleaveBits(uint a, uint b)
    {
        ulong x = a;
        x = (x | (x << 16)) & 0x0000FFFF0000FFFFUL;
        x = (x | (x << 08)) & 0x00FF00FF00FF00FFUL;
        x = (x | (x << 04)) & 0x0F0F0F0F0F0F0F0FUL;
        x = (x | (x << 02)) & 0x3333333333333333UL;
        x = (x | (x << 01)) & 0x5555555555555555UL;

        ulong y = b;
        y = (y | (y << 16)) & 0x0000FFFF0000FFFFUL;
        y = (y | (y << 08)) & 0x00FF00FF00FF00FFUL;
        y = (y | (y << 04)) & 0x0F0F0F0F0F0F0F0FUL;
        y = (y | (y << 02)) & 0x3333333333333333UL;
        y = (y | (y << 01)) & 0x5555555555555555UL;

        return x | (y << 1);
    }
}
