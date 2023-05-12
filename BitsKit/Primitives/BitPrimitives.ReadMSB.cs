namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    /// <summary>
    /// Reads a most significant bit from a span of bytes
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static bool ReadBitMSB(ReadOnlySpan<byte> source, int bitOffset)
    {
        if (bitOffset < 0 || bitOffset >= source.Length * 8)
            throw new ArgumentOutOfRangeException(nameof(bitOffset));

        return ((source[bitOffset >> 3] >> (7 - (bitOffset & 7))) & 1) == 1;
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="sbyte"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static sbyte ReadInt8MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        int result = ReadUInt8MSB(source, bitOffset, bitCount);

        return (sbyte)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="sbyte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static sbyte ReadInt8MSB(sbyte source, int bitOffset, int bitCount)
    {
        int result = ReadUInt8MSB((byte)source, bitOffset, bitCount);

        return (sbyte)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="short"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static short ReadInt16MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        int result = ReadUInt16MSB(source, bitOffset, bitCount);

        return (short)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="short"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static short ReadInt16MSB(short source, int bitOffset, int bitCount)
    {
        int result = ReadUInt16MSB((ushort)source, bitOffset, bitCount);

        return (short)((result << (32 - bitCount)) >> (32 - bitCount));
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="int"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static int ReadInt32MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        int result = (int)ReadUInt32MSB(source, bitOffset, bitCount);

        return (result << (32 - bitCount)) >> (32 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="int"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static int ReadInt32MSB(int source, int bitOffset, int bitCount)
    {
        int result = (int)ReadUInt32MSB((uint)source, bitOffset, bitCount);

        return (result << (32 - bitCount)) >> (32 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="long"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static long ReadInt64MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        long result = (long)ReadUInt64MSB(source, bitOffset, bitCount);

        return (result << (64 - bitCount)) >> (64 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="long"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static long ReadInt64MSB(long source, int bitOffset, int bitCount)
    {
        long result = (long)ReadUInt64MSB((ulong)source, bitOffset, bitCount);

        return (result << (64 - bitCount)) >> (64 - bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="nint"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static nint ReadIntPtrMSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nint)ReadInt64MSB(source, bitOffset, bitCount);
        else
            return ReadInt32MSB(source, bitOffset, bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="nint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static nint ReadIntPtrMSB(nint source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nint)ReadInt64MSB(source, bitOffset, bitCount);
        else
            return ReadInt32MSB((int)source, bitOffset, bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="byte"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static byte ReadUInt8MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 8);

        uint value = Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        return (byte)ReadValue32(value, bitOffset & 7, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="byte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static byte ReadUInt8MSB(byte source, int bitOffset, int bitCount)
    {
        ValidateArgs(8, bitOffset, bitCount, 8);

        return (byte)ReadValue32(source, bitOffset, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="ushort"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static ushort ReadUInt16MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 16);

        uint value = Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        return (ushort)ReadValue32(value, bitOffset & 7, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="ushort"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static ushort ReadUInt16MSB(ushort source, int bitOffset, int bitCount)
    {
        ValidateArgs(16, bitOffset, bitCount, 16);

        return (ushort)ReadValue32(source, bitOffset, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="uint"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static uint ReadUInt32MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 32);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        if (bitCount + (bitOffset & 7) > 32)
            return (uint)ReadValue64(value, bitOffset & 7, bitCount, BitOrder.MostSignificant);

        return ReadValue32((uint)value, bitOffset & 7, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="uint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static uint ReadUInt32MSB(uint source, int bitOffset, int bitCount)
    {
        ValidateArgs(32, bitOffset, bitCount, 32);

        return ReadValue32(source, bitOffset, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="ulong"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static ulong ReadUInt64MSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ValidateArgs(source.Length * 8, bitOffset, bitCount, 64);

        UInt128 value = Unsafe.ReadUnaligned<UInt128>(ref Unsafe.AsRef(source[bitOffset >> 3]));

        if (bitCount + (bitOffset & 7) > 64)
            return (ulong)ReadValue128(value, bitOffset & 7, bitCount, BitOrder.MostSignificant);

        return ReadValue64((ulong)value, bitOffset & 7, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="ulong"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static ulong ReadUInt64MSB(ulong source, int bitOffset, int bitCount)
    {
        ValidateArgs(64, bitOffset, bitCount, 64);

        return ReadValue64(source, bitOffset, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, <see cref="nuint"/> from 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static nuint ReadUIntPtrMSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nuint)ReadUInt64MSB(source, bitOffset, bitCount);
        else
            return ReadUInt32MSB(source, bitOffset, bitCount);
    }

    /// <summary>
    /// Reads a <paramref name="bitCount"/> sized, most significant bit, value from 
    /// a <see cref="nuint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static nuint ReadUIntPtrMSB(nuint source, int bitOffset, int bitCount)
    {
        if (IntPtr.Size == 8)
            return (nuint)ReadUInt64MSB(source, bitOffset, bitCount);
        else
            return ReadUInt32MSB((uint)source, bitOffset, bitCount);
    }
}
