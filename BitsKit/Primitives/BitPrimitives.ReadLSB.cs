namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    /// <summary>
    /// Reads a bit from a span of bytes, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static bool ReadBitLSB(ReadOnlySpan<byte> source, int bitOffset)
    {
        if (bitOffset < 0 || bitOffset + 1 > source.Length * 8)
            throw new ArgumentOutOfRangeException(nameof(bitOffset));

        return ((source[bitOffset >> 3] >> (bitOffset & 7)) & 1) == 1;
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="sbyte"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static sbyte ReadInt8LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        int result = ReadUInt8LSB(source, bitOffset, bitCount);

        return (sbyte)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="sbyte"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static sbyte ReadInt8LSB(sbyte source, int bitOffset, int bitCount)
    {
        int result = ReadUInt8LSB((byte)source, bitOffset, bitCount);

        return (sbyte)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="short"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static short ReadInt16LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        int result = ReadUInt16LSB(source, bitOffset, bitCount);

        return (short)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="short"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static short ReadInt16LSB(short source, int bitOffset, int bitCount)
    {
        int result = ReadUInt16LSB((ushort)source, bitOffset, bitCount);

        return (short)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="int"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static int ReadInt32LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        int result = (int)ReadUInt32LSB(source, bitOffset, bitCount);

        return (result << (32 - bitCount)) >> (32 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="int"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static int ReadInt32LSB(int source, int bitOffset, int bitCount)
    {
        int result = (int)ReadUInt32LSB((uint)source, bitOffset, bitCount);

        return (result << (32 - bitCount)) >> (32 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="long"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static long ReadInt64LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        long result = (long)ReadUInt64LSB(source, bitOffset, bitCount);

        return (result << (64 - bitCount)) >> (64 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="long"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static long ReadInt64LSB(long source, int bitOffset, int bitCount)
    {
        long result = (long)ReadUInt64LSB((ulong)source, bitOffset, bitCount);

        return (result << (64 - bitCount)) >> (64 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="nint"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static nint ReadIntPtrLSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nint)ReadInt64LSB(source, bitOffset, bitCount);
        else
            return ReadInt32LSB(source, bitOffset, bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="nint"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static nint ReadIntPtrLSB(nint source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nint)ReadInt64LSB(source, bitOffset, bitCount);
        else
            return ReadInt32LSB((int)source, bitOffset, bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="byte"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static byte ReadUInt8LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 8);

        uint value = Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        return (byte)ReadValue32(value, bitOffset & 7, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="byte"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static byte ReadUInt8LSB(byte source, int bitOffset, int bitCount)
    {
        ValidateArgs(8, bitOffset, bitCount, 8);

        return (byte)ReadValue32(source, bitOffset, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="ushort"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static ushort ReadUInt16LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 16);

        uint value = Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        return (ushort)ReadValue32(value, bitOffset & 7, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="ushort"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static ushort ReadUInt16LSB(ushort source, int bitOffset, int bitCount)
    {
        ValidateArgs(16, bitOffset, bitCount, 16);

        return (ushort)ReadValue32(source, bitOffset, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="uint"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static uint ReadUInt32LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 32);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        if (bitCount + (bitOffset & 7) <= 32)
            return ReadValue32((uint)value, bitOffset & 7, bitCount, BitOrder.LeastSignificant);
        else
            return (uint)ReadValue64(value, bitOffset & 7, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="uint"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static uint ReadUInt32LSB(uint source, int bitOffset, int bitCount)
    {
        ValidateArgs(32, bitOffset, bitCount, 32);

        return ReadValue32(source, bitOffset, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="ulong"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static ulong ReadUInt64LSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 64);

        UInt128 value = Unsafe.ReadUnaligned<UInt128>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        if (bitCount + (bitOffset & 7) > 64)
            return (ulong)ReadValue128(value, bitOffset & 7, bitCount, BitOrder.LeastSignificant);

        return ReadValue64((ulong)value, bitOffset & 7, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="ulong"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static ulong ReadUInt64LSB(ulong source, int bitOffset, int bitCount)
    {
        ValidateArgs(64, bitOffset, bitCount, 64);

        return ReadValue64(source, bitOffset, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized <see cref="nuint"/> from a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static nuint ReadUIntPtrLSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nuint)ReadUInt64LSB(source, bitOffset, bitCount);
        else
            return ReadUInt32LSB(source, bitOffset, bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized value from a <see cref="nuint"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static nuint ReadUIntPtrLSB(nuint source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nuint)ReadUInt64LSB(source, bitOffset, bitCount);
        else
            return ReadUInt32LSB((uint)source, bitOffset, bitCount);
    }
}
