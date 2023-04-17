using System.Buffers.Binary;

[assembly: InternalsVisibleTo("BitsKit.Benchmarks")]
[assembly: InternalsVisibleTo("BitsKit.Tests")]

namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ValidateArgs(int availableBits, int bitOffset, int bitCount, int maxBits)
    {
        // check the amount of bits to read is valid
        if (bitCount < 0 || bitCount > maxBits)
            throw new ArgumentOutOfRangeException(nameof(bitCount));
        // check the start/end offsets aren't out of bounds
        if (bitOffset < 0 || bitOffset + bitCount > availableBits)
            throw new ArgumentOutOfRangeException(nameof(bitOffset));
    }

    private static uint ReadValue32(uint source, int bitOffset, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return 0;

        if (bitOrder == BitOrder.LeastSignificant)
            return source << (32 - bitCount - bitOffset) >> (32 - bitCount);
        else
            return BinaryPrimitives.ReverseEndianness(source) << bitOffset >> (32 - bitCount);
    }

    private static ulong ReadValue64(ulong source, int bitOffset, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return 0;

        if (bitOrder == BitOrder.LeastSignificant)
            return source << (64 - bitCount - bitOffset) >> (64 - bitCount);
        else
            return BinaryPrimitives.ReverseEndianness(source) << bitOffset >> (64 - bitCount);
    }

    private static UInt128 ReadValue128(UInt128 source, int bitOffset, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return 0;

        if (bitOrder == BitOrder.LeastSignificant)
            return source << (128 - bitCount - bitOffset) >> (128 - bitCount);
        else
            return UInt128Helper.ReverseEndianness(source) << bitOffset >> (128 - bitCount);
    }

    private static void WriteValue32(ref uint destination, int bitOffset, uint value, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return;

        // create the mask
        uint mask = uint.MaxValue >> (32 - bitCount);

        // truncate the value
        value &= mask;

        if (bitOrder == BitOrder.LeastSignificant)
        {
            // align to the correct bit
            mask <<= bitOffset;
            value <<= bitOffset;
        }
        else
        {
            /// align the mask and reverse it's bit order
            mask = ReverseBitOrder(mask << bitOffset);
            // align the value to the opposite significant bit and reverse it's endianness
            value = BinaryPrimitives.ReverseEndianness(value << (32 - bitCount - bitOffset));
        }

        destination &= ~mask;
        destination |= value;
    }

    private static void WriteValue64(ref ulong destination, int bitOffset, ulong value, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return;

        // create the mask
        ulong mask = ulong.MaxValue >> (64 - bitCount);

        // truncate the value
        value &= mask;

        if (bitOrder == BitOrder.LeastSignificant)
        {
            // align to the correct bit
            mask <<= bitOffset;
            value <<= bitOffset;
        }
        else
        {
            // align the mask and reverse it's bit order
            mask = ReverseBitOrder(mask << bitOffset);
            // align the value to the opposite significant bit and reverse it's endianness
            value = BinaryPrimitives.ReverseEndianness(value << (64 - bitCount - bitOffset));
        }

        destination &= ~mask;
        destination |= value;
    }

    private static void WriteValue128(ref UInt128 destination, int bitOffset, UInt128 value, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return;

        // create the mask
        UInt128 mask = UInt128.MaxValue >> (128 - bitCount);

        // truncate the value
        value &= mask;

        if (bitOrder == BitOrder.LeastSignificant)
        {
            // align to the correct bit
            mask <<= bitOffset;
            value <<= bitOffset;
        }
        else
        {
            // align the mask and reverse it's bit order
            mask = ReverseBitOrder(mask << bitOffset);
            // align the value to the opposite significant bit and reverse it's endianness
            value = UInt128Helper.ReverseEndianness(value << (128 - bitCount - bitOffset));
        }

        destination &= ~mask;
        destination |= value;
    }
}
