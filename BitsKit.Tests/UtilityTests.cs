using BitsKit.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitsKit.Tests;

[TestClass]
public class UtilityTests
{
    [TestMethod]
    public void SimpleBitInterleavingTest()
    {
        const uint a = uint.MaxValue;
        const uint b = 0;

        unchecked
        {
            ushort uint08 = BitUtilities.InterleaveBits((byte)a, (byte)b);
            uint uint16 = BitUtilities.InterleaveBits((ushort)a, (ushort)b);
            ulong uint32 = BitUtilities.InterleaveBits(a, b);

            Assert.AreEqual(0x5555, uint08);
            Assert.AreEqual(0x55555555u, uint16);
            Assert.AreEqual(0x5555555555555555UL, uint32);

            uint08 = BitUtilities.InterleaveBits((byte)b, (byte)a);
            uint16 = BitUtilities.InterleaveBits((ushort)b, (ushort)a);
            uint32 = BitUtilities.InterleaveBits(b, a);

            Assert.AreEqual(0xAAAA, uint08);
            Assert.AreEqual(0xAAAAAAAAu, uint16);
            Assert.AreEqual(0xAAAAAAAAAAAAAAAAUL, uint32);
        }
    }

    [TestMethod]
    public void ComplexBitInterleavingTest()
    {
        sbyte a1 = 0b00_1111;
        sbyte b1 = 0b10_0010;
        Assert.AreEqual(0b1000_0101_1101, BitUtilities.InterleaveBits(a1, b1));

        short a2 = 0b11_0101_1010_0101;
        short b2 = 0b10_0010_0110_1111;
        Assert.AreEqual(0xD196CBB, BitUtilities.InterleaveBits(a2, b2));

        int a3 = 0b0100_1010_1101_1010_1110_1001_0011_0110;
        int b3 = 0b0110_0010_0101_0010_0110_0010_1011_0001;
        Assert.AreEqual(0x384C734C7C498F16L, BitUtilities.InterleaveBits(a3, b3));

        byte a4 = 0b1010_0101;
        byte b4 = 0b0111_1000;
        Assert.AreEqual(0b0110_1110_1001_0001, BitUtilities.InterleaveBits(a4, b4));

        ushort a5 = 0b1010_1010_0101_0101;
        ushort b5 = 0b1111_0000_1111_0000;
        Assert.AreEqual(0xEE44BB11u, BitUtilities.InterleaveBits(a5, b5));

        uint a6 = 0b1010_1010_0101_0101_1111_0000_1111_0000;
        uint b6 = 0b0000_1111_0000_1111_1010_1010_0101_0101;
        Assert.AreEqual(0x44EE11BBDD887722UL, BitUtilities.InterleaveBits(a6, b6));
    }

    [TestMethod]
    public void BitInvertTest()
    {
        byte uint8___ = 0b1100_1101;
        Assert.AreEqual(0b0011_0010, BitUtilities.NegateBits(uint8___, 0, 8));
        Assert.AreEqual(0b1100_1101, BitUtilities.NegateBits(uint8___, 0, 0));
        Assert.AreEqual(0b1100_0010, BitUtilities.NegateBits(uint8___, 0, 4));
        Assert.AreEqual(0b0011_1101, BitUtilities.NegateBits(uint8___, 4, 4));

        uint uint32__ = 0b1111_1110_1111_0000_1111_0101_1111_0001u;
        Assert.AreEqual(0b0000_0001_0000_1111_0000_1010_0000_1110u, BitUtilities.NegateBits((ulong)uint32__, 0, 32));
        Assert.AreEqual(0b1100_0001_0000_1111_0000_1010_0000_1110u, BitUtilities.NegateBits((ulong)uint32__, 0, 30));
        Assert.AreEqual(0b0000_0001_0000_1111_0000_1010_0000_1101u, BitUtilities.NegateBits((ulong)uint32__, 2, 30));

        ulong uint64_ = 0b1111_1111_1111_1110_1111_0000_1111_0101_1111_0000_0001_0111_1111_0100_0000_0110UL;
        Assert.AreEqual(0b0000_0000_0000_0001_0000_1111_0000_1010_0000_1111_1110_1000_0000_1011_1111_1001UL, BitUtilities.NegateBits(uint64_, 0, 64));
        Assert.AreEqual(0b1111_1111_1111_1110_1111_0000_1100_1010_0000_1111_1110_1000_0000_1011_1000_0110UL, BitUtilities.NegateBits(uint64_, 7, 31));
        Assert.AreEqual(0b0000_0000_0000_0001_0000_1111_0000_1010_0000_1100_0001_0111_1111_0100_0000_0110UL, BitUtilities.NegateBits(uint64_, 26, 62));

    }

    [TestMethod]
    public void BitMergeTest()
    {
        //Assert.AreEqual(0b11001110, BitUtilities.MergeBits((byte)0b10101110, (byte)0b11001010, 0b11110000));

        //// Test MergeBits method
        //Assert.AreEqual((sbyte)11, BitUtilities.MergeBits((sbyte)9, (sbyte)3, (byte)4));
        //Assert.AreEqual((short)1028, BitUtilities.MergeBits((short)960, (short)132, (ushort)1088));
        //Assert.AreEqual(1000000011, BitUtilities.MergeBits(1000000000, 11, 10));
        //Assert.AreEqual(9223372036854775851, BitUtilities.MergeBits(9223372036854775807, 4, 4));
        //Assert.AreEqual((byte)30, BitUtilities.MergeBits((byte)18, (byte)15, (byte)12));
        //Assert.AreEqual((ushort)1389, BitUtilities.MergeBits((ushort)409, (ushort)1523, (ushort)1300));
        //Assert.AreEqual(4026531854, BitUtilities.MergeBits(268435455, 4026531854, 1));
        //Assert.AreEqual(9223372036854775819, BitUtilities.MergeBits(9223372036854775807, 4, 4));
    }
}
