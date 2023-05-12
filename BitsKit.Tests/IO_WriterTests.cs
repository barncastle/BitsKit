using System;
using System.IO;
using System.Runtime.CompilerServices;
using BitsKit.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitsKit.Tests;

[TestClass]
public class IO_WriterTests
{
    private readonly byte[] Data = new byte[]
    {
        0xCD, 0x0A, 162, 245, 92, 71, 202, 103, 218, 72
    };

    private readonly int[] BitCounts = new int[]
    {
        7, 10, 43, 0, 13, 6
    };

    [TestMethod]
    public void LSBSequentialMatchTest()
    {
        byte[] expected = Data[..];
        byte[] data1 = Data[..];
        byte[] data2 = Data[..];
        byte[] data3 = Data[..];
        using MemoryStream ms = new(data3);

        BitWriter bitWriter = new(data1);
        MemoryBitWriter memoryBitWriter = new(data2);
        BitStreamWriter bitStreamWriter = new(ms);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Data[2]);

        int bitOffset = 0;
        foreach (int bitCount in BitCounts)
        {
            Helpers.WriteBitsLSB(expected, bitOffset, value, bitCount);

            bitWriter.WriteUInt64LSB(value, bitCount);
            memoryBitWriter.WriteUInt64LSB(value, bitCount);
            bitStreamWriter.WriteUInt64LSB(value, bitCount);

            bitOffset += bitCount;

            // BitStreamWriter buffers partially written bytes
            // so only comparing the number of whole bytes written
            int byteCount = bitOffset >> 3;

            CollectionAssert.AreEqual(expected[..byteCount], data1[..byteCount], "BitWriter");
            CollectionAssert.AreEqual(expected[..byteCount], data2[..byteCount], "MemoryBitWriter");
            CollectionAssert.AreEqual(expected[..byteCount], data3[..byteCount], "BitStreamWriter");

            Assert.AreEqual(bitOffset, bitWriter.Position, "BitWriter.Position");
            Assert.AreEqual(bitOffset, memoryBitWriter.Position, "MemoryBitWriter.Position");
            Assert.AreEqual(bitOffset, bitStreamWriter.Position, "BitStreamWriter.Position");
        }

        // write remaining bits to stream
        bitStreamWriter.Flush();

