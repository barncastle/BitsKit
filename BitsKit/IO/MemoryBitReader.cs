using System.Runtime.InteropServices;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A reader for retrieving packed bits from a a contiguous region of arbitrary memory
/// </summary>
public ref struct MemoryBitReader
{
    /// <summary>Gets or sets the bit position</summary>
    public long Position
    {
        get => ((long)_bytePos << 3) + _bitsPos;
        set
        {
            _bytePos = (int)(value >> 3);
            _bitsPos = (int)(value & 7);
        }
    }

    /// <summary>Gets the total bit length of the source buffer</summary>
    public readonly long Length => (long)_buffer.Length << 3;

    private readonly ReadOnlySpan<byte> _buffer;
    private int _bytePos;
    private int _bitsPos;

    public MemoryBitReader(ReadOnlySpan<byte> source)
    {
        _buffer = source;
        _bytePos = 0;
        _bitsPos = 0;
    }

    #region Methods

    /// <summary>
    /// Reads the next least significant bit.
    /// </summary>
    public bool ReadBitLSB()
    {
        bool value = BitPrimitives.ReadBitLSB(_buffer[_bytePos..], _bitsPos);
        Position++;
        return value;
    }


    /// <summary>
    /// Reads the next most significant bit.
    /// </summary>
    public bool ReadBitMSB()
    {
        bool value = BitPrimitives.ReadBitMSB(_buffer[_bytePos..], _bitsPos);
        Position++;
        return value;
    }

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as least significant bit first.</summary>
    public sbyte ReadInt8LSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as most significant bit first.</summary>
    public sbyte ReadInt8MSB(int bitCount)
    {
        sbyte value = BitPrimitives.ReadInt8MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as least significant bit first.</summary>
    public short ReadInt16LSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as most significant bit first.</summary>
    public short ReadInt16MSB(int bitCount)
    {
        short value = BitPrimitives.ReadInt16MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as least significant bit first.</summary>
    public int ReadInt32LSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as most significant bit first.</summary>
    public int ReadInt32MSB(int bitCount)
    {
        int value = BitPrimitives.ReadInt32MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as least significant bit first.</summary>
    public long ReadInt64LSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as most significant bit first.</summary>
    public long ReadInt64MSB(int bitCount)
    {
        long value = BitPrimitives.ReadInt64MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as least significant bit first.</summary>
    public byte ReadUInt8LSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as most significant bit first.</summary>
    public byte ReadUInt8MSB(int bitCount)
    {
        byte value = BitPrimitives.ReadUInt8MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as least significant bit first.</summary>
    public ushort ReadUInt16LSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as most significant bit first.</summary>
    public ushort ReadUInt16MSB(int bitCount)
    {
        ushort value = BitPrimitives.ReadUInt16MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as least significant bit first.</summary>
    public uint ReadUInt32LSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as most significant bit first.</summary>
    public uint ReadUInt32MSB(int bitCount)
    {
        uint value = BitPrimitives.ReadUInt32MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as least significant bit first.</summary>
    public ulong ReadUInt64LSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64LSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as most significant bit first.</summary>
    public ulong ReadUInt64MSB(int bitCount)
    {
        ulong value = BitPrimitives.ReadUInt64MSB(_buffer[_bytePos..], _bitsPos, bitCount);
        Position += bitCount;
        return value;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Creates a new <see cref="MemoryBitReader"/> from a specified number of <see cref="byte"/>
    /// elements starting at a specified memory address.
    /// </summary>
    /// <returns>An <see cref="MemoryBitReader"/></returns>
    public static unsafe MemoryBitReader FromPointer(void* source, int length)
    {
        return new(new ReadOnlySpan<byte>(source, length));
    }

    /// <summary>
    /// Creates a new <see cref="MemoryBitReader"/> object over a span of bytes.
    /// </summary>
    /// <returns>An <see cref="MemoryBitReader"/></returns>
    public static MemoryBitReader FromBytes(ReadOnlySpan<byte> source)
    {
        return new(source);
    }

    /// <summary>
    /// Creates a new <see cref="MemoryBitReader"/> over a regular managed object.
    /// </summary>
    /// <returns>An <see cref="MemoryBitReader"/></returns>
    public static MemoryBitReader FromObject<T>(ref T source) where T : unmanaged
    {
        return new(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref source, 1)));
    }

    #endregion
}
