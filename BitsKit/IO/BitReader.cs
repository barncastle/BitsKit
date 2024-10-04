using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A reader for retrieving packed bits from a byte array
/// </summary>
public sealed class BitReader : IBitReader, IBitArray
{
    /// <inheritdoc cref="IBitReader.Position"/>
    public int Position
    {
        get => _pos;
        set => _pos = value;
    }

    /// <inheritdoc cref="IBitArray.Length"/>
    public int Length => _buffer.Length << 3;

    private readonly byte[] _buffer;
    private int _pos;

    /// <summary>
    /// Initialises a new instance of the <see cref="BitReader"/> class from the specified byte array
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is too large.</exception>
    public BitReader(byte[] source)
    {
        if (source is null)
            IBitArray.ThrowSourceNullException();

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
    /// Creates a new <see cref="BitReader"/> from a specified number of <see cref="byte"/>
    /// elements starting at a specified memory address
    /// </summary>
    public static unsafe BitReader FromPointer(void* source, int length)
    {
        return new(new Span<byte>(source, length).ToArray());
    }

    /// <summary>Creates a new <see cref="BitReader"/> object over a span of bytes</summary>
    public static BitReader FromBytes(ReadOnlySpan<byte> source)
    {
        return new(source.ToArray());
    }

    /// <summary>Creates a new <see cref="BitReader"/> object over the entirety of a specified array of bytes</summary>
    public static BitReader FromBytes(byte[] source)
    {
        return new(source);
    }

    /// <summary>Creates a new <see cref="BitReader"/> from a regular managed object</summary>
    public static BitReader FromObject<T>(T source) where T : unmanaged
    {
        Span<byte> sourceBytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1));
        return new(sourceBytes.ToArray());
    }

    #endregion
}
