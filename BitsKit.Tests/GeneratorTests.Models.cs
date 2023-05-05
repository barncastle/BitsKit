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

    public int IntValue00 => MemoryMarshal.Read<int>(BackingField00.Span);
    public int IntValue01 => MemoryMarshal.Read<int>(BackingField01.Span);
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

    public int IntValue00 => MemoryMarshal.Read<int>(BackingField00);
    public int IntValue01 => MemoryMarshal.Read<int>(BackingField01);
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
public unsafe ref partial struct BooleanGeneratorTest
{
    [BitField("Generated01", 2, BitFieldType.Boolean)]
    public int BackingField00;       

    [BitField("Generated10", 2, BitFieldType.Boolean)]
    public Span<byte> BackingField01;

    [BitField("Generated20", 2, BitFieldType.Boolean)]
    public ReadOnlySpan<byte> BackingField02;

    [BitField("Generated30", 2, BitFieldType.Boolean)]
    public fixed byte BackingField03[4];

    [BitField("Generated41", 2, BitFieldType.Boolean)]
    public byte BackingField04;

    [BitField("Generated51", 2, BitFieldType.Boolean)]
    public sbyte BackingField05;
}