        CollectionAssert.AreEqual(expected, data1, "BitWriter");
        CollectionAssert.AreEqual(expected, data2, "MemoryBitWriter");
        CollectionAssert.AreEqual(expected, data3, "BitStreamWriter");
    }

    [TestMethod]
    public void MSBSequentialMatchTest()
    {
        byte[] expected = Data[..];
        byte[] data1 = Data[..];
        byte[] data2 = Data[..];
        byte[] data3 = Data[..];
        using MemoryStream ms = new(data3);

        BitWriter bitWriter = new(data1);
        MemoryBitWriter memoryBitWriter = new(data2);
        BitStreamWriter bitStreamWriter = new(ms);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Data[2]);

        int bitOffset = 0;
        foreach (int bitCount in BitCounts)
        {
            Helpers.WriteBitsLSB(expected, bitOffset, value, bitCount);

            bitWriter.WriteUInt64LSB(value, bitCount);
            memoryBitWriter.WriteUInt64LSB(value, bitCount);
            bitStreamWriter.WriteUInt64LSB(value, bitCount);

            bitOffset += bitCount;

            // BitStreamWriter buffers partially written bytes
            // so only comparing the number of whole bytes written
            int byteCount = bitOffset >> 3;

            CollectionAssert.AreEqual(expected[..byteCount], data1[..byteCount], "BitWriter");
            CollectionAssert.AreEqual(expected[..byteCount], data2[..byteCount], "MemoryBitWriter");
            CollectionAssert.AreEqual(expected[..byteCount], data3[..byteCount], "BitStreamWriter");

            Assert.AreEqual(bitOffset, bitWriter.Position, "BitWriter.Position");
            Assert.AreEqual(bitOffset, memoryBitWriter.Position, "MemoryBitWriter.Position");
            Assert.AreEqual(bitOffset, bitStreamWriter.Position, "BitStreamWriter.Position");
        }

        // write remaining bits to stream
        bitStreamWriter.Flush();

        CollectionAssert.AreEqual(expected, data1, "BitWriter");
        CollectionAssert.AreEqual(expected, data2, "MemoryBitWriter");
        CollectionAssert.AreEqual(expected, data3, "BitStreamWriter");
    }

    [TestMethod]
    public void LSBNonSequentialMatchTest()
    {
        byte[] expected = Data[..];
        byte[] data1 = Data[..];
        byte[] data2 = Data[..];
        byte[] data3 = Data[..];
        using MemoryStream ms = new(data3);

        BitWriter bitWriter = new(data1);
        MemoryBitWriter memoryBitWriter = new(data2);
        BitStreamWriter bitStreamWriter = new(ms);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Data[2]);

        int bitOffset = 80;
        foreach (int bitCount in BitCounts)
        {
            bitOffset -= bitCount;

            Helpers.WriteBitsLSB(expected, bitOffset, value, bitCount);

            bitWriter.Position = bitOffset;
            memoryBitWriter.Position = bitOffset;
            bitStreamWriter.Position = bitOffset;

            bitWriter.WriteUInt64LSB(value, bitCount);
            memoryBitWriter.WriteUInt64LSB(value, bitCount);
            bitStreamWriter.WriteUInt64LSB(value, bitCount);

            bitStreamWriter.Flush();

            CollectionAssert.AreEqual(expected, data1, "BitWriter");
            CollectionAssert.AreEqual(expected, data2, "MemoryBitWriter");
            CollectionAssert.AreEqual(expected, data3, "BitStreamWriter");
        }
    }

    [TestMethod]
    public void MSBNonSequentialMatchTest()
    {
        byte[] expected = Data[..];
        byte[] data1 = Data[..];
        byte[] data2 = Data[..];
        byte[] data3 = Data[..];
        using MemoryStream ms = new(data3);

        BitWriter bitWriter = new(data1);
        MemoryBitWriter memoryBitWriter = new(data2);
        BitStreamWriter bitStreamWriter = new(ms);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Data[2]);

        int bitOffset = 80;
        foreach (int bitCount in BitCounts)
        {
            bitOffset -= bitCount;

            Helpers.WriteBitsMSB(expected, bitOffset, value, bitCount);

            bitWriter.Position = bitOffset;
            memoryBitWriter.Position = bitOffset;
            bitStreamWriter.Position = bitOffset;

            bitWriter.WriteUInt64MSB(value, bitCount);
            memoryBitWriter.WriteUInt64MSB(value, bitCount);
            bitStreamWriter.WriteUInt64MSB(value, bitCount);

            bitStreamWriter.Flush();

            CollectionAssert.AreEqual(expected, data1, "BitWriter");
            CollectionAssert.AreEqual(expected, data2, "MemoryBitWriter");
            CollectionAssert.AreEqual(expected, data3, "BitStreamWriter");
        }
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    public void LSBBitMatchTest(int bitOffset)
    {
        byte[] expected = Data[..];
        byte[] data1 = Data[..];
        byte[] data2 = Data[..];
        byte[] data3 = Data[..];
        using MemoryStream ms = new(data3);

        BitWriter bitWriter = new(data1);
        MemoryBitWriter memoryBitWriter = new(data2);
        BitStreamWriter bitStreamWriter = new(ms);

        Helpers.WriteBitsLSB(expected, bitOffset, 1, 1);

        bitWriter.Position = bitOffset;
        memoryBitWriter.Position = bitOffset;
        bitStreamWriter.Position = bitOffset;

        bitWriter.WriteBitLSB(true);
        memoryBitWriter.WriteBitLSB(true);
        bitStreamWriter.WriteBitLSB(true);

        bitStreamWriter.Flush();

        CollectionAssert.AreEqual(expected, data1, "BitWriter");
        CollectionAssert.AreEqual(expected, data2, "MemoryBitWriter");
        CollectionAssert.AreEqual(expected, data3, "BitStreamWriter");
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    public void MSBBitMatchTest(int bitOffset)
    {
        byte[] expected = Data[..];
        byte[] data1 = Data[..];
        byte[] data2 = Data[..];
        byte[] data3 = Data[..];
        using MemoryStream ms = new(data3);

        BitWriter bitWriter = new(data1);
        MemoryBitWriter memoryBitWriter = new(data2);
        BitStreamWriter bitStreamWriter = new(ms);

        Helpers.WriteBitsMSB(expected, bitOffset, 1, 1);

        bitWriter.Position = bitOffset;
        memoryBitWriter.Position = bitOffset;
        bitStreamWriter.Position = bitOffset;

        bitWriter.WriteBitMSB(true);
        memoryBitWriter.WriteBitMSB(true);
        bitStreamWriter.WriteBitMSB(true);

        bitStreamWriter.Flush();

        CollectionAssert.AreEqual(expected, data1, "BitWriter");
        CollectionAssert.AreEqual(expected, data2, "MemoryBitWriter");
        CollectionAssert.AreEqual(expected, data3, "BitStreamWriter");
    }

    [TestMethod]
    public void WriteOnlyStreamTest()
    {
        byte[] expected = new byte[10];

        using WriteOnlyMemoryStream ms = new();
        using BitStreamWriter bitStreamWriter = new(ms);

        ulong value = Unsafe.ReadUnaligned<ulong>(ref Data[2]);

        int bitOffset = 0;
        foreach (int bitCount in BitCounts)
        {
            Helpers.WriteBitsLSB(expected, bitOffset, value, bitCount);

            bitStreamWriter.WriteUInt64LSB(value, bitCount);

            bitOffset += bitCount;

            // BitStreamWriter buffers partially written bytes
            // so only comparing the number of whole bytes written
            int byteCount = bitOffset >> 3;

            CollectionAssert.AreEqual(expected[..byteCount], ms.GetBuffer(byteCount), "BitStreamWriter");

            Assert.AreEqual(bitOffset, bitStreamWriter.Position, "BitStreamWriter.Position");
        }

        // write remaining bits to stream
        bitStreamWriter.Flush();

        CollectionAssert.AreEqual(expected, ms.GetBuffer((int)ms.Length), "BitStreamWriter");

        // check we can't seek or buffer
        Assert.ThrowsException<NotSupportedException>(() => bitStreamWriter.Position = 0);
    }
}
