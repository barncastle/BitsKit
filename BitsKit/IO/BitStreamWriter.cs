using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A writer for packing bits into a stream
/// <remarks>
/// <para>
/// Note: Writing mid-stream bits requires the source
/// stream to be readable to allow buffering
/// </para> 
/// </remarks>
/// </summary>
public sealed class BitStreamWriter : IBitWriter, IBitStream
{
    /// <inheritdoc cref="IBitStream.Position"/>
    public long Position
    {
        get => (_stream.Position << 3) + _bitsPos;
        set => SetPosition(value);
    }

    /// <inheritdoc cref="IBitStream.Length"/>
    public long Length => _stream.Length << 3;

    private Stream _stream;
    private byte _buffer;
    private int _bitsPos;

    private readonly bool _leaveOpen;
    private bool _disposed;

    /// <summary>
    /// Initialises a new instance of the <see cref="BitStreamWriter"/> class using the specific stream
    /// </summary>
    /// <param name="source"></param>
    /// <exception cref="NotSupportedException"></exception>
    public BitStreamWriter(Stream source) : this(source, false)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="BitStreamWriter"/> class using the specific stream
    /// and optionally leaves the stream open
    /// </summary>
    /// <param name="source"></param>
    /// <param name="leaveOpen"></param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when <paramref name="source"/> does not support writing.</exception>
    public BitStreamWriter(Stream source, bool leaveOpen)
    {
        if (source is null)
            IBitStream.ThrowSourceNullException();

        if (!source.CanWrite)
            throw new NotSupportedException("Stream does not support writing.");

        _stream = source;
        _leaveOpen = leaveOpen;
        
        ResetBuffer();
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

    /// <summary>
    /// Clears all buffers for this stream and causes any buffered data 
    /// to be written to the underlying stream
    /// </summary>
    public void Flush()
    {
        // write any buffered bits
        if (_bitsPos != 0)
        {
            _stream.WriteByte(_buffer);
            _bitsPos = 0;
            ResetBuffer();
        }

        _stream.Flush();
    }

    /// <summary>
    /// Closes this stream and releases all associated resources
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            Flush();

            if (!_leaveOpen)
                _stream.Dispose();

            _stream = null!;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    #region Methods

    /// <inheritdoc cref="IBitWriter.WriteBitLSB"/>
    public void WriteBitLSB(bool value)
    {
        Span<byte> buffer = stackalloc byte[1];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteBitLSB(buffer, _bitsPos, value);
        InternalWrite(buffer, 1);
    }

    /// <inheritdoc cref="IBitWriter.WriteBitMSB"/>
    public void WriteBitMSB(bool value)
    {
        Span<byte> buffer = stackalloc byte[1];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteBitMSB(buffer, _bitsPos, value);
        InternalWrite(buffer, 1);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt8LSB"/>
    public void WriteInt8LSB(sbyte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt8LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt8MSB"/>
    public void WriteInt8MSB(sbyte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt8MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt16LSB"/>
    public void WriteInt16LSB(short value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt16LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt16MSB"/>
    public void WriteInt16MSB(short value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt16MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt32LSB"/>
    public void WriteInt32LSB(int value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt32LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt32MSB"/>
    public void WriteInt32MSB(int value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt32MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt64LSB"/>
    public void WriteInt64LSB(long value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt64LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteInt64MSB"/>
    public void WriteInt64MSB(long value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt64MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt8LSB"/>
    public void WriteUInt8LSB(byte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt8LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt8MSB"/>
    public void WriteUInt8MSB(byte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt8MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt16LSB"/>
    public void WriteUInt16LSB(ushort value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt16LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt16MSB"/>
    public void WriteUInt16MSB(ushort value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt16MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt32LSB"/>
    public void WriteUInt32LSB(uint value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt32LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt32MSB"/>
    public void WriteUInt32MSB(uint value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt32MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt64LSB"/>
    public void WriteUInt64LSB(ulong value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt64LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="IBitWriter.WriteUInt64MSB"/>
    public void WriteUInt64MSB(ulong value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt64MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    #endregion

    private void ResetBuffer()
    {
        _buffer = 0;

        // if this is mid-stream, buffer the next byte
        if (_stream.Position < _stream.Length)
        {
            _buffer = (byte)_stream.ReadByte();
            _stream.Position -= 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void PopulateWriteBuffer(Span<byte> buffer)
    {
        // if this is a mid-stream write then populate the buffer
        // with the existing data to allow writing in-place
        if (_stream.Position < _stream.Length)
        {
            int read = _stream.Read(buffer);
            _stream.Position -= read;
        }

        // preserve any unwritten bits
        if (_bitsPos != 0)
            buffer[0] = _buffer;
    }

    private void InternalWrite(Span<byte> buffer, int bitCount)
    {
        // number of whole bytes to write
        int writeLen = (bitCount + _bitsPos) >> 3;

        // write all the whole bytes
        if (writeLen != 0)
            _stream.Write(buffer[..writeLen]);

        _bitsPos = (_bitsPos + bitCount) & 7;

        // preserve any unwritten bits
        if (_bitsPos != 0)
            _buffer = buffer[writeLen];
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
            // write any buffered bits
            Flush();
            // update the stream position
            _stream.Position = bytePos;
            // and repopulate the buffer
            ResetBuffer();
        }

        _bitsPos = bitsPos;
    }
}
