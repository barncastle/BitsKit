namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    /// <summary>
    /// Writes a least significant bit to a span of bytes
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"/> 
    public static void WriteBitLSB(Span<byte> destination, int bitOffset, bool value)
    {
        int mask = 1 << (bitOffset & 7);

        if (value)
            destination[bitOffset >> 3] |= (byte)mask;
        else
            destination[bitOffset >> 3] &= (byte)~mask;
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="sbyte"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteInt8LSB(Span<byte> destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8LSB(destination, bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="sbyte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt8LSB(ref sbyte destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8LSB(ref Unsafe.As<sbyte, byte>(ref destination), bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="short"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteInt16LSB(Span<byte> destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16LSB(destination, bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="short"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt16LSB(ref short destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16LSB(ref Unsafe.As<short, ushort>(ref destination), bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="int"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteInt32LSB(Span<byte> destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32LSB(destination, bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="int"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt32LSB(ref int destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32LSB(ref Unsafe.As<int, uint>(ref destination), bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="long"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteInt64LSB(Span<byte> destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64LSB(destination, bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="long"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteInt64LSB(ref long destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64LSB(ref Unsafe.As<long, ulong>(ref destination), bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="nint"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteIntPtrLSB(Span<byte> destination, int bitOffset, nint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteInt64LSB(destination, bitOffset, value, bitCount);
        else
            WriteInt32LSB(destination, bitOffset, (int)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="nint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteIntPtrLSB(ref nint destination, int bitOffset, nint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteInt64LSB(ref Unsafe.As<nint, long>(ref destination), bitOffset, value, bitCount);
        else
            WriteInt32LSB(ref Unsafe.As<nint, int>(ref destination), bitOffset, (int)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="byte"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteUInt8LSB(Span<byte> destination, int bitOffset, byte value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 8))
            ThrowArgumentOutOfRangeException();

        if (bitCount + (bitOffset & 7) > 8)
        {
            ref ushort target = ref Unsafe.As<byte, ushort>(ref destination[bitOffset >> 3]);
            WriteValue16(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
        else
        {
            ref byte target = ref destination[bitOffset >> 3];
            WriteValue8(ref target, bitOffset & 7, value, bitCount);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="byte"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt8LSB(ref byte destination, int bitOffset, byte value, int bitCount)
    {
        if (!ValidateArgs(8, bitOffset, bitCount, 8))
            ThrowArgumentOutOfRangeException();

        WriteValue8(ref destination, bitOffset, value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="ushort"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteUInt16LSB(Span<byte> destination, int bitOffset, ushort value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 16))
            ThrowArgumentOutOfRangeException();

        if (bitCount + (bitOffset & 7) > 16)
        {
            ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
            WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
        else
        {
            ref ushort target = ref Unsafe.As<byte, ushort>(ref destination[bitOffset >> 3]);
            WriteValue16(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="ushort"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt16LSB(ref ushort destination, int bitOffset, ushort value, int bitCount)
    {
        if (!ValidateArgs(16, bitOffset, bitCount, 16))
            ThrowArgumentOutOfRangeException();

        WriteValue16(ref destination, bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="uint"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteUInt32LSB(Span<byte> destination, int bitOffset, uint value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 32))
            ThrowArgumentOutOfRangeException();

        if (bitCount + (bitOffset & 7) > 32)
        {
            // note: decomposing is ~18% faster on x86
            ref ulong target = ref Unsafe.As<byte, ulong>(ref destination[bitOffset >> 3]);
            WriteValue64(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
        else
        {
            ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
            WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="uint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt32LSB(ref uint destination, int bitOffset, uint value, int bitCount)
    {
        if (!ValidateArgs(32, bitOffset, bitCount, 32))
            ThrowArgumentOutOfRangeException();

        WriteValue32(ref destination, bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="ulong"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteUInt64LSB(Span<byte> destination, int bitOffset, ulong value, int bitCount)
    {
        if (!ValidateArgs(destination.Length * 8, bitOffset, bitCount, 64))
            ThrowArgumentOutOfRangeException();

        ref ulong target = ref Unsafe.As<byte, ulong>(ref destination[bitOffset >> 3]);

        if (bitCount + (bitOffset & 7) > 64)
            WriteValue128(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        else
            WriteValue64(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="ulong"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUInt64LSB(ref ulong destination, int bitOffset, ulong value, int bitCount)
    {
        if (!ValidateArgs(64, bitOffset, bitCount, 64))
            ThrowArgumentOutOfRangeException();

        WriteValue64(ref destination, bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <see cref="nuint"/> into 
    /// a span of bytes at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>  
    public static void WriteUIntPtrLSB(Span<byte> destination, int bitOffset, nuint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteUInt64LSB(destination, bitOffset, value, bitCount);
        else
            WriteUInt32LSB(destination, bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized, least significant bit, <paramref name="value"/>
    /// to a <see cref="nuint"/> at the specified <paramref name="bitOffset"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/> 
    public static void WriteUIntPtrLSB(ref nuint destination, int bitOffset, nuint value, int bitCount)
    {
        if (IntPtr.Size == 8)
            WriteUInt64LSB(ref Unsafe.As<nuint, ulong>(ref destination), bitOffset, value, bitCount);
        else
            WriteUInt32LSB(ref Unsafe.As<nuint, uint>(ref destination), bitOffset, (uint)value, bitCount);
    }
}
