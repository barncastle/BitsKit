﻿namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    /// <summary>
    /// Writes a bit into a span of bytes, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteBitLSB(Span<byte> destination, int bitOffset, bool value)
    {
        if (bitOffset < 0 || bitOffset + 1 > destination.Length * 8)
            throw new ArgumentOutOfRangeException(nameof(bitOffset));

        int mask = 1 << (bitOffset & 7);

        if (value)
            destination[bitOffset >> 3] |= (byte)mask;
        else
            destination[bitOffset >> 3] &= (byte)~mask;
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt8LSB(Span<byte> destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8LSB(destination, bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="sbyte"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt8LSB(ref sbyte destination, int bitOffset, sbyte value, int bitCount)
    {
        WriteUInt8LSB(ref Unsafe.As<sbyte, byte>(ref destination), bitOffset, (byte)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="short"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt16LSB(Span<byte> destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16LSB(destination, bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="short"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt16LSB(ref short destination, int bitOffset, short value, int bitCount)
    {
        WriteUInt16LSB(ref Unsafe.As<short, ushort>(ref destination), bitOffset, (ushort)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="int"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt32LSB(Span<byte> destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32LSB(destination, bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="int"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt32LSB(ref int destination, int bitOffset, int value, int bitCount)
    {
        WriteUInt32LSB(ref Unsafe.As<int, uint>(ref destination), bitOffset, (uint)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="long"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt64LSB(Span<byte> destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64LSB(destination, bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="long"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteInt64LSB(ref long destination, int bitOffset, long value, int bitCount)
    {
        WriteUInt64LSB(ref Unsafe.As<long, ulong>(ref destination), bitOffset, (ulong)value, bitCount);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="nint"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
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
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="nint"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
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
    /// Writes a <paramref name="bitCount"/> sized <see cref="byte"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt8LSB(Span<byte> destination, int bitOffset, byte value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 8);

        ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
        WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="byte"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt8LSB(ref byte destination, int bitOffset, byte value, int bitCount)
    {
        ValidateArgs(8, bitOffset, bitCount, 8);

        WriteValue32(ref Unsafe.As<byte, uint>(ref destination), bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="ushort"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt16LSB(Span<byte> destination, int bitOffset, ushort value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 16);

        ref uint target = ref Unsafe.As<byte, uint>(ref destination[bitOffset >> 3]);
        WriteValue32(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="ushort"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt16LSB(ref ushort destination, int bitOffset, ushort value, int bitCount)
    {
        ValidateArgs(16, bitOffset, bitCount, 16);

        WriteValue32(ref Unsafe.As<ushort, uint>(ref destination), bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="uint"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt32LSB(Span<byte> destination, int bitOffset, uint value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 32);

        if (bitCount + (bitOffset & 7) > 32)
        {
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
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="uint"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt32LSB(ref uint destination, int bitOffset, uint value, int bitCount)
    {
        ValidateArgs(32, bitOffset, bitCount, 32);

        WriteValue32(ref destination, bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="ulong"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt64LSB(Span<byte> destination, int bitOffset, ulong value, int bitCount)
    {
        ValidateArgs(destination.Length * 8, bitOffset, bitCount, 64);

        if (bitCount + (bitOffset & 7) > 64)
        {
            ref UInt128 target = ref Unsafe.As<byte, UInt128>(ref destination[bitOffset >> 3]);
            WriteValue128(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
        else
        {
            ref ulong target = ref Unsafe.As<byte, ulong>(ref destination[bitOffset >> 3]);
            WriteValue64(ref target, bitOffset & 7, value, bitCount, BitOrder.LeastSignificant);
        }
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="ulong"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>    
    public static void WriteUInt64LSB(ref ulong destination, int bitOffset, ulong value, int bitCount)
    {
        ValidateArgs(64, bitOffset, bitCount, 64);

        WriteValue64(ref destination, bitOffset, value, bitCount, BitOrder.LeastSignificant);
    }

    /// <summary>
    /// Writes a <paramref name="bitCount"/> sized <see cref="nuint"/> into a span of bytes
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
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
    /// Writes a <paramref name="bitCount"/> sized <paramref name="value"/> to a <see cref="nuint"/>
    /// at the specified <paramref name="bitOffset"/>, as least significant bit first.
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
