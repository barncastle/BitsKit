﻿using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A reader for retrieving packed bits from a a contiguous region of arbitrary memory
/// </summary>
public ref struct MemoryBitReader
{
    /// <inheritdoc cref="MemoryBitWriter.Position"/>
    public int Position
    {
        readonly get => _pos;
        set => _pos = value;
    }

    /// <inheritdoc cref="MemoryBitWriter.Length"/>
    public readonly long Length => (long)_buffer.Length << 3;

    private readonly ReadOnlySpan<byte> _buffer;
    private int _pos;

    public MemoryBitReader(ReadOnlySpan<byte> source)
    {
        if (source.Length >= 0x10000000)
            throw new ArgumentException("Source too large.", nameof(source));

        _buffer = source;
    }

    #region Methods

    /// <summary>Reads the next least significant bit</summary>
    public bool ReadBitLSB()
    {
        bool value = BitPrimitives.ReadBitLSB(_buffer, _pos);
        _pos++;
        return value;
    }

    /// <summary>Reads the next most significant bit</summary>
    public bool ReadBitMSB()
    {
        bool value = BitPrimitives.ReadBitMSB(_buffer, _pos);
        _pos++;
        return value;
    }

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as least significant bit first</summary>
    public sbyte ReadInt8LSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as most significant bit first</summary>
    public sbyte ReadInt8MSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as least significant bit first</summary>
    public short ReadInt16LSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as most significant bit first</summary>
    public short ReadInt16MSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as least significant bit first</summary>
    public int ReadInt32LSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as most significant bit first</summary>
    public int ReadInt32MSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as least significant bit first</summary>
    public long ReadInt64LSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as most significant bit first</summary>
    public long ReadInt64MSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as least significant bit first</summary>
    public byte ReadUInt8LSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as most significant bit first</summary>
    public byte ReadUInt8MSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as least significant bit first</summary>
    public ushort ReadUInt16LSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as most significant bit first</summary>
    public ushort ReadUInt16MSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as least significant bit first</summary>
    public uint ReadUInt32LSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as most significant bit first</summary>
    public uint ReadUInt32MSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as least significant bit first</summary>
    public ulong ReadUInt64LSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as most significant bit first</summary>
    public ulong ReadUInt64MSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Creates a new <see cref="MemoryBitReader"/> from a specified number of <see cref="byte"/>
    /// elements starting at a specified memory address
    /// </summary>
    /// <param name="source">An unmanaged pointer to memory</param>
    /// <param name="length">The number of bytes the memory contains</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified <paramref name="length"/> is negative
    /// </exception>
    public static unsafe MemoryBitReader FromPointer(void* source, int length)
    {
        return new(new ReadOnlySpan<byte>(source, length));
    }

    /// <summary>
    /// Creates a new <see cref="MemoryBitReader"/> object over a span of bytes.
    /// </summary>
    /// <param name="source">The read-only span to read</param>
    public static MemoryBitReader FromBytes(ReadOnlySpan<byte> source)
    {
        return new(source);
    }

    /// <summary>
    /// Creates a new <see cref="MemoryBitReader"/> over a regular managed object.
    /// </summary>
    /// <param name="source">The managed object to read</param>
    /// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/> contains pointers</exception>
    public static MemoryBitReader FromObject<T>(ref T source) where T : unmanaged
    {
        return new(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1)));
    }

    #endregion
}
