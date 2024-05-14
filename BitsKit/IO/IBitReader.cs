namespace BitsKit.IO;

public interface IBitReader
{
    /// <summary>Reads the next least significant bit</summary>
    bool ReadBitLSB();

    /// <summary>Reads the next most significant bit</summary>
    bool ReadBitMSB();

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as least significant bit first</summary>
    sbyte ReadInt8LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as most significant bit first</summary>
    sbyte ReadInt8MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="short"/>, as least significant bit first</summary>
    short ReadInt16LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="short"/>, as most significant bit first</summary>
    short ReadInt16MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="int"/>, as least significant bit first</summary>
    int ReadInt32LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="int"/>, as most significant bit first</summary>
    int ReadInt32MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="long"/>, as least significant bit first</summary>
    long ReadInt64LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="long"/>, as most significant bit first</summary>
    long ReadInt64MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="byte"/>, as least significant bit first</summary>
    byte ReadUInt8LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="byte"/>, as most significant bit first</summary>
    byte ReadUInt8MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="ushort"/>, as least significant bit first</summary>
    ushort ReadUInt16LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="ushort"/>, as most significant bit first</summary>
    ushort ReadUInt16MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="uint"/>, as least significant bit first</summary>
    uint ReadUInt32LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="uint"/>, as most significant bit first</summary>
    uint ReadUInt32MSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="ulong"/>, as least significant bit first</summary>
    ulong ReadUInt64LSB(int bitCount);

    /// <summary>Reads a <paramref name="bitCount"/> sized <see cref="ulong"/>, as most significant bit first</summary>
    ulong ReadUInt64MSB(int bitCount);
}
