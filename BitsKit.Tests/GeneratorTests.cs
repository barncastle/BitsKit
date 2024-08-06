using System;
using System.Collections.Immutable;
using System.Linq;
using BitsKit.BitFields;
using BitsKit.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitsKit.Tests;

[TestClass]
public class GeneratorTests
{
    [TestMethod]
    public void SetterTests()
    {
        const int Expected = 0b1111;

        SequentialBitFieldStruct sequentialObj = new()
        {
            Generated01 = 0b11,
            Generated02 = 0b11,
        };

        Assert.AreEqual(sequentialObj.BackingField00, Expected, "SequentialBitFieldStruct.BackingField00 != Expected");
        Assert.AreEqual(sequentialObj.BackingField01, 0, "SequentialBitFieldStruct.BackingField01 != 0");

        ExplicitBitFieldStruct explicitObj = new()
        {
            Generated01 = 0b11,
            Generated02 = 0b11,
        };

        Assert.AreEqual(explicitObj.BackingField00, Expected, "ExplicitBitFieldStruct.BackingField00 != Expected");
        Assert.AreEqual(explicitObj.BackingField01, (ulong)Expected, "ExplicitBitFieldStruct.BackingField00 != ExplicitBitFieldStruct.BackingField01");

        BitFieldRecordStruct recordObj = new()
        {
            Generated01 = 0b11,
            Generated02 = 0b11,
        };

        Assert.AreEqual(recordObj.BackingField00, Expected, "BitFieldRecordStruct.BackingField00 != Expected");
        Assert.AreEqual(recordObj.BackingField01, 0, "BitFieldRecordStruct.BackingField01 != 0");

        BitFieldMemoryStruct memoryObj = new()
        {
            BackingField00 = new byte[4],
            BackingField01 = new byte[4],
            Generated01 = 0b11,
            Generated02 = 0b11,
        };

        Assert.AreEqual(memoryObj.IntValue00, Expected, "BitFieldMemoryStruct.IntValue00 != Expected");
        Assert.AreEqual(memoryObj.IntValue01, 0, "BitFieldMemoryStruct.IntValue01 != 0");

        BitFieldRefStruct refStructObj = new()
        {
            BackingField00 = new byte[4],
            BackingField01 = new byte[4],
            Generated01 = 0b11,
            Generated02 = 0b11,
        };

        Assert.AreEqual(refStructObj.IntValue00, Expected, "BitFieldRefStruct.IntValue00 != Expected");
        Assert.AreEqual(refStructObj.IntValue01, 0, "BitFieldRefStruct.IntValue01 != 0");


        BitFieldFixedStruct fixedObj = new()
        {
            Generated01 = 0b11,
            Generated02 = 0b11,
        };

        Assert.AreEqual(fixedObj.IntValue00, Expected, "BitFieldFixedStruct.IntValue00 != Expected");
        Assert.AreEqual(fixedObj.BackingField01, 0, "BitFieldFixedStruct.BackingField01 != 0");

        EnumBitFieldStruct enumObj = new()
        {
            Generated01 = (TestEnum)0b111,
            Generated02 = (TestEnum)0b111,
        };

        Assert.AreEqual(enumObj.BackingField00, (uint)Expected, "EnumBitFieldStruct.BackingField00 != Expected");
        Assert.AreEqual(enumObj.BackingField01, 0u, "EnumBitFieldStruct.BackingField01 != 0");

#if NET8_0_OR_GREATER
        InlineArrayStruct inlineArrayObj = new()
        {
            Generated01 = 0b11,
            Generated02 = 0b11,
            // padding : 27
            Generated03 = 0b11 // boundary straddling test
        };

        Assert.AreEqual((uint)inlineArrayObj[0], Expected | 0x80000000, "InlineArrayStruct[0] != (Expected | 0x80000000)");
        Assert.AreEqual(inlineArrayObj[1], 1, "InlineArrayStruct[1] != 1");
#endif
    }

