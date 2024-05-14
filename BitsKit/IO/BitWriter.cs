using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A writer for packing bits into a byte array
/// </summary>
public sealed class BitWriter : IBitWriter, IBitArray
{
    /// <inheritdoc cref="IBitArray.Position"/>
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
    /// Initialises a new instance of the <see cref="BitWriter"/> class from the specified byte array
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is too large.</exception>
    public BitWriter(byte[] source)
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

    /// <inheritdoc cref="IBitWriter.WriteBitLSB"/>
    public void WriteBitLSB(bool value)
    {
        BitPrimitives.WriteBitLSB(_buffer, _pos, value);
        _pos++;
    }

    /// <inheritdoc cref="IBitWriter.WriteBitMSB"/>
    public void WriteBitMSB(bool value)
    {
        BitPrimitives.WriteBitMSB(_buffer, _pos, value);
        _pos++;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt8LSB"/>
    public void WriteInt8LSB(sbyte value, int bitCount)
    {
        BitPrimitives.WriteInt8LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt8MSB"/>
    public void WriteInt8MSB(sbyte value, int bitCount)
    {
        BitPrimitives.WriteInt8MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt16LSB"/>
    public void WriteInt16LSB(short value, int bitCount)
    {
        BitPrimitives.WriteInt16LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt16MSB"/>
    public void WriteInt16MSB(short value, int bitCount)
    {
        BitPrimitives.WriteInt16MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt32LSB"/>
    public void WriteInt32LSB(int value, int bitCount)
    {
        BitPrimitives.WriteInt32LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt32MSB"/>
    public void WriteInt32MSB(int value, int bitCount)
    {
        BitPrimitives.WriteInt32MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt64LSB"/>
    public void WriteInt64LSB(long value, int bitCount)
    {
        BitPrimitives.WriteInt64LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteInt64MSB"/>
    public void WriteInt64MSB(long value, int bitCount)
    {
        BitPrimitives.WriteInt64MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt8LSB"/>
    public void WriteUInt8LSB(byte value, int bitCount)
    {
        BitPrimitives.WriteUInt8LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt8MSB"/>
    public void WriteUInt8MSB(byte value, int bitCount)
    {
        BitPrimitives.WriteUInt8MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt16LSB"/>
    public void WriteUInt16LSB(ushort value, int bitCount)
    {
        BitPrimitives.WriteUInt16LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt16MSB"/>
    public void WriteUInt16MSB(ushort value, int bitCount)
    {
        BitPrimitives.WriteUInt16MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt32LSB"/>
    public void WriteUInt32LSB(uint value, int bitCount)
    {
        BitPrimitives.WriteUInt32LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt32MSB"/>
    public void WriteUInt32MSB(uint value, int bitCount)
    {
        BitPrimitives.WriteUInt32MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt64LSB"/>
    public void WriteUInt64LSB(ulong value, int bitCount)
    {
        BitPrimitives.WriteUInt64LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt64MSB"/>
    public void WriteUInt64MSB(ulong value, int bitCount)
    {
        BitPrimitives.WriteUInt64MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Creates a new <see cref="BitWriter"/> from a specified number of <see cref="byte"/>
    /// elements starting at a specified memory address
    /// </summary>
    public static unsafe BitWriter FromPointer(void* source, int length)
    {
        return new(new Span<byte>(source, length).ToArray());
    }

    /// <summary>Creates a new <see cref="BitWriter"/> object over a span of bytes</summary>
    public static BitWriter FromBytes(Span<byte> source)
    {
        return new(source.ToArray());
    }

    /// <summary>Creates a new <see cref="BitWriter"/> object over the entirety of a specified array of bytes</summary>
    public static BitWriter FromBytes(byte[] source)
    {
        return new(source);
    }

    /// <summary>Creates a new <see cref="BitWriter"/> over a regular managed object</summary>
    public static BitWriter FromObject<T>(ref T source) where T : unmanaged
    {
        Span<byte> sourceBytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1));
        return new(sourceBytes.ToArray());
    }

    #endregion
}
