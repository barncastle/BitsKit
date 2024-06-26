﻿using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BitsKit.Generator;

internal static class Extensions
{
    /// <summary>
    /// Prefixes indents to a line before appending it to the end of the StringBuilder object
    /// </summary>
    public static StringBuilder AppendIndentedLine(this StringBuilder builder, byte level, string text, params object?[]? args)
    {
        builder.Append(' ', level * 4);

        if (args is { Length: > 0 })
            builder.AppendLine(string.Format(text, args));
        else
            builder.AppendLine(text);

        return builder;
    }

    /// <summary>
    /// Truncates a StringBuilder from the last newline
    /// </summary>
    public static StringBuilder RemoveLastLine(this StringBuilder builder)
    {
        int i;
        for (i = builder.Length - 1; i != 0; i--)
        {
            // find the last newline char
            if (builder[i] == '\n')
            {
                // remove the carriage return too
                if (i != 0 && builder[i - 1] == '\r')
                    i--;

                break;
            }
        }

        builder.Length = i;

        return builder;
    }

    /// <summary>
    /// Returns if a TypeDeclaration is a struct or not
    /// </summary>
    public static bool IsStruct(this TypeDeclarationSyntax typeDeclaration)
    {
        return (SyntaxKind)typeDeclaration.RawKind is SyntaxKind.StructDeclaration or SyntaxKind.RecordStructDeclaration;
    }

    public static bool IsSupportedIntegralType(this ITypeSymbol? type) => type?.SpecialType switch
    {
        SpecialType.System_Byte or
        SpecialType.System_SByte or
        SpecialType.System_Int16 or
        SpecialType.System_UInt16 or
        SpecialType.System_Int32 or
        SpecialType.System_UInt32 or
        SpecialType.System_Int64 or
        SpecialType.System_UInt64 or
        SpecialType.System_IntPtr or
        SpecialType.System_UIntPtr => true,
        _ => false,
    };

    public static BitFieldType ToBitFieldType(this SpecialType type) => type switch
    {
        SpecialType.System_SByte => BitFieldType.SByte,
        SpecialType.System_Byte => BitFieldType.Byte,
        SpecialType.System_Int16 => BitFieldType.Int16,
        SpecialType.System_UInt16 => BitFieldType.UInt16,
        SpecialType.System_Int32 => BitFieldType.Int32,
        SpecialType.System_UInt32 => BitFieldType.UInt32,
        SpecialType.System_Int64 => BitFieldType.Int64,
        SpecialType.System_UInt64 => BitFieldType.UInt64,
        SpecialType.System_IntPtr => BitFieldType.IntPtr,
        SpecialType.System_UIntPtr => BitFieldType.UIntPtr,
        _ => throw new NotSupportedException()
    };

    public static string ToIntegralName(this BitFieldType type) => type switch
    {
        BitFieldType.SByte => "Int8",
        BitFieldType.Byte => "UInt8",
        BitFieldType.Int16 => "Int16",
        BitFieldType.UInt16 => "UInt16",
        BitFieldType.Int32 => "Int32",
        BitFieldType.UInt32 => "UInt32",
        BitFieldType.Int64 => "Int64",
        BitFieldType.UInt64 => "UInt64",
        BitFieldType.IntPtr => "IntPtr",
        BitFieldType.UIntPtr => "UIntPtr",
        BitFieldType.Boolean => "Boolean",
        _ => throw new NotSupportedException()
    };

    public static string ToShortName(this BitOrder order) => order switch
    {
        BitOrder.LeastSignificant => "LSB",
        BitOrder.MostSignificant => "MSB",
        _ => throw new NotSupportedException()
    };
}
