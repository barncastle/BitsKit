using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;

[assembly: InternalsVisibleTo("BitsKit.Benchmarks")]
[assembly: InternalsVisibleTo("BitsKit.Tests")]

namespace BitsKit.Primitives;

public static partial class BitPrimitives
{
    [DoesNotReturn]
    private static void ThrowArgumentOutOfRangeException() => throw new ArgumentOutOfRangeException();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ValidateArgs(int availableBits, int bitOffset, int bitCount, int maxBits)
    {
        // check the amount of bits to read is valid
        if (bitCount < 0 || bitCount > maxBits)
            return false;
        // check the start/end offsets aren't out of bounds
        if (bitOffset < 0 || bitOffset + bitCount > availableBits)
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetMask(int size) => (1 << size) - 1;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteValue8(ref byte destination, int bitShift, int value, int bitCount)
    {
        if (bitCount == 0)
            return;

        destination = (byte)((destination & ~(GetMask(bitCount) << bitShift)) | ((value & GetMask(bitCount)) << bitShift));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteValue16(ref ushort destination, int bitOffset, int value, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return;

        // decompose into two byte refs
        ref byte destLo = ref Unsafe.As<ushort, byte>(ref destination);
        ref byte destHi = ref Unsafe.Add(ref destLo, 1);

        int bitMask = GetMask(bitCount);

        if (bitOrder == BitOrder.LeastSignificant)
        {
            value = (value & bitMask) << bitOffset;
            bitMask <<= bitOffset;

            destLo = (byte)((destLo & ~bitMask) | value);
            destHi = (byte)((destHi & ~(bitMask >> 8)) | (value >> 8));
        }
        else
        {
            value = (value & bitMask) << (16 - bitCount - bitOffset);
            bitMask <<= 16 - bitCount - bitOffset;

            destLo = (byte)((destLo & ~(bitMask >> 8)) | (value >> 8));
            destHi = (byte)((destHi & ~bitMask) | value);
        }
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
            // align the mask and reverse it's bit order
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

    private static void WriteValue128(ref ulong destination, int bitOffset, ulong value, int bitCount, BitOrder bitOrder)
    {
        // benchmarking shows that decomposing into ulong writes is faster

        int countHi = 64 - bitOffset;
        int countLo = bitCount - countHi;

        if (bitOrder == BitOrder.LeastSignificant)
        {
            WriteValue64(ref destination, bitOffset, value, countHi, bitOrder);
            WriteValue64(ref Unsafe.Add(ref destination, 1), 0, value >> countHi, countLo, bitOrder);
        }
        else
        {
            WriteValue64(ref destination, bitOffset, value >> countLo, countHi, bitOrder);
            WriteValue64(ref Unsafe.Add(ref destination, 1), 0, value, countLo, bitOrder);
        }
    }
}
