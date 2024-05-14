using System.Diagnostics.CodeAnalysis;

namespace BitsKit.IO;

public interface IBitStream : IDisposable
{
    /// <summary>
    /// Gets or sets the bit position within the current stream
    /// </summary>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    long Position { get; set; }

    /// <summary>
    /// Gets the length of the stream in bits
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    long Length { get; }

    /// <summary>
    /// Sets the bit position within the current stream
    /// </summary>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    long Seek(long offset, SeekOrigin origin);

    [DoesNotReturn]
    internal static void ThrowSourceNullException() =>
        throw new ArgumentNullException("source");

    [DoesNotReturn]
    internal static void ThrowNegativePositionException() =>
        throw new IOException("An attempt was made to move the position before the beginning of the stream.");
}
