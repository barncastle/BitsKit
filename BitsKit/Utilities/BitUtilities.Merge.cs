namespace BitsKit.Utilities;

public static partial class BitUtilities
{
    /// <summary>
    /// Merges the bits from two integral values according to a mask
    /// <para>
    /// Where the mask bits are 0, bits are selected from a otherwise b
    /// </para>
    /// </summary>
    /// <param name="a">Value to merge in non-masked bits</param>
    /// <param name="b">Value to merge in masked bits</param>
    /// <param name="mask"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte MergeBits(sbyte a, sbyte b, byte mask)
    {
        return (sbyte)(a ^ ((a ^ b) & mask));
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short MergeBits(short a, short b, ushort mask)
    {
        return (short)(a ^ ((a ^ b) & mask));
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MergeBits(int a, int b, uint mask)
    {
        return (int)(a ^ ((a ^ b) & mask));
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long MergeBits(long a, long b, ulong mask)
    {
        return (long)MergeBits((ulong)a, (ulong)b, mask);
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte MergeBits(byte a, byte b, byte mask)
    {
        return (byte)(a ^ ((a ^ b) & mask));
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort MergeBits(ushort a, ushort b, ushort mask)
    {
        return (ushort)(a ^ ((a ^ b) & mask));
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint MergeBits(uint a, uint b, uint mask)
    {
        return a ^ ((a ^ b) & mask);
    }

    /// <inheritdoc cref="MergeBits(sbyte, sbyte, byte)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong MergeBits(ulong a, ulong b, ulong mask)
    {
        return a ^ ((a ^ b) & mask);
    }
}
