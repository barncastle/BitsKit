using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A writer for packing bits into a contiguous region of arbitrary memory
/// </summary>
public ref struct MemoryBitWriter
{
    /// <summary>
    /// Gets or sets the bit position within the source buffer
    /// </summary>
    public int Position
    {
        readonly get => _pos;
        set => _pos = value;
    }

    /// <summary>
    /// Gets the length of the source buffer in bits
    /// </summary>
    public readonly long Length => (long)_buffer.Length << 3;

    private readonly Span<byte> _buffer;
    private int _pos;

    /// <summary>
    /// Initialises a new instance of the <see cref="MemoryBitWriter"/> class from the specified span of bytes
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="ArgumentException"></exception>
    public MemoryBitWriter(Span<byte> source)
    {
        if (source.Length >= 0x10000000)
            throw new ArgumentException("Source too large.", nameof(source));

        _buffer = source;
    }

    #region Methods

    /// <summary>Writes the next least significant bit</summary>
    public void WriteBitLSB(bool value)
    {
        BitPrimitives.WriteBitLSB(_buffer, _pos, value);
        _pos++;
    }

    /// <summary>Writes the next most significant bit</summary>
    public void WriteBitMSB(bool value)
    {
        BitPrimitives.WriteBitMSB(_buffer, _pos, value);
        _pos++;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as least significant bit first</summary>
    public void WriteInt8LSB(sbyte value, int bitCount)
    {
        BitPrimitives.WriteInt8LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as most significant bit first</summary>
    public void WriteInt8MSB(sbyte value, int bitCount)
    {
        BitPrimitives.WriteInt8MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as least significant bit first</summary>
    public void WriteInt16LSB(short value, int bitCount)
    {
        BitPrimitives.WriteInt16LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as most significant bit first</summary>
    public void WriteInt16MSB(short value, int bitCount)
    {
        BitPrimitives.WriteInt16MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as least significant bit first</summary>
    public void WriteInt32LSB(int value, int bitCount)
    {
        BitPrimitives.WriteInt32LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as most significant bit first</summary>
    public void WriteInt32MSB(int value, int bitCount)
    {
        BitPrimitives.WriteInt32MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as least significant bit first</summary>
    public void WriteInt64LSB(long value, int bitCount)
    {
        BitPrimitives.WriteInt64LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as most significant bit first</summary>
    public void WriteInt64MSB(long value, int bitCount)
    {
        BitPrimitives.WriteInt64MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as least significant bit first</summary>
    public void WriteUInt8LSB(byte value, int bitCount)
    {
        BitPrimitives.WriteUInt8LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as most significant bit first</summary>
    public void WriteUInt8MSB(byte value, int bitCount)
    {
        BitPrimitives.WriteUInt8MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as least significant bit first</summary>
    public void WriteUInt16LSB(ushort value, int bitCount)
    {
        BitPrimitives.WriteUInt16LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as most significant bit first</summary>
    public void WriteUInt16MSB(ushort value, int bitCount)
    {
        BitPrimitives.WriteUInt16MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as least significant bit first</summary>
    public void WriteUInt32LSB(uint value, int bitCount)
    {
        BitPrimitives.WriteUInt32LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as most significant bit first</summary>
    public void WriteUInt32MSB(uint value, int bitCount)
    {
        BitPrimitives.WriteUInt32MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as least significant bit first</summary>
    public void WriteUInt64LSB(ulong value, int bitCount)
    {
        BitPrimitives.WriteUInt64LSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as most significant bit first</summary>
    public void WriteUInt64MSB(ulong value, int bitCount)
    {
        BitPrimitives.WriteUInt64MSB(_buffer, _pos, value, bitCount);
        _pos += bitCount;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Creates a new <see cref="MemoryBitWriter"/> from a specified number of <see cref="byte"/>
    /// elements starting at a specified memory address
    /// </summary>
    /// <returns>An <see cref="MemoryBitWriter"/></returns>
    public static unsafe MemoryBitWriter FromPointer(void* source, int length)
    {
        return new(new Span<byte>(source, length));
    }

    /// <summary>
    /// Creates a new <see cref="MemoryBitWriter"/> object over a span of bytes
    /// </summary>
    /// <returns>An <see cref="MemoryBitWriter"/></returns>
    public static MemoryBitWriter FromBytes(Span<byte> source)
    {
        return new(source);
    }

    /// <summary>
    /// Creates a new <see cref="MemoryBitWriter"/> over a regular managed object
    /// </summary>
    /// <returns>An <see cref="MemoryBitWriter"/></returns>
    public static MemoryBitWriter FromObject<T>(ref T source) where T : unmanaged
    {
        return new(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1)));
    }

    #endregion
}
