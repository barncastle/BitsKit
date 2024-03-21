using System.IO;
using BitsKit.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitsKit.Tests;

[TestClass]
public class IO_ReaderTests
{
    private readonly byte[] Data =
    [
        0xCD, 0x0A, 162, 245, 92, 71, 202, 103, 218, 72
    ];

    private readonly int[] BitCounts =
    [
        7, 10, 43, 0, 13, 6
    ];

    [TestMethod]
    public void LSBSequentialMatchTest()
    {
        using MemoryStream ms = new(Data);

        BitReader bitReader = new(Data);
        MemoryBitReader memoryBitReader = new(Data);
        BitStreamReader bitStreamReader = new(ms);

        int bitOffset = 0;
        foreach (int bitCount in BitCounts)
        {
            ulong expected = Helpers.ReadBitsLSB(Data, bitOffset, bitCount);

            ulong readerValue = bitReader.ReadUInt64LSB(bitCount);
            ulong memoryValue = memoryBitReader.ReadUInt64LSB(bitCount);
            ulong streamValue = bitStreamReader.ReadUInt64LSB(bitCount);

            Assert.AreEqual(expected, readerValue, "BitReader");
            Assert.AreEqual(expected, memoryValue, "MemoryBitReader");
            Assert.AreEqual(expected, streamValue, "BitStreamReader");

            bitOffset += bitCount;

            Assert.AreEqual(bitOffset, bitReader.Position, "BitReader.Position");
            Assert.AreEqual(bitOffset, memoryBitReader.Position, "MemoryBitReader.Position");
            Assert.AreEqual(bitOffset, bitStreamReader.Position, "BitStreamReader.Position");
        }
    }

    [TestMethod]
    public void MSBSequentialMatchTest()
    {
        using MemoryStream ms = new(Data);

        BitReader bitReader = new(Data);
        MemoryBitReader memoryBitReader = new(Data);
        BitStreamReader bitStreamReader = new(ms);

        int bitOffset = 0;
        foreach (int bitCount in BitCounts)
        {
            ulong expected = Helpers.ReadBitsMSB(Data, bitOffset, bitCount);

            ulong readerValue = bitReader.ReadUInt64MSB(bitCount);
            ulong memoryValue = memoryBitReader.ReadUInt64MSB(bitCount);
            ulong streamValue = bitStreamReader.ReadUInt64MSB(bitCount);

            Assert.AreEqual(expected, readerValue, "BitReader");
            Assert.AreEqual(expected, memoryValue, "MemoryBitReader");
            Assert.AreEqual(expected, streamValue, "BitStreamReader");

            bitOffset += bitCount;

            Assert.AreEqual(bitOffset, bitReader.Position, "BitReader.Position");
            Assert.AreEqual(bitOffset, memoryBitReader.Position, "MemoryBitReader.Position");
            Assert.AreEqual(bitOffset, bitStreamReader.Position, "BitStreamReader.Position");
        }
    }

    [TestMethod]
    public void LSBNonSequentialMatchTest()
    {
        using MemoryStream ms = new(Data);

        BitReader bitReader = new(Data);
        MemoryBitReader memoryBitReader = new(Data);
        BitStreamReader bitStreamReader = new(ms);

        int bitOffset = 80;
        foreach (int bitCount in BitCounts)
        {
            bitOffset -= bitCount;

            ulong expected = Helpers.ReadBitsLSB(Data, bitOffset, bitCount);

            bitReader.Position = bitOffset;
            memoryBitReader.Position = bitOffset;
            bitStreamReader.Position = bitOffset;

            ulong readerValue = bitReader.ReadUInt64LSB(bitCount);
            ulong memoryValue = memoryBitReader.ReadUInt64LSB(bitCount);
            ulong streamValue = bitStreamReader.ReadUInt64LSB(bitCount);

            Assert.AreEqual(expected, readerValue, "BitReader");
            Assert.AreEqual(expected, memoryValue, "MemoryBitReader");
            Assert.AreEqual(expected, streamValue, "BitStreamReader");
        }
    }

    [TestMethod]
    public void MSBNonSequentialMatchTest()
    {
        using MemoryStream ms = new(Data);

        BitReader bitReader = new(Data);
        MemoryBitReader memoryBitReader = new(Data);
        BitStreamReader bitStreamReader = new(ms);

        int bitOffset = 80;
        foreach (int bitCount in BitCounts)
        {
            bitOffset -= bitCount;

            ulong expected = Helpers.ReadBitsMSB(Data, bitOffset, bitCount);

            bitReader.Position = bitOffset;
            memoryBitReader.Position = bitOffset;
            bitStreamReader.Position = bitOffset;

            ulong readerValue = bitReader.ReadUInt64MSB(bitCount);
            ulong memoryValue = memoryBitReader.ReadUInt64MSB(bitCount);
            ulong streamValue = bitStreamReader.ReadUInt64MSB(bitCount);

            Assert.AreEqual(expected, readerValue, "BitReader");
            Assert.AreEqual(expected, memoryValue, "MemoryBitReader");
            Assert.AreEqual(expected, streamValue, "BitStreamReader");
        }
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    public void LSBBitMatchTest(int bitOffset)
    {
        using MemoryStream ms = new(Data);

        BitReader bitReader = new(Data);
        MemoryBitReader memoryBitReader = new(Data);
        BitStreamReader bitStreamReader = new(ms);

        bool expected = Helpers.ReadBitsLSB(Data, bitOffset, 1) == 1;

        bitReader.Position = bitOffset;
        memoryBitReader.Position = bitOffset;
        bitStreamReader.Position = bitOffset;

        bool readerValue = bitReader.ReadBitLSB();
        bool memoryValue = memoryBitReader.ReadBitLSB();
        bool streamValue = bitStreamReader.ReadBitLSB();

        Assert.AreEqual(expected, readerValue, "BitReader");
        Assert.AreEqual(expected, memoryValue, "MemoryBitReader");
        Assert.AreEqual(expected, streamValue, "BitStreamReader");
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    public void MSBBitMatchTest(int bitOffset)
    {
        using MemoryStream ms = new(Data);

        BitReader bitReader = new(Data);
        MemoryBitReader memoryBitReader = new(Data);
        BitStreamReader bitStreamReader = new(ms);

        bool expected = Helpers.ReadBitsMSB(Data, bitOffset, 1) == 1;

        bitReader.Position = bitOffset;
        memoryBitReader.Position = bitOffset;
        bitStreamReader.Position = bitOffset;

        bool readerValue = bitReader.ReadBitMSB();
        bool memoryValue = memoryBitReader.ReadBitMSB();
        bool streamValue = bitStreamReader.ReadBitMSB();

        Assert.AreEqual(expected, readerValue, "BitReader");
        Assert.AreEqual(expected, memoryValue, "MemoryBitReader");
        Assert.AreEqual(expected, streamValue, "BitStreamReader");
    }
}
