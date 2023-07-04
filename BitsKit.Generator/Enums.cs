using System;

namespace BitsKit.Generator;

/// <summary>
/// An enumeration of the different backing field types
/// </summary>
internal enum BackingFieldType
{
    Integral,
    Memory,
    Span,
    Pointer,

    Invalid = 0xFF
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
    Boolean,

    /// <summary>
    /// Blocks out a set of bits
    /// </summary>
    Padding = 0xFF
}

/// <summary>
/// An enumeration listing the possible directions of bits in a unit
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
/// An enumeration listing all of the options to modify bit-field generation
/// </summary>
[Flags]
internal enum BitFieldModifiers
{
    /// <summary>
    /// Generates a public settable property
    /// </summary>
    None = 0,
    /// <summary>
    /// Specifies the property is public
    /// </summary>
    Public = 0x01,
    /// <summary>
    /// Specifies the property is private
    /// </summary>
    Private = 0x02,
    /// <summary>
    /// Specifies the property is protected
    /// </summary>
    Protected = 0x04,
    /// <summary>
    /// Specifies the property is internal
    /// </summary>
    Internal = 0x08,
    /// <summary>
    /// Specifies the property is readonly
    /// </summary>
    ReadOnly = 0x10,
    /// <summary>
    /// Specifies the property is init only
    /// </summary>
    InitOnly = 0x20,
    /// <summary>
    /// Specifies the property is required
    /// </summary>
    Required = 0x40,

    /// <summary>
    /// Specifies the property is protected internal
    /// </summary>
    ProtectedInternal = Protected | Internal,

    /// <summary>
    /// Specifies the property is private protected
    /// </summary>
    PrivateProtected = Private | Protected,

    AccessorMask = Public | Private | Protected | Internal,
    SetterMask = ReadOnly | InitOnly
}
