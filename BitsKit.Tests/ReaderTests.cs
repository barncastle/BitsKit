using BitsKit.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BitsKit.Tests;

[TestClass]
public class ReaderTests
{
    private readonly byte[] Data = new byte[]
    {
        0xCD, 0x0A, 162, 245, 92, 71, 202, 103, 218, 72
    };

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void LSBSpanMatchTest(int bitOffset, int bitSize)
    {
        ulong expected = Helpers.ReadBitsLSB(Data, bitOffset, bitSize);
        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        if (bitSize + bitOffset <= 8)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt8LSB(Data, bitOffset, bitSize), message, "ReadUInt8");
        if (bitSize + bitOffset <= 16)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt16LSB(Data, bitOffset, bitSize), message, "ReadUInt16");
        if (bitSize + bitOffset <= 32)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt32LSB(Data, bitOffset, bitSize), message, "ReadUInt32");
        if (bitSize + bitOffset <= 64)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt64LSB(Data, bitOffset, bitSize), message, "ReadUInt64");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void MSBSpanMatchTest(int bitOffset, int bitSize)
    {
        ulong expected = Helpers.ReadBitsMSB(Data, bitOffset, bitSize);
        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        if (bitSize + bitOffset <= 8)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt8MSB(Data, bitOffset, bitSize), message, "ReadUInt8");
        if (bitSize + bitOffset <= 16)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt16MSB(Data, bitOffset, bitSize), message, "ReadUInt16");
        if (bitSize + bitOffset <= 32)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt32MSB(Data, bitOffset, bitSize), message, "ReadUInt32");
        if (bitSize + bitOffset <= 64)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt64MSB(Data, bitOffset, bitSize), message, "ReadUInt64");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetParams), DynamicDataSourceType.Method)]
    public void LSBBitReadTests(int bitOffset)
    {
        bool targetBit = Helpers.ReadBitsLSB(Data, bitOffset, 1) == 1;
        bool resultBit = BitPrimitives.ReadBitLSB(Data, bitOffset);

        Assert.AreEqual(targetBit, resultBit, "Bit_LSB");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetParams), DynamicDataSourceType.Method)]
    public void MSBBitReadTests(int bitOffset)
    {
        bool targetBit = Helpers.ReadBitsMSB(Data, bitOffset, 1) == 1;
        bool resultBit = BitPrimitives.ReadBitMSB(Data, bitOffset);

        Assert.AreEqual(targetBit, resultBit, "Bit_MSB");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void LSBValueMatchTest(int bitOffset, int bitSize)
    {
        ulong expected = Helpers.ReadBitsLSB(Data, bitOffset, bitSize);
        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        byte uint08 = Data[0];
        ushort uint16 = Unsafe.ReadUnaligned<ushort>(ref Data[0]);
        uint uint32 = Unsafe.ReadUnaligned<uint>(ref Data[0]);
        ulong uint64 = Unsafe.ReadUnaligned<ulong>(ref Data[0]);

        if (bitSize + bitOffset <= 8)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt8LSB(uint08, bitOffset, bitSize), message, "ReadUInt8");
        if (bitSize + bitOffset <= 16)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt16LSB(uint16, bitOffset, bitSize), message, "ReadUInt16");
        if (bitSize + bitOffset <= 32)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt32LSB(uint32, bitOffset, bitSize), message, "ReadUInt32");
        if (bitSize + bitOffset <= 64)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt64LSB(uint64, bitOffset, bitSize), message, "ReadUInt64");
    }

    [TestMethod]
    [DynamicData(nameof(GetTestOffsetAndSizeParams), DynamicDataSourceType.Method)]
    public void MSBValueMatchTest(int bitOffset, int bitSize)
    {
        ulong expected = Helpers.ReadBitsMSB(Data, bitOffset, bitSize);
        string message = $"UInt{bitSize:D2} @ {bitOffset} {{0}}";

        byte uint08 = Data[0];
        ushort uint16 = Unsafe.ReadUnaligned<ushort>(ref Data[0]);
        uint uint32 = Unsafe.ReadUnaligned<uint>(ref Data[0]);
        ulong uint64 = Unsafe.ReadUnaligned<ulong>(ref Data[0]);

        if (bitSize + bitOffset <= 8)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt8MSB(uint08, bitOffset, bitSize), message, "ReadUInt8");
        if (bitSize + bitOffset <= 16)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt16MSB(uint16, bitOffset, bitSize), message, "ReadUInt16");
        if (bitSize + bitOffset <= 32)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt32MSB(uint32, bitOffset, bitSize), message, "ReadUInt32");
        if (bitSize + bitOffset <= 64)
            Assert.AreEqual(expected, BitPrimitives.ReadUInt64MSB(uint64, bitOffset, bitSize), message, "ReadUInt64");
    }

    private static IEnumerable<object[]> GetTestOffsetAndSizeParams() =>
        Helpers.GetTestOffsetAndSizeParams();

    private static IEnumerable<object[]> GetTestOffsetParams() =>
        Helpers.GetTestOffsetParams();
}