    [TestMethod]
    public void GetterTests()
    {
        const int Input = 0b101110;
        const int Expected01 = -0b10; // negative as sign extended
        const int Expected02 = -0b01; // negative as sign extended

        SequentialBitFieldStruct sequentialObj = new()
        {
            BackingField00 = Input
        };

        Assert.AreEqual(sequentialObj.Generated01, Expected01, "SequentialBitFieldStruct.Generated01 != Expected01");
        Assert.AreEqual(sequentialObj.Generated02, Expected02, "SequentialBitFieldStruct.Generated02 != Expected02");

        ExplicitBitFieldStruct explicitObj = new()
        {
            BackingField00 = Input
        };

        Assert.AreEqual(explicitObj.Generated01, Expected01, "ExplicitBitFieldStruct.Generated01 != Expected01");
        Assert.AreEqual(explicitObj.Generated02, Expected02, "ExplicitBitFieldStruct.Generated02 != Expected02");

        BitFieldRecordStruct recordObj = new()
        {
            BackingField00 = Input
        };

        Assert.AreEqual(recordObj.Generated01, Expected01, "BitFieldRecordStruct.Generated01 != Expected01");
        Assert.AreEqual(recordObj.Generated02, Expected02, "BitFieldRecordStruct.Generated02 != Expected02");

        BitFieldMemoryStruct memoryObj = new()
        {
            BackingField00 = BitConverter.GetBytes(Input)
        };

        Assert.AreEqual(memoryObj.Generated01, Expected01, "BitFieldMemoryStruct.Generated01 != Expected01");
        Assert.AreEqual(memoryObj.Generated02, Expected02, "BitFieldMemoryStruct.Generated02 != Expected02");

        BitFieldRefStruct refStructObj = new()
        {
            BackingField00 = BitConverter.GetBytes(Input)
        };

        Assert.AreEqual(refStructObj.Generated01, Expected01, "BitFieldRefStruct.Generated01 != Expected01");
        Assert.AreEqual(refStructObj.Generated02, Expected02, "BitFieldRefStruct.Generated02 != Expected02");


        BitFieldFixedStruct fixedObj = new()
        {
            IntValue00 = Input
        };

        Assert.AreEqual(fixedObj.Generated01, Expected01, "BitFieldFixedStruct.Generated01 != Expected01");
        Assert.AreEqual(fixedObj.Generated02, Expected02, "BitFieldFixedStruct.Generated02 != Expected02");

        EnumBitFieldStruct enumObj = new()
        {
            BackingField00 = Input
        };

        Assert.AreEqual(enumObj.Generated01, TestEnum.B, "EnumBitFieldStruct.Generated01 != B");
        Assert.AreEqual(enumObj.Generated02, TestEnum.A | TestEnum.B, "EnumBitFieldStruct.Generated02 != (A | B)");

#if NET8_0_OR_GREATER
        InlineArrayStruct inlineArrayObj = new();
        inlineArrayObj[0] = Input;

        Assert.AreEqual(inlineArrayObj.Generated01, Expected01, "InlineArrayStruct.Generated01 != Expected01");
        Assert.AreEqual(inlineArrayObj.Generated02, Expected02, "InlineArrayStruct.Generated02 != Expected02");
        Assert.AreEqual(inlineArrayObj.Generated03, 0, "InlineArrayStruct.Generated03 != 0");
#endif
    }

    [TestMethod]
    public void PaddingFieldTest()
    {
        const uint ExpectedA = 0b110010110011;
        const uint ExpectedB = 0b001101001100;

        // set the fields to test the setters
        PaddingFieldStruct obj = new()
        {
            Generated00 = 0b11,
            //          0b00, // 2 padding bits
            Generated01 = 0b11,
            //          0b1,  // 1 padding bit
            Generated02 = true,
            //          0b00, // 2 padding bits
            Generated03 = TestEnum.A | TestEnum.B,
        };

        Assert.AreEqual(obj.BackingField00, ExpectedA);

        // fill the backing field to test setters
        obj.BackingField00 = 0b111111111111;

        obj.Generated00 = 0b00;
        obj.Generated01 = 0b00;
        obj.Generated02 = false;
        obj.Generated03 = 0;

        Assert.AreEqual(obj.BackingField00, ExpectedB);
    }

