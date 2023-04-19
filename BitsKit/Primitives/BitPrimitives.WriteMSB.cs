namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    /// <summary>
    /// Writes a bit into a span of bytes, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteBitMSB(Span<byte> destination, int bitOffset, bool value)
    {
        if (bitOffset < 0 || bitOffset + 1 > destination.Length * 8)
            throw new ArgumentOutOfRangeException(nameof(bitOffset));

        int mask = 1 << (7 - (bitOffset & 7));

        if (value)
            destination[bitOffset >> 3] |= (byte)mask;
        else
            destination[bitOffset >> 3] &= (byte)~mask;
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>   
    public static void WriteInt8MSB(Span<byte> destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8MSB(destination, bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="sbyte"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt8MSB(ref sbyte destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8MSB(ref Unsafe.As<sbyte, byte>(ref destination), bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="short"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt16MSB(Span<byte> destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16MSB(destination, bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="short"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt16MSB(ref short destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16MSB(ref Unsafe.As<short, ushort>(ref destination), bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="int"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt32MSB(Span<byte> destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32MSB(destination, bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="int"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt32MSB(ref int destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32MSB(ref Unsafe.As<int, uint>(ref destination), bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="long"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt64MSB(Span<byte> destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64MSB(destination, bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="long"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt64MSB(ref long destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64MSB(ref Unsafe.As<long, ulong>(ref destination), bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="nint"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteIntPtrMSB(Span<byte> destination, int bitOffset, nint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteInt64MSB(destination, bitOffset, value, bitCount);
        else
            WriteInt32MSB(destination, bitOffset, (int)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="nint"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>   
    public static void WriteIntPtrMSB(ref nint destination, int bitOffset, nint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteInt64MSB(ref Unsafe.As<nint, long>(ref destination), bitOffset, value, bitCount);
        else
            WriteInt32MSB(ref Unsafe.As<nint, int>(ref destination), bitOffset, (int)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="byte"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void WriteUInt8MSB(Span<byte> destination, int bitOffset, byte value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 8);

        ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
        WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="byte"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt8MSB(ref byte destination, int bitOffset, byte value, int bitCount)
    {
        ValidateArgs(8, bitOffset, bitCount, 8);

        WriteValue32(ref Unsafe.As<byte, uint>(ref destination), bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="ushort"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt16MSB(Span<byte> destination, int bitOffset, ushort value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 16);

        ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
        WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="ushort"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt16MSB(ref ushort destination, int bitOffset, ushort value, int bitCount)
    {
        ValidateArgs(16, bitOffset, bitCount, 16);

        WriteValue32(ref Unsafe.As<ushort, uint>(ref destination), bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="uint"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt32MSB(Span<byte> destination, int bitOffset, uint value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 32);

        if (bitCount + (bitOffset & 7) > 32)
        {
            ref ulong target = ref Unsafe.As<byte, ulong>(ref destination[bitOffset >> 3]);
            WriteValue64(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        }
        else
        {
            ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
            WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="uint"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void WriteUInt32MSB(ref uint destination, int bitOffset, uint value, int bitCount)
    {
        ValidateArgs(32, bitOffset, bitCount, 32);

        WriteValue32(ref destination, bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="ulong"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt64MSB(Span<byte> destination, int bitOffset, ulong value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 64);

        ref ulong target = ref Unsafe.As<byte, ulong>(ref destination[bitOffset >> 3]);

        if (bitCount + (bitOffset & 7) > 64)
            WriteValue128(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        else
            WriteValue64(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="ulong"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt64MSB(ref ulong destination, int bitOffset, ulong value, int bitCount)
    {
        ValidateArgs(64, bitOffset, bitCount, 64);

        WriteValue64(ref destination, bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="nuint"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUIntPtrMSB(Span<byte> destination, int bitOffset, nuint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteUInt64MSB(destination, bitOffset, value, bitCount);
        else
            WriteUInt32MSB(destination, bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="nuint"/>
    /// at the specified <paramref name="bitOffset"/>, as most significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>   
    public static void WriteUIntPtrMSB(ref nuint destination, int bitOffset, nuint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteUInt64MSB(ref Unsafe.As<nuint, ulong>(ref destination), bitOffset, value, bitCount);
        else
            WriteUInt32MSB(ref Unsafe.As<nuint, uint>(ref destination), bitOffset, (uint)value, bitCount);
    }
}
