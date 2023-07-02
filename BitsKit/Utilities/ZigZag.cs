namespace BitsKit.Utilities;

/// <summary>
/// An implementation of ZigZag Encoding for signed integers
/// <remark>
/// <para>
/// ZigZag encoding makes the least significant bit the sign bit thus
/// making the bit count proportional to the magnitude
/// </para>
/// </remark>
/// </summary>
public static class ZigZag
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Encode(sbyte value)
    {
        return (byte)((value << 1) ^ (value >> 31));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Encode(short value)
    {
        return (ushort)((value << 1) ^ (value >> 31));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Encode(int value)
    {
        return (uint)((value << 1) ^ (value >> 31));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Encode(long value)
    {
        return (ulong)((value << 1) ^ (value >> 63));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Decode(byte value)
    {
        return (sbyte)((value >> 1) ^ -(value & 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Decode(ushort value)
    {
        return (short)((value >> 1) ^ -(value & 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Decode(uint value)
    {
        return (int)(value >> 1) ^ -(int)(value & 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Decode(ulong value)
    {
        return (long)(value >> 1) ^ -(int)(value & 0x1);
    }
}
