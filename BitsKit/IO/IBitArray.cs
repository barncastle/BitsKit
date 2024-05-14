using System.Diagnostics.CodeAnalysis;

namespace BitsKit.IO;

public interface IBitArray
{
    /// <summary>
    /// Gets or sets the bit position within the current source
    /// </summary>
    int Position { get; set; }

    /// <summary>
    /// Gets the length of the source in bits
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Sets the bit position within the current source
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    int Seek(int offset, SeekOrigin origin);

    [DoesNotReturn]
    internal static void ThrowSourceNullException() =>
        throw new ArgumentNullException("source");

    [DoesNotReturn]
    internal static void ThrowSourceSizeException() =>
        throw new ArgumentException("Source must not exceed 268435455 bytes.", "source");
}
