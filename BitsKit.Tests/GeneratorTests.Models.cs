using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BitsKit.BitFields;

namespace BitsKit.Tests;

[BitObject(BitOrder.LeastSignificant)]
[StructLayout(LayoutKind.Sequential)]
public partial struct SequentialBitFieldStruct
{
    [BitField("Generated01", 2)]
    [BitField("Generated02", 2)]
    public int BackingField00;

    [BitField("Generated03", 2)]
    [BitField("Generated04", 2)]
    public int BackingField01;

    [BitField("Padding01", 2)]
    [BitField(2)]
    [BitField("Padding02", 2)]
    public int BackingFieldPadding;

    [BitField("Generated05", 2)]
    [BitField(2)]
    [BitField("Generated06", 2)]
    public nint BackingFieldIntPtr;

    [BitField("Generated07", 2)]
    [BitField(2)]
    [BitField("Generated08", 2)]
    public nuint BackingFieldUIntPtr;
}

[BitObject(BitOrder.LeastSignificant)]
[StructLayout(LayoutKind.Explicit)]
public partial struct ExplicitBitFieldStruct
{
    [BitField("Generated01", 2)]
    [BitField("Generated02", 2)]
    [FieldOffset(0)]
    public int BackingField00;

    [BitField("Generated03", 2)]
    [BitField("Generated04", 2)]
    [FieldOffset(0)]
    public ulong BackingField01;
}

[BitObject(BitOrder.LeastSignificant)]
public partial record struct BitFieldRecordStruct
{
    [BitField("Generated01", 2)]
    [BitField("Generated02", 2)]
    public int BackingField00;

    [BitField("Generated03", 2)]
    [BitField("Generated04", 2)]
    public int BackingField01;
}

[BitObject(BitOrder.LeastSignificant)]
public partial struct BitFieldMemoryStruct
{
    [BitField("Generated01", 2, BitFieldType.Int32)]
    [BitField("Generated02", 2, BitFieldType.Int16)]
    public Memory<byte> BackingField00;

    [BitField("Generated03", 2, BitFieldType.Int32)]
    [BitField("Generated04", 2, BitFieldType.Int16)]
    public ReadOnlyMemory<byte> BackingField01;

    public readonly int IntValue00 => MemoryMarshal.Read<int>(BackingField00.Span);
    public readonly int IntValue01 => MemoryMarshal.Read<int>(BackingField01.Span);
}

[BitObject(BitOrder.LeastSignificant)]
public ref partial struct BitFieldRefStruct
{
    [BitField("Generated01", 2, BitFieldType.Int32)]
    [BitField("Generated02", 2, BitFieldType.Int16)]
    public Span<byte> BackingField00;

    [BitField("Generated03", 2, BitFieldType.Int32)]
    [BitField("Generated04", 2, BitFieldType.Int16)]
    public ReadOnlySpan<byte> BackingField01;

    public readonly int IntValue00 => MemoryMarshal.Read<int>(BackingField00);
    public readonly int IntValue01 => MemoryMarshal.Read<int>(BackingField01);
}

[BitObject(BitOrder.LeastSignificant)]
public unsafe partial struct BitFieldFixedStruct
{
    [BitField("Generated01", 2, BitFieldType.Int32)]
    [BitField("Generated02", 2, BitFieldType.Int16)]
    public fixed byte BackingField00[4];

    [BitField("Generated03", 2, BitFieldType.Int32)]
    [BitField("Generated04", 2, BitFieldType.Int16)]
    public int BackingField01;

    public int IntValue00
    {
        get => Unsafe.ReadUnaligned<int>(ref BackingField00[0]);
        set => Unsafe.WriteUnaligned(ref BackingField00[0], value);
    }
}

[BitObject(BitOrder.LeastSignificant)]
[StructLayout(LayoutKind.Sequential)]
public partial struct EnumBitFieldStruct
{
    [EnumField("Generated01", 2, typeof(TestEnum))]
    [EnumField("Generated02", 2, typeof(TestEnum))]
    public uint BackingField00;

    [EnumField("Generated03", 2, typeof(TestEnum))]
    [EnumField("Generated04", 2, typeof(TestEnum))]
    public uint BackingField01;

    [EnumField("Padding01", 2, typeof(TestEnum))]
    [BitField(2)]
    [EnumField("Padding02", 2, typeof(TestEnum))]
    public uint BackingFieldPadding;

    [EnumField("Generated07", 2, typeof(TestEnum))]
    [BitField(2)]
    [EnumField("Generated08", 2, typeof(TestEnum))]
    public nuint BackingFieldUIntPtr;
}

[BitObject(BitOrder.LeastSignificant)]
[StructLayout(LayoutKind.Sequential)]
public partial struct PaddingFieldStruct
{
    [BitField("Generated00", 2)]
    [BitField(2)]
    [BitField("Generated01", 2)]
    [BooleanField]
    [BooleanField("Generated02")]
    [EnumField(2)]
    [EnumField("Generated03", 2, typeof(TestEnum))]
    public uint BackingField00;
}

[BitObject(BitOrder.LeastSignificant)]
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct ReadOnlyStruct
{
    [BitField("Generated01", 2)]
    [BitField("Generated02", 2)]
    [FieldOffset(0)]
    public readonly int BackingField00;

    [BitField("Generated03", 2)]
    [BitField("Generated04", 2)]
    [FieldOffset(0)]
    public readonly ulong BackingField01;
}

[Flags]
public enum TestEnum
{
    A = 1,
    B = 2,
    C = 4,
}
