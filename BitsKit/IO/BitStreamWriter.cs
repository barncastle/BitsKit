using System.ComponentModel;
using BitsKit.Primitives;

namespace BitsKit.IO;

/// <summary>
/// A writer for packing bits into a stream
/// <remarks>
/// <para>
/// Note: Writing mid-place bits is supported but requires
/// the underlying stream to be both readable and seekable.<br/>
/// If the stream does not support seeking or reading, the 
/// operation will overwrite any existing data.
/// </para> 
/// </remarks>
/// </summary>
public sealed class BitStreamWriter : IDisposable
{
    /// <summary>
    /// Gets or sets the bit position of the current stream
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public long Position
    {
        get => (_bytePos << 3) + _bitsPos;
        set => SetPosition(value);
    }

    /// <summary>
    /// Gets the length of the stream in bits
    /// </summary>
    public long Length => _stream.Length << 3;

    private Stream _stream;
    private byte _buffer;
    private long _bytePos;
    private int _bitsPos;

    private readonly bool _canRead; // TODO: should the writer error instead?
    private readonly bool _leaveOpen;
    private bool _disposed;

    public BitStreamWriter(Stream source) : this(source, false)
    {
    }

    public BitStreamWriter(Stream source, bool leaveOpen)
    {
        if (!source.CanWrite)
            throw new NotSupportedException("Stream does not support writing.");

        _stream = source;
        _leaveOpen = leaveOpen;
        _canRead = source.CanRead && source.CanSeek;
        _bytePos = source.Position;

        ResetBuffer();
    }

    /// <summary>
    /// Sets the bit position within the current stream
    /// </summary>
    /// <exception cref="InvalidEnumArgumentException"></exception>
    public long Seek(long offset, SeekOrigin origin)
    {
        return Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => Length - offset,
            _ => throw new InvalidEnumArgumentException(nameof(origin))
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
            _bytePos++;
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

    /// <inheritdoc cref="MemoryBitWriter.WriteBitLSB"/>
    public void WriteBitLSB(bool value)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteBitLSB(buffer, _bitsPos, value);
        InternalWrite(buffer, 1);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteBitMSB"/>
    public void WriteBitMSB(bool value)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteBitMSB(buffer, _bitsPos, value);
        InternalWrite(buffer, 1);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt8LSB"/>
    public void WriteInt8LSB(sbyte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt8LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt8MSB"/>
    public void WriteInt8MSB(sbyte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt8MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt16LSB"/>
    public void WriteInt16LSB(short value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt16LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt16MSB"/>
    public void WriteInt16MSB(short value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt16MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt32LSB"/>
    public void WriteInt32LSB(int value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt32LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt32MSB"/>
    public void WriteInt32MSB(int value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt32MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt64LSB"/>
    public void WriteInt64LSB(long value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt64LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteInt64MSB"/>
    public void WriteInt64MSB(long value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteInt64MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt8LSB"/>
    public void WriteUInt8LSB(byte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt8LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt8MSB"/>
    public void WriteUInt8MSB(byte value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[2];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt8MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt16LSB"/>
    public void WriteUInt16LSB(ushort value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt16LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt16MSB"/>
    public void WriteUInt16MSB(ushort value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[3];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt16MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt32LSB"/>
    public void WriteUInt32LSB(uint value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt32LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt32MSB"/>
    public void WriteUInt32MSB(uint value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[5];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt32MSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt64LSB"/>
    public void WriteUInt64LSB(ulong value, int bitCount)
    {
        Span<byte> buffer = stackalloc byte[9];
        PopulateWriteBuffer(buffer);

        BitPrimitives.WriteUInt64LSB(buffer, _bitsPos, value, bitCount);
        InternalWrite(buffer, bitCount);
    }

    /// <inheritdoc cref="MemoryBitWriter.WriteUInt64MSB"/>
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
        if (_canRead && _bytePos < _stream.Length)
        {
            _buffer = (byte)_stream.ReadByte();
            _stream.Position -= 1;
        }
    }

    private void PopulateWriteBuffer(Span<byte> buffer)
    {
        // if this is a mid-stream write then populate the buffer
        // with the existing data to allow writing in-place
        if (_canRead && _bytePos < _stream.Length)
        {
            int read = _stream.Read(buffer);
            _stream.Position -= read;
        }

        // prepend any buffered bits
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

        Position += bitCount;

        // buffer any unprocessed bits
        if (_bitsPos != 0)
            _buffer = buffer[writeLen];
    }

    private void SetPosition(long position)
    {
        if (position < 0)
            throw new ArgumentOutOfRangeException(nameof(position));

        // calculate offsets
        _bytePos = position >> 3;
        _bitsPos = (int)(position & 7);

        // check if this is a Seek operation
        if (_bytePos != _stream.Position)
        {
            // write any buffered bits
            Flush();
            // update the stream position
            _stream.Position = _bytePos;
            // and repopulate the buffer
            ResetBuffer();
        }
    }
}