    [TestMethod]
    public void EnumFieldTest()
    {
        const TestEnum Expected = TestEnum.A | TestEnum.B;

        // set the fields to test the setters
        EnumBitFieldStruct obj = new()
        {
            Generated01 = TestEnum.A | TestEnum.B | TestEnum.C
        };

        Assert.AreEqual(obj.Generated01, Expected);

        obj.BackingField00 = 0b11111111;

        Assert.AreEqual(obj.Generated01, Expected);
        Assert.AreEqual(obj.Generated02, Expected);
    }

    [TestMethod]
    public void ReadOnlyMemberTest()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public ref partial struct BitFieldReadOnly
        {
            [BitField(""Generated00"", 2)]
            public readonly int BackingField00;

            [BitField(""Generated10"", 2, BitFieldType.Int32)]
            public ReadOnlySpan<byte> BackingField01;

            [BitField(""Generated20"", 2, BitFieldType.Int32)]
            public ReadOnlyMemory<byte> BackingField02;
        }
        ";

        string expected = @"
        public ref partial struct  BitFieldReadOnly
        {
            public  Int32 Generated00 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 0, 2);
            }

            public  Int32 Generated10 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField01, 0, 2);
            }

            public  Int32 Generated20 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField02.Span, 0, 2);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

    [TestMethod]
    public void TypeModifierTest()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public readonly ref partial struct BitFieldReadOnly
        {
            [BitField(""Generated00"", 2)]
            public readonly int BackingField00;

            [BitField(""Generated10"", 2, BitFieldType.Int32)]
            public readonly ReadOnlySpan<byte> BackingField01;

            [BitField(""Generated20"", 2, BitFieldType.Int32)]
            public readonly ReadOnlyMemory<byte> BackingField02;
        }
        ";

        string expected = @"
        public readonly ref partial struct  BitFieldReadOnly
        {
            public  Int32 Generated00 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 0, 2);
            }

            public  Int32 Generated10 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField01, 0, 2);
            }

            public  Int32 Generated20 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField02.Span, 0, 2);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

    [TestMethod]
    public void MemberAttributeTestNet60()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public unsafe partial class BitFieldGeneratorTest
        {
            [BitField(""Generated01"", 2, Modifiers = BitFieldModifiers.Public)]
            [BitField(2)]
            [BitField(""Generated02"", 2, Modifiers = BitFieldModifiers.Private)]
            [BitField(""Generated03"", 2, Modifiers = BitFieldModifiers.Internal)]
            [BitField(""Generated04"", 2, Modifiers = BitFieldModifiers.ReadOnly)]
            [BitField(""Generated05"", 2, Modifiers = BitFieldModifiers.InitOnly)]
            [BitField(""Generated06"", 2, ReverseBitOrder = true)]
            [BitField(""Generated07"", 2, Modifiers = BitFieldModifiers.Protected)]
            [BitField(""Generated08"", 2, Modifiers = BitFieldModifiers.ProtectedInternal)]
            [BitField(""Generated09"", 2, Modifiers = BitFieldModifiers.PrivateProtected)]
            public int BackingField00;

            [BitField(""Generated10"", 2, BitFieldType.SByte)]
            [BitField(2)]
            [BitField(""Generated11"", 2, BitFieldType.Int16)]
            [BitField(""Generated12"", 2, BitFieldType.Int32)]
            [BitField(""Generated13"", 2, BitFieldType.Int64)]
            [BitField(""Generated14"", 2, BitFieldType.Byte)]
            [BitField(""Generated15"", 2, BitFieldType.UInt16)]
            [BitField(""Generated16"", 2, BitFieldType.UInt32)]
            [BitField(""Generated17"", 2, BitFieldType.UInt64)]
            [BitField(""Generated18"", 2, BitFieldType.IntPtr)]
            [BitField(""Generated19"", 2, BitFieldType.UIntPtr)]
            public byte[] BackingField01;
        }
        ";

        string expected = @"
        public unsafe partial class  BitFieldGeneratorTest
        {
            public  Int32 Generated01 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 0, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 0, value, 2);
            }

            private  Int32 Generated02 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 4, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 4, value, 2);
            }

            internal  Int32 Generated03 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 6, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 6, value, 2);
            }

            public  Int32 Generated04 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 8, 2);
            }

            public  Int32 Generated05 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 10, 2);
                init => BitPrimitives.WriteInt32LSB(ref BackingField00, 10, value, 2);
            }

            public  Int32 Generated06 
            {
                get => BitPrimitives.ReadInt32MSB(BackingField00, 12, 2);
                set => BitPrimitives.WriteInt32MSB(ref BackingField00, 12, value, 2);
            }

            protected  Int32 Generated07 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 14, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 14, value, 2);
            }

            protected internal  Int32 Generated08 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 16, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 16, value, 2);
            }

            private protected  Int32 Generated09 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 18, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 18, value, 2);
            }

            public  SByte Generated10 
            {
                get => BitPrimitives.ReadInt8LSB(BackingField01, 0, 2);
                set => BitPrimitives.WriteInt8LSB(BackingField01, 0, value, 2);
            }

            public  Int16 Generated11 
            {
                get => BitPrimitives.ReadInt16LSB(BackingField01, 4, 2);
                set => BitPrimitives.WriteInt16LSB(BackingField01, 4, value, 2);
            }

            public  Int32 Generated12 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField01, 6, 2);
                set => BitPrimitives.WriteInt32LSB(BackingField01, 6, value, 2);
            }

            public  Int64 Generated13 
            {
                get => BitPrimitives.ReadInt64LSB(BackingField01, 8, 2);
                set => BitPrimitives.WriteInt64LSB(BackingField01, 8, value, 2);
            }

            public  Byte Generated14 
            {
                get => BitPrimitives.ReadUInt8LSB(BackingField01, 10, 2);
                set => BitPrimitives.WriteUInt8LSB(BackingField01, 10, value, 2);
            }

            public  UInt16 Generated15 
            {
                get => BitPrimitives.ReadUInt16LSB(BackingField01, 12, 2);
                set => BitPrimitives.WriteUInt16LSB(BackingField01, 12, value, 2);
            }

            public  UInt32 Generated16 
            {
                get => BitPrimitives.ReadUInt32LSB(BackingField01, 14, 2);
                set => BitPrimitives.WriteUInt32LSB(BackingField01, 14, value, 2);
            }

            public  UInt64 Generated17 
            {
                get => BitPrimitives.ReadUInt64LSB(BackingField01, 16, 2);
                set => BitPrimitives.WriteUInt64LSB(BackingField01, 16, value, 2);
            }

            public  IntPtr Generated18 
            {
                get => BitPrimitives.ReadIntPtrLSB(BackingField01, 18, 2);
                set => BitPrimitives.WriteIntPtrLSB(BackingField01, 18, value, 2);
            }

            public  UIntPtr Generated19 
            {
                get => BitPrimitives.ReadUIntPtrLSB(BackingField01, 20, 2);
                set => BitPrimitives.WriteUIntPtrLSB(BackingField01, 20, value, 2);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

    [TestMethod]
    public void MemberAttributeTestNet70()
    {
#if NET7_0_OR_GREATER
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public unsafe partial class BitFieldGeneratorTest
        {
            [BitField(""Generated01"", 2, Modifiers = BitFieldModifiers.Public)]
            [BitField(2)]
            [BitField(""Generated02"", 2, Modifiers = BitFieldModifiers.Private)]
            [BitField(""Generated03"", 2, Modifiers = BitFieldModifiers.Internal)]
            [BitField(""Generated04"", 2, Modifiers = BitFieldModifiers.ReadOnly)]
            [BitField(""Generated05"", 2, Modifiers = BitFieldModifiers.InitOnly)]
            [BitField(""Generated06"", 2, ReverseBitOrder = true)]
            [BitField(""Generated07"", 2, Modifiers = BitFieldModifiers.Required)]
            [BitField(""Generated08"", 2, Modifiers = BitFieldModifiers.Protected)]
            [BitField(""Generated09"", 2, Modifiers = BitFieldModifiers.ProtectedInternal)]
            [BitField(""Generated0A"", 2, Modifiers = BitFieldModifiers.PrivateProtected)]
            public int BackingField00;
        }
        ";

        string expected = @"
        public unsafe partial class  BitFieldGeneratorTest
        {
            public  Int32 Generated01 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 0, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 0, value, 2);
            }

            private  Int32 Generated02 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 4, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 4, value, 2);
            }

            internal  Int32 Generated03 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 6, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 6, value, 2);
            }

            public  Int32 Generated04 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 8, 2);
            }

            public  Int32 Generated05 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 10, 2);
                init => BitPrimitives.WriteInt32LSB(ref BackingField00, 10, value, 2);
            }

            public  Int32 Generated06 
            {
                get => BitPrimitives.ReadInt32MSB(BackingField00, 12, 2);
                set => BitPrimitives.WriteInt32MSB(ref BackingField00, 12, value, 2);
            }

            public required Int32 Generated07 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 14, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 14, value, 2);
            }

            protected Int32 Generated08 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 16, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 16, value, 2);
            }

            protected internal Int32 Generated09 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 18, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 18, value, 2);
            }

            private protected Int32 Generated0A 
            {
                get => BitPrimitives.ReadInt32LSB(BackingField00, 20, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 20, value, 2);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
#endif
    }

    [TestMethod]
    public void BooleanMemberTest()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public unsafe ref partial struct BooleanGeneratorTest
        {
            [BooleanField(""Generated01"")]
            public int BackingField00;

            [BooleanField(""Generated10"")]
            public Span<byte> BackingField01;

            [BooleanField(""Generated20"")]
            public ReadOnlySpan<byte> BackingField02;

            [BooleanField(""Generated30"")]
            public fixed byte BackingField03[4];
        }
        ";

        string expected = @"
        public unsafe ref partial struct  BooleanGeneratorTest
        {
            public  System.Boolean Generated01 
            {
                readonly get => BitPrimitives.ReadInt32LSB(BackingField00, 0, 1) == 1;
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 0, (Int32)(value ? 1 : 0), 1);
            }

            public  System.Boolean Generated10 
            {
                readonly get => BitPrimitives.ReadBitLSB(BackingField01, 0);
                set => BitPrimitives.WriteBitLSB(BackingField01, 0, value);
            }

            public  System.Boolean Generated20 
            {
                get => BitPrimitives.ReadBitLSB(BackingField02, 0);
            }

            public unsafe  System.Boolean Generated30 
            {
                get => BitPrimitives.ReadBitLSB(MemoryMarshal.CreateReadOnlySpan(ref BackingField03[0], 4), 0);
                set => BitPrimitives.WriteBitLSB(MemoryMarshal.CreateSpan(ref BackingField03[0], 4), 0, value);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

    [TestMethod]
    public void EnumMemberTest()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public unsafe ref partial struct EnumGeneratorTest
        {
            [EnumField(""Generated00"", 2, typeof(BitsKit.Tests.TestEnum))]
            [EnumField(""Generated01"", 2, typeof(BitsKit.Tests.TestEnum))]
            public int BackingField00;

            [EnumField(""Generated10"", 2, typeof(BitsKit.Tests.TestEnum))]
            [EnumField(""Generated11"", 2, typeof(BitsKit.Tests.TestEnum))]
            public Span<byte> BackingField01;

            [EnumField(""Generated20"", 2, typeof(BitsKit.Tests.TestEnum))]
            [EnumField(""Generated21"", 2, typeof(BitsKit.Tests.TestEnum))]
            public ReadOnlySpan<byte> BackingField02;

            [EnumField(""Generated30"", 2, typeof(BitsKit.Tests.TestEnum))]
            [EnumField(""Generated31"", 2, typeof(BitsKit.Tests.TestEnum))]
            public fixed byte BackingField03[4];
        }
        ";

        string expected = @"
        public unsafe ref partial struct  EnumGeneratorTest
        {
            public  BitsKit.Tests.TestEnum Generated00 
            {
                readonly get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(BackingField00, 0, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 0, (Int32)value, 2);
            }

            public  BitsKit.Tests.TestEnum Generated01 
            {
                readonly get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(BackingField00, 2, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 2, (Int32)value, 2);
            }

            public  BitsKit.Tests.TestEnum Generated10 
            {
                readonly get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(BackingField01, 0, 2);
                set => BitPrimitives.WriteInt32LSB(BackingField01, 0, (Int32)value, 2);
            }

            public  BitsKit.Tests.TestEnum Generated11 
            {
                readonly get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(BackingField01, 2, 2);
                set => BitPrimitives.WriteInt32LSB(BackingField01, 2, (Int32)value, 2);
            }

            public  BitsKit.Tests.TestEnum Generated20 
            {
                get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(BackingField02, 0, 2);
            }

            public  BitsKit.Tests.TestEnum Generated21 
            {
                get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(BackingField02, 2, 2);
            }

            public unsafe  BitsKit.Tests.TestEnum Generated30 
            {
                get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(MemoryMarshal.CreateReadOnlySpan(ref BackingField03[0], 4), 0, 2);
                set => BitPrimitives.WriteInt32LSB(MemoryMarshal.CreateSpan(ref BackingField03[0], 4), 0, (Int32)value, 2);
            }

            public unsafe  BitsKit.Tests.TestEnum Generated31 
            {
                get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(MemoryMarshal.CreateReadOnlySpan(ref BackingField03[0], 4), 2, 2);
                set => BitPrimitives.WriteInt32LSB(MemoryMarshal.CreateSpan(ref BackingField03[0], 4), 2, (Int32)value, 2);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

    [TestMethod]
    public void IntegerConversionTest()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        public ref partial struct BitFieldIntegerConversion
        {
            [BitField(""Generated00"", 2, BitFieldType.Byte)]
            public int BackingField00;

            [BitField(""Generated10"", 2, BitFieldType.Int32)]
            public Span<byte> BackingField01;
        }
        ";

        string expected = @"
        public ref partial struct  BitFieldIntegerConversion
        {
            public  Byte Generated00 
            {
                readonly get => (Byte)BitPrimitives.ReadInt32LSB(BackingField00, 0, 2);
                set => BitPrimitives.WriteInt32LSB(ref BackingField00, 0, (Int32)value, 2);
            }

            public  Int32 Generated10 
            {
                readonly get => BitPrimitives.ReadInt32LSB(BackingField01, 0, 2);
                set => BitPrimitives.WriteInt32LSB(BackingField01, 0, value, 2);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

#if NET8_0_OR_GREATER

    [TestMethod]
    public void InlineArrayTest()
    {
        string source = @"
        [BitObject(BitOrder.LeastSignificant)]
        [InlineArray(length: 10)]
        public partial struct BitFieldInlineArray
        {
            [BitField(""Generated00"", 2)]
            [BitField(""Generated01"", 2, BitFieldType.Byte)]
            [EnumField(""Generated02"", 2, typeof(BitsKit.Tests.TestEnum))]
            [BooleanField(""Generated03"")]
            public int BackingField00;
        }
        ";

        string expected = @"
        public partial struct  BitFieldInlineArray
        {
            public  Int32 Generated00 
            {
                 get => BitPrimitives.ReadInt32LSB(MemoryMarshal.Cast<int, byte>(this), 0, 2);
                 set => BitPrimitives.WriteInt32LSB(MemoryMarshal.Cast<int, byte>(this), 0, value, 2);
            }

            public  Byte Generated01 
            {
                 get => BitPrimitives.ReadUInt8LSB(MemoryMarshal.Cast<int, byte>(this), 2, 2);
                 set => BitPrimitives.WriteUInt8LSB(MemoryMarshal.Cast<int, byte>(this), 2, value, 2);
            }

            public  BitsKit.Tests.TestEnum Generated02 
            {
                 get => (BitsKit.Tests.TestEnum)BitPrimitives.ReadInt32LSB(MemoryMarshal.Cast<int, byte>(this), 4, 2);
                 set => BitPrimitives.WriteInt32LSB(MemoryMarshal.Cast<int, byte>(this), 4, (Int32)value, 2);
            }

            public  System.Boolean Generated03 
            {
                 get => BitPrimitives.ReadBitLSB(MemoryMarshal.Cast<int, byte>(this), 6);
                 set => BitPrimitives.WriteBitLSB(MemoryMarshal.Cast<int, byte>(this), 6, value);
            }
        }
        ";

        string? sourceOutput = GenerateSourceAndTest(source, new BitObjectGenerator());

        Assert.IsTrue(Helpers.StrEqualExWhiteSpace(sourceOutput, expected));
    }

#endif

    private static string? GenerateSourceAndTest(string source, IIncrementalGenerator generator)
    {
        var references = AppDomain.CurrentDomain.GetAssemblies()
                                  .Where(assembly => !assembly.IsDynamic)
                                  .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
                                  .Cast<MetadataReference>();

        CSharpCompilation compilation = CSharpCompilation.Create("compilation",
                                        [CSharpSyntaxTree.ParseText(Helpers.GeneratorTestHeader + source)],
                                        references,
                                        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));

        GeneratorDriver driver = CSharpGeneratorDriver
           .Create(generator)
           .RunGeneratorsAndUpdateCompilation(compilation, out Compilation? outputCompilation, out ImmutableArray<Diagnostic> diagnostics);

        var diag = outputCompilation.GetDiagnostics();

        Assert.IsTrue(diagnostics.IsEmpty); // there were no diagnostics created by the generators
        Assert.AreEqual(outputCompilation.SyntaxTrees.Count(), 2); // we have two syntax trees, the original 'user' provided one, and the one added by the generator
        Assert.IsTrue(outputCompilation.GetDiagnostics().IsEmpty); // verify the compilation with the added source has no diagnostics

        GeneratorDriverRunResult runResult = driver.GetRunResult();

        Assert.AreEqual(runResult.GeneratedTrees.Length, 1);
        Assert.IsTrue(runResult.Diagnostics.IsEmpty);

        GeneratorRunResult generatorResult = runResult.Results[0];
        Assert.AreEqual(generatorResult.Generator.GetGeneratorType(), typeof(BitObjectGenerator));
        Assert.IsTrue(generatorResult.Diagnostics.IsEmpty);
        Assert.AreEqual(generatorResult.GeneratedSources.Length, 1);
        Assert.IsTrue(generatorResult.Exception is null);

        string sourceOutput = generatorResult.GeneratedSources[0].SourceText.ToString();

        return TruncateUsings(sourceOutput);
    }

    private static string? TruncateUsings(string? source)
    {
        if (string.IsNullOrEmpty(source))
            return source;

        int index = source.LastIndexOf("using");
        int eol = source.IndexOf('\n', index);

        return source[eol..].TrimStart();
    }

    /// <summary>Loads the BitsKit.Generator assembly into the current AppDomain</summary>
    [BitObject(BitOrder.LeastSignificant)]
    private readonly partial struct BitsKitGeneratorStub { }
}
