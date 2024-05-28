using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A reader for retrieving packed bits from a stream
/// </summary>
public sealed class BitStreamReader : IBitReader, IBitStream
{
    /// <inheritdoc cref="IBitStream.Position"/>
    public long Position
    {
        get => (_stream.Position << 3) - ((8 - _bitsPos) & 7);
        set => SetPosition(value);
    }

    /// <inheritdoc cref="IBitStream.Length"/>
    public long Length => _stream.Length << 3;

    private Stream _stream;
    private byte[] _buffer;
    private int _bitsPos;
    private int _buffLen;

    private readonly bool _leaveOpen;
    private bool _disposed;

    /// <summary>
    /// Initialises a new instance of the <see cref="BitStreamReader"/> class using the specific stream
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="NotSupportedException"></exception>
    public BitStreamReader(Stream source) : this(source, false)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="BitStreamReader"/> class using the specific stream
    /// and optionally leaves the stream open
    /// </summary>
    /// <param name="source"></param>
    /// <param name="leaveOpen"></param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when <paramref name="source"/> does not support reading.</exception>
    public BitStreamReader(Stream source, bool leaveOpen)
    {
        if (source is null)
            IBitStream.ThrowSourceNullException();

        if (!source.CanRead)
            throw new NotSupportedException("Stream does not support reading.");

        _stream = source;
        _buffer = new byte[9];
        _buffLen = 1;
        _leaveOpen = leaveOpen;
    }

    /// <inheritdoc cref="IBitStream.Seek"/>
    public long Seek(long offset, SeekOrigin origin)
    {
        return Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length - offset,
            _ => throw new ArgumentOutOfRangeException(nameof(origin))
        };
    }

    /// <inheritdoc cref="BitStreamWriter.Dispose"/>
    public void Dispose()
    {
        if (!_disposed)
        {
            if (!_leaveOpen)
                _stream.Dispose();

            _stream = null!;
            _buffer = null!;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    #region Methods

    /// <inheritdoc cref="IBitReader.ReadBitLSB"/>
    public bool ReadBitLSB()
    {
        if (_bitsPos == 0)
            _buffLen = _stream.Read(_buffer, 0, 1);

        bool value = BitPrimitives.ReadBitLSB(_buffer, _bitsPos);
        _bitsPos = (_bitsPos + 1) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadBitMSB"/>
    public bool ReadBitMSB()
    {
        if (_bitsPos == 0)
            _buffLen = _stream.Read(_buffer, 0, 1);

        bool value = BitPrimitives.ReadBitMSB(_buffer, _bitsPos);
        _bitsPos = (_bitsPos + 1) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt8LSB"/>
    public sbyte ReadInt8LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        sbyte value = BitPrimitives.ReadInt8LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt8MSB"/>
    public sbyte ReadInt8MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        sbyte value = BitPrimitives.ReadInt8MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt16LSB"/>
    public short ReadInt16LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        short value = BitPrimitives.ReadInt16LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt16MSB"/>
    public short ReadInt16MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        short value = BitPrimitives.ReadInt16MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt32LSB"/>
    public int ReadInt32LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        int value = BitPrimitives.ReadInt32LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt32MSB"/>
    public int ReadInt32MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        int value = BitPrimitives.ReadInt32MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt64LSB"/>
    public long ReadInt64LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        long value = BitPrimitives.ReadInt64LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadInt64MSB"/>
    public long ReadInt64MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        long value = BitPrimitives.ReadInt64MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt8LSB"/>
    public byte ReadUInt8LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        byte value = BitPrimitives.ReadUInt8LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt8MSB"/>
    public byte ReadUInt8MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        byte value = BitPrimitives.ReadUInt8MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt16LSB"/>
    public ushort ReadUInt16LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        ushort value = BitPrimitives.ReadUInt16LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt16MSB"/>
    public ushort ReadUInt16MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        ushort value = BitPrimitives.ReadUInt16MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt32LSB"/>
    public uint ReadUInt32LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        uint value = BitPrimitives.ReadUInt32LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt32MSB"/>
    public uint ReadUInt32MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        uint value = BitPrimitives.ReadUInt32MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt64LSB"/>
    public ulong ReadUInt64LSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        ulong value = BitPrimitives.ReadUInt64LSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    /// <inheritdoc cref="IBitReader.ReadUInt64MSB"/>
    public ulong ReadUInt64MSB(int bitCount)
    {
        PopulateBuffer(bitCount);

        ulong value = BitPrimitives.ReadUInt64MSB(_buffer, _bitsPos, bitCount);
        _bitsPos = (_bitsPos + bitCount) & 7;
        return value;
    }

    #endregion

    private void PopulateBuffer(int bitCount)
    {
        // calculate the number of whole bytes to buffer
        int bitsBuffered = (8 - _bitsPos) & 7;
        int bytesRequired = (bitCount - bitsBuffered + 7) >> 3;

        // preserve the last byte which may contain unread bits
        if (_buffLen != 1)
            _buffer[0] = _buffer[_buffLen - 1];

        if (bytesRequired != 0)
        {
            if (_bitsPos != 0)
                _buffLen = _stream.Read(_buffer, 1, bytesRequired) + 1;
            else
                _buffLen = _stream.Read(_buffer, 0, bytesRequired);
        }
    }

    private void SetPosition(long position)
    {
        if (position < 0)
            IBitStream.ThrowNegativePositionException();

        // calculate offsets
        int bytePos = (int)(position >> 3);
        int bitsPos = (int)(position & 7);

        // check if this is a Seek operation
        if ((position + 7) >> 3 != _stream.Position)
        {
            // update the stream position
            _stream.Position = bytePos;

            // buffer the current byte if not aligned
            if (bitsPos != 0)
                _buffLen = _stream.Read(_buffer, 0, 1);
        }

        _bitsPos = bitsPos;
    }
}
