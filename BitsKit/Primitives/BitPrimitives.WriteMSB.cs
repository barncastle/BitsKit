namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    /// <summary>
    /// Writes a most significant bit to a span of bytes
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"/> 
    public static void WriteBitMSB(Span<byte> destination, int bitOffset, bool value)
    {
        int mask = 0x80 >> (bitOffset & 7);

        if (value)
            destination[bitOffset >> 3] |= (byte)mask;
        else
            destination[bitOffset >> 3] &= (byte)~mask;
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="sbyte"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt8MSB(Span<byte> destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8MSB(destination, bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="sbyte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt8MSB(ref sbyte destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8MSB(ref Unsafe.As<sbyte, byte>(ref destination), bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="short"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt16MSB(Span<byte> destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16MSB(destination, bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="short"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt16MSB(ref short destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16MSB(ref Unsafe.As<short, ushort>(ref destination), bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="int"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt32MSB(Span<byte> destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32MSB(destination, bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="int"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt32MSB(ref int destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32MSB(ref Unsafe.As<int, uint>(ref destination), bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="long"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt64MSB(Span<byte> destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64MSB(destination, bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="byte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt64MSB(ref long destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64MSB(ref Unsafe.As<long, ulong>(ref destination), bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="nint"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
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
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="nint"/> at the specified <paramref name="bitOffset"/>
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
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="byte"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt8MSB(Span<byte> destination, int bitOffset, byte value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 8))
            ThrowArgumentOutOfRangeException();

        if (bitCount + (bitOffset & 7) > 8)
        {
            ref ushort target = ref Unsafe.As<byte, ushort>(ref destination[bitOffset >> 3]);
            WriteValue16(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        }
        else
        {
            ref byte target = ref destination[bitOffset >> 3];
            WriteValue8(ref target, 8 - bitCount - (bitOffset & 7), value, bitCount);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="byte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt8MSB(ref byte destination, int bitOffset, byte value, int bitCount)
    {
        if (!ValidateArgs(8, bitOffset, bitCount, 8))
            ThrowArgumentOutOfRangeException();

        WriteValue8(ref destination, 8 - bitCount - bitOffset, value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="ushort"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt16MSB(Span<byte> destination, int bitOffset, ushort value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 16))
            ThrowArgumentOutOfRangeException();

        if (bitCount + (bitOffset & 7) > 16)
        {
            ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
            WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        }
        else
        {
            ref ushort target = ref Unsafe.As<byte, ushort>(ref destination[bitOffset >> 3]);
            WriteValue16(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="ushort"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt16MSB(ref ushort destination, int bitOffset, ushort value, int bitCount)
    {
        if (!ValidateArgs(16, bitOffset, bitCount, 16))
            ThrowArgumentOutOfRangeException();

        WriteValue16(ref destination, bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="uint"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt32MSB(Span<byte> destination, int bitOffset, uint value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 32))
            ThrowArgumentOutOfRangeException();

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
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="uint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt32MSB(ref uint destination, int bitOffset, uint value, int bitCount)
    {
        if (!ValidateArgs(32, bitOffset, bitCount, 32))
            ThrowArgumentOutOfRangeException();

        WriteValue32(ref destination, bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="ulong"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt64MSB(Span<byte> destination, int bitOffset, ulong value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 64))
            ThrowArgumentOutOfRangeException();

        ref ulong target = ref Unsafe.As<byte, ulong>(ref destination[bitOffset >> 3]);

        if (bitCount + (bitOffset & 7) > 64)
            WriteValue128(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
        else
            WriteValue64(ref target, bitOffset & 7, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="ulong"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt64MSB(ref ulong destination, int bitOffset, ulong value, int bitCount)
    {
        if (!ValidateArgs(64, bitOffset, bitCount, 64))
            ThrowArgumentOutOfRangeException();

        WriteValue64(ref destination, bitOffset, value, bitCount, BitOrder.MostSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <see cref="nuint"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
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
    /// Writes a <paramref name="bitCount"/> sized, most significant bit, <paramref name="value"/>
    /// to a <see cref="nuint"/> at the specified <paramref name="bitOffset"/>
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
