using BitsKit.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BitsKit.Tests;

[TestClass]
public class WriterTests
{
    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void LSBSpanMatchTest(int bitOffset, int bitSize)
    {
        Span<byte> expected = stackalloc byte[9];
        Span<byte> result = stackalloc byte[9];

        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        // fill buffers to test masking
        expected.Fill(0xAA);
        result.Fill(0xAA);

        Helpers.WriteBitsLSB(expected, bitOffset, 0x5555555555555555UL, bitSize);

        if (bitSize + bitOffset <= 8)
        {
            BitPrimitives.WriteUInt8LSB(result, bitOffset, 0x55, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt8");
        }

        if (bitSize + bitOffset <= 16)
        {
            BitPrimitives.WriteUInt16LSB(result, bitOffset, 0x5555, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt16");
        }

        if (bitSize + bitOffset <= 32)
        {
            BitPrimitives.WriteUInt32LSB(result, bitOffset, 0x55555555u, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt32");
        }

        if (bitSize + bitOffset <= 64)
        {
            BitPrimitives.WriteUInt64LSB(result, bitOffset, 0x5555555555555555UL, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt64");
        }
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void MSBSpanMatchTest(int bitOffset, int bitSize)
    {
        Span<byte> expected = stackalloc byte[9];
        Span<byte> result = stackalloc byte[9];

        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        // fill buffers to test masking
        expected.Fill(0xAA);
        result.Fill(0xAA);

        Helpers.WriteBitsMSB(expected, bitOffset, 0x5555555555555555UL, bitSize);

        if (bitSize + bitOffset <= 8)
        {
            BitPrimitives.WriteUInt8MSB(result, bitOffset, 0x55, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt8");
        }

        if (bitSize + bitOffset <= 16)
        {
            BitPrimitives.WriteUInt16MSB(result, bitOffset, 0x5555, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt16");
        }

        if (bitSize + bitOffset <= 32)
        {
            BitPrimitives.WriteUInt32MSB(result, bitOffset, 0x55555555u, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt32");
        }

        if (bitSize + bitOffset <= 64)
        {
            BitPrimitives.WriteUInt64MSB(result, bitOffset, 0x5555555555555555UL, bitSize);
            Assert.IsTrue(expected.SequenceEqual(result), message, "WriteUInt64");
        }
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetParams), DynamicDataSourceType.Method)]
    public void LSBBitWriteTests(int bitOffset)
    {
        // write buffers
        Span<byte> expected = stackalloc byte[17];
        Span<byte> result = stackalloc byte[17];

        // fill buffers to test masking
        expected.Fill(0xAA);
        result.Fill(0xAA);

        Helpers.WriteBitsLSB(expected, bitOffset, 1, 1);
        BitPrimitives.WriteBitLSB(result, bitOffset, true);
        Assert.IsTrue(expected.SequenceEqual(result), "Bit_LSB");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetParams), DynamicDataSourceType.Method)]
    public void MSBBitWriteTests(int bitOffset)
    {
        // write buffers
        Span<byte> expected = stackalloc byte[17];
        Span<byte> result = stackalloc byte[17];

        // fill buffers to test masking
        expected.Fill(0xAA);
        result.Fill(0xAA);

        Helpers.WriteBitsMSB(expected, bitOffset, 1, 1);
        BitPrimitives.WriteBitMSB(result, bitOffset, true);
        Assert.IsTrue(expected.SequenceEqual(result), "Bit_MSB");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void LSBValueMatchTest(int bitOffset, int bitSize)
    {
        Span<byte> expected = stackalloc byte[9];

        byte uint8 = 0;
        ushort uint16 = 0;
        uint uint32 = 0;
        ulong uint64 = 0;

        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        // fill buffers to test masking
        expected.Fill(0xAA);
        uint8 = (byte)(uint16 = (ushort)(uint32 = (uint)(uint64 = 0xAAAAAAAAAAAAAAAAUL)));

        Helpers.WriteBitsLSB(expected, bitOffset, 0x5555555555555555UL, bitSize);

        if (bitSize + bitOffset <= 8)
        {
            BitPrimitives.WriteUInt8LSB(ref uint8, bitOffset, 0x55, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<byte>(ref expected[0]), uint8, message, "WriteUInt8");
        }

        if (bitSize + bitOffset <= 16)
        {
            BitPrimitives.WriteUInt16LSB(ref uint16, bitOffset, 0x5555, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<ushort>(ref expected[0]), uint16, message, "WriteUInt16");
        }

        if (bitSize + bitOffset <= 32)
        {
            BitPrimitives.WriteUInt32LSB(ref uint32, bitOffset, 0x55555555u, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<uint>(ref expected[0]), uint32, message, "WriteUInt32");
        }

        if (bitSize + bitOffset <= 64)
        {
            BitPrimitives.WriteUInt64LSB(ref uint64, bitOffset, 0x5555555555555555UL, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<ulong>(ref expected[0]), uint64, message, "WriteUInt64");
        }
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void MSBValueMatchTest(int bitOffset, int bitSize)
    {
        Span<byte> expected = stackalloc byte[9];

        byte uint8 = 0;
        ushort uint16 = 0;
        uint uint32 = 0;
        ulong uint64 = 0;

        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        // fill buffers to test masking
        expected.Fill(0xAA);
        uint8 = (byte)(uint16 = (ushort)(uint32 = (uint)(uint64 = 0xAAAAAAAAAAAAAAAAUL)));

        Helpers.WriteBitsMSB(expected, bitOffset, 0x5555555555555555UL, bitSize);

        if (bitSize + bitOffset <= 8)
        {
            BitPrimitives.WriteUInt8MSB(ref uint8, bitOffset, 0x55, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<byte>(ref expected[0]), uint8, message, "WriteUInt8");
        }

        if (bitSize + bitOffset <= 16)
        {
            BitPrimitives.WriteUInt16MSB(ref uint16, bitOffset, 0x5555, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<ushort>(ref expected[0]), uint16, message, "WriteUInt16");
        }

        if (bitSize + bitOffset <= 32)
        {
            BitPrimitives.WriteUInt32MSB(ref uint32, bitOffset, 0x55555555u, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<uint>(ref expected[0]), uint32, message, "WriteUInt32");
        }

        if (bitSize + bitOffset <= 64)
        {
            BitPrimitives.WriteUInt64MSB(ref uint64, bitOffset, 0x5555555555555555UL, bitSize);
            Assert.AreEqual(Unsafe.ReadUnaligned<ulong>(ref expected[0]), uint64, message, "WriteUInt64");
        }
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    public void ExceptionTests(int bitOffset)
    {
        Assert.ThrowsException<IndexOutOfRangeException>(() => BitPrimitives.WriteBitLSB([], bitOffset, true));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt8LSB(new byte[1], bitOffset, 1, 9 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt8MSB(new byte[1], bitOffset, 1, 9 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt16LSB(new byte[2], bitOffset, 1, 17 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt16MSB(new byte[2], bitOffset, 1, 17 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt32LSB(new byte[4], bitOffset, 1, 33 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt32MSB(new byte[4], bitOffset, 1, 33 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt64LSB(new byte[8], bitOffset, 1, 65 - bitOffset));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitPrimitives.WriteUInt64MSB(new byte[8], bitOffset, 1, 65 - bitOffset));
    }

    private static IEnumerable<object[]> GetTestOffsetAndSizeParams() =>
        Helpers.GetTestOffsetAndSizeParams();

    private static IEnumerable<object[]> GetTestOffsetParams() =>
        Helpers.GetTestOffsetParams();
}
