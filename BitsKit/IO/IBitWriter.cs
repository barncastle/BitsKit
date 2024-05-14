namespace BitsKit.IO;

public interface IBitWriter
{
    /// <summary>Writes the next least significant bit</summary>
    void WriteBitLSB(bool value);

    /// <summary>Writes the next most significant bit</summary>
    void WriteBitMSB(bool value);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as least significant bit first</summary>
    void WriteInt8LSB(sbyte value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="sbyte"/>, as most significant bit first</summary>
    void WriteInt8MSB(sbyte value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as least significant bit first</summary>
    void WriteInt16LSB(short value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="short"/>, as most significant bit first</summary>
    void WriteInt16MSB(short value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as least significant bit first</summary>
    void WriteInt32LSB(int value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="int"/>, as most significant bit first</summary>
    void WriteInt32MSB(int value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as least significant bit first</summary>
    void WriteInt64LSB(long value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="long"/>, as most significant bit first</summary>
    void WriteInt64MSB(long value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as least significant bit first</summary>
    void WriteUInt8LSB(byte value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="byte"/>, as most significant bit first</summary>
    void WriteUInt8MSB(byte value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as least significant bit first</summary>
    void WriteUInt16LSB(ushort value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ushort"/>, as most significant bit first</summary>
    void WriteUInt16MSB(ushort value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as least significant bit first</summary>
    void WriteUInt32LSB(uint value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="uint"/>, as most significant bit first</summary>
    void WriteUInt32MSB(uint value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as least significant bit first</summary>
    void WriteUInt64LSB(ulong value, int bitCount);

    /// <summary>Writes a <paramref name="bitCount"/> sized <see cref="ulong"/>, as most significant bit first</summary>
    void WriteUInt64MSB(ulong value, int bitCount);
}
