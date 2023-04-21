using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A reader for retrieving packed bits from a byte array
/// </summary>
public sealed class BitReader
{
    /// <inheritdoc cref="MemoryBitReader.Position"/>
    public long Position
    {
        get => ((long)_bytePos << 3) + _bitsPos;
        set
        {
            _bytePos = (int)(value >> 3);
            _bitsPos = (int)(value & 7);
        }
    }

    /// <inheritdoc cref="MemoryBitWriter.Length"/>
    public long Length => (long)_buffer.Length << 3;

    private readonly byte[] _buffer;
    private int _bytePos;
    private int _bitsPos;

    public BitReader(byte[] source)
    {
        _buffer = source;
        _bytePos = 0;
        _bitsPos = 0;
    }

    #region Methods

    /// <inheritdoc cref="MemoryBitReader.ReadBitLSB"/>
    public bool ReadBitLSB()
    {
        bool value = BitPrimitives.ReadBitLSB(_buffer.AsSpan(_bytePos), _bitsPos);
        Position++;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadBitMSB"/>
    public bool ReadBitMSB()
    {
        bool value = BitPrimitives.ReadBitMSB(_buffer.AsSpan(_bytePos), _bitsPos);
        Position++;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt8LSB"/>
    public sbyte ReadInt8LSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt8MSB"/>
    public sbyte ReadInt8MSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt16LSB"/>
    public short ReadInt16LSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt16MSB"/>
    public short ReadInt16MSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt32LSB"/>
    public int ReadInt32LSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt32MSB"/>
    public int ReadInt32MSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt64LSB"/>
    public long ReadInt64LSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadInt64MSB"/>
    public long ReadInt64MSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt8LSB"/>
    public byte ReadUInt8LSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt8MSB"/>
    public byte ReadUInt8MSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt16LSB"/>
    public ushort ReadUInt16LSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt16MSB"/>
    public ushort ReadUInt16MSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt32LSB"/>
    public uint ReadUInt32LSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt32MSB"/>
    public uint ReadUInt32MSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt64LSB"/>
    public ulong ReadUInt64LSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64LSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <inheritdoc cref="MemoryBitReader.ReadUInt64MSB"/>
    public ulong ReadUInt64MSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64MSB(_buffer.AsSpan(_bytePos), _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Creates a new <see cref="BitReader"/> from a specified number of <see cref="byte"/>
    /// elements starting at a specified memory address.
    /// </summary>
    /// <returns>A new <see cref="BitReader"/></returns>
    public static unsafe BitReader FromPointer(void* source, int length)
    {
        return new(new Span<byte>(source, length).ToArray());
    }

    /// <summary>
    /// Creates a new <see cref="BitReader"/> object over a span of bytes.
    /// </summary>
    /// <returns>A new <see cref="BitReader"/></returns>
    public static BitReader FromBytes(ReadOnlySpan<byte> source)
    {
        return new(source.ToArray());
    }

    /// <summary>
    /// Creates a new <see cref="BitReader"/> object over the entirety of a specified array of bytes.
    /// </summary>
    /// <returns>A new <see cref="BitReader"/></returns>
    public static BitReader FromBytes(byte[] source)
    {
        return new(source);
    }

    /// <summary>
    /// Creates a new <see cref="BitReader"/> over a regular managed object.
    /// </summary>
    /// <returns>A new <see cref="BitReader"/></returns>
    public static BitReader FromObject<T>(ref T source) where T : unmanaged
    {
        Span<byte> sourceBytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1));
        return new(sourceBytes.ToArray());
    }

    #endregion
}
