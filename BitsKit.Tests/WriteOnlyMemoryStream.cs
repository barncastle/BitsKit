using System;
using System.IO;

namespace BitsKit.Tests;

internal class WriteOnlyMemoryStream : MemoryStream
{
    public override bool CanRead => false;

    public override int Read(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException("Stream does not support reading");

    public override int Read(Span<byte> buffer) =>
        throw new NotSupportedException("Stream does not support reading");

    public override int ReadByte() =>
        throw new NotSupportedException("Stream does not support reading");

    public byte[] GetBuffer(int length) => GetBuffer()[..length];
}
