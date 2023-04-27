using System;

namespace BitsKit.Generator;

/// <summary>
/// Enumeration to differentiate the backing field type
/// </summary>
internal enum BackingType
{
    Integral,
    Memory,
    Span,
    Pointer,

    Invalid
}

/// <summary>
/// An enumeration of the possible bit-field types
/// </summary>
internal enum BitFieldType
{
    SByte = 1,
    Byte,
    Int16,
    UInt16,
    Int32,
    UInt32,
    Int64,
    UInt64,
    IntPtr,
    UIntPtr,

    /// <summary>
    /// Blocks out a set of bits
    /// </summary>
    Padding = 0xFF
}

/// <summary>
/// Dictates the direction of the bits respresented in a unit
/// </summary>
internal enum BitOrder
{
    /// <summary>
    /// "Low-Order" or "Right-Most" bit
    /// </summary>
    LeastSignificant,
    /// <summary>
    /// "High-Order" or "Left-Most" bit
    /// </summary>
    MostSignificant
}

/// <summary>
/// Modifiers for the generated bit-field property
/// </summary>
[Flags]
internal enum BitFieldModifiers
{
    None = 0,
    /// <summary>
    /// Generates a public accessor
    /// </summary>
    Public = 0x01,
    /// <summary>
    /// Generates an internal accessor
    /// </summary>
    Internal = 0x02,
    /// <summary>
    /// Generates a private accessor
    /// </summary>
    Private = 0x04,
    /// <summary>
    /// Only generates a getter
    /// </summary>
    ReadOnly = 0x08,
    /// <summary>
    /// Generates an init only setter
    /// </summary>
    InitOnly = 0x10,
    /// <summary>
    /// Generates a required modifier
    /// </summary>
    Required = 0x20,

    AccessorMask = Public | Internal | Private,
    SetterMask = ReadOnly | InitOnly
}
