using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A reader for retrieving packed bits from a a contiguous region of arbitrary memory
/// </summary>
public ref struct MemoryBitReader
{
    /// <inheritdoc cref="IBitArray.Position"/>
    public int Position
    {
        readonly get => _pos;
        set => _pos = value;
    }

    /// <inheritdoc cref="IBitArray.Length"/>
    public readonly int Length => _buffer.Length << 3;

    private readonly ReadOnlySpan<byte> _buffer;
    private int _pos;

    /// <summary>
    /// Initialises a new instance of the <see cref="MemoryBitReader"/> class from the specified span of bytes
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is too large.</exception>
    public MemoryBitReader(ReadOnlySpan<byte> source)
    {
        if (source.Length >= 0x10000000)
            IBitArray.ThrowSourceSizeException();

        _buffer = source;
    }

    /// <inheritdoc cref="IBitArray.Seek"/>
    public int Seek(int offset, SeekOrigin origin)
    {
        return Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length - offset,
            _ => throw new ArgumentOutOfRangeException(nameof(origin))
        };
    }

    #region Methods

    /// <inheritdoc cref="IBitReader.ReadBitLSB"/>
    public bool ReadBitLSB()
    {
        bool value = BitPrimitives.ReadBitLSB(_buffer, _pos);
        _pos++;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadBitMSB"/>
    public bool ReadBitMSB()
    {
        bool value = BitPrimitives.ReadBitMSB(_buffer, _pos);
        _pos++;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt8LSB"/>
    public sbyte ReadInt8LSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt8MSB"/>
    public sbyte ReadInt8MSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt16LSB"/>
    public short ReadInt16LSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt16MSB"/>
    public short ReadInt16MSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt32LSB"/>
    public int ReadInt32LSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt32MSB"/>
    public int ReadInt32MSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt64LSB"/>
    public long ReadInt64LSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt64MSB"/>
    public long ReadInt64MSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt8LSB"/>
    public byte ReadUInt8LSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt8MSB"/>
    public byte ReadUInt8MSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt16LSB"/>
    public ushort ReadUInt16LSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt16MSB"/>
    public ushort ReadUInt16MSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt32LSB"/>
    public uint ReadUInt32LSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt32MSB"/>
    public uint ReadUInt32MSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32MSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt64LSB"/>
    public ulong ReadUInt64LSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64LSB(_buffer, _pos, bitCount);
        _pos += bitCount;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt64MSB"/>
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
    public static unsafe MemoryBitReader FromPointer(void* source, int length)
    {
        return new(new ReadOnlySpan<byte>(source, length));
    }

    /// <summary>Creates a new <see cref="MemoryBitReader"/> object over a span of bytes</summary>
    public static MemoryBitReader FromBytes(ReadOnlySpan<byte> source)
    {
        return new(source);
    }

    /// <summary>Creates a new <see cref="MemoryBitReader"/> over a regular managed object</summary>
    public static MemoryBitReader FromObject<T>(ref T source) where T : unmanaged
    {
        return new(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1)));
    }

    #endregion
}
