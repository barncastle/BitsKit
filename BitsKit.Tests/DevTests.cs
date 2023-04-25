
#pragma warning disable

#if DEBUG

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BitsKit;
using BitsKit.IO;
using BitsKit.Primitives;
using BitsKit.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitsKit.Tests;

[TestClass]
public class DevTests
{
    private delegate void WriteMethod<T>(Span<byte> destination, int bitOffset, T value, int bitCount) where T : unmanaged;

    private readonly byte[] Data = new byte[]
    {
        239, 68, 162, 245, 92, 32, 202, 103, 218,
        251, 248, 150, 234, 92, 161, 192, 27, 146,
        52, 99, 200, 1
    };

    [TestMethod]
    public void DebugTest()
    {
        var u1_LE = Unsafe.ReadUnaligned<uint>(ref Data[1]);
        var u1_BE = BinaryPrimitives.ReverseEndianness(BitPrimitives.ReverseBitOrder(u1_LE));

        var r1_LE = ReadValue32(u1_LE, 3, 14, BitOrder.LeastSignificant);
        var r1_BE = ReadValue32(u1_BE, 3, 14, BitOrder.LeastSignificant);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint ReadValue32(uint source, int bitOffset, int bitCount, BitOrder bitOrder)
    {
        if (bitCount == 0)
            return 0;

        if (bitOrder == BitOrder.LeastSignificant)
            return source << (32 - bitCount - bitOffset) >> (32 - bitCount);
        else
            return BinaryPrimitives.ReverseEndianness(source) << bitOffset >> (32 - bitCount);
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
            mask = BitPrimitives.ReverseBitOrder(mask << bitOffset);
            // align the value to the opposite significant bit and reverse it's endianness
            value = BinaryPrimitives.ReverseEndianness(value << (32 - bitCount - bitOffset));
        }

        destination &= ~mask;
        destination |= value;
    }
}

#endif
