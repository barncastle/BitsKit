using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

#if NET7_0_OR_GREATER
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
#endif

namespace BitsKit.Tests;

public static class Helpers
{
    private static readonly Regex RemoveWhiteSpaceRegex = new("\\s", RegexOptions.Compiled);

    public const string GeneratorTestHeader = @"
    #pragma warning disable IDE0005 // Using directive is unnecessary
    #pragma warning disable IDE0005_gen // Using directive is unnecessary
    #pragma warning disable CS8019  // Unnecessary using directive.
    #pragma warning disable IDE0161 // Convert to file-scoped namespace
    
    using System;
    using BitsKit;
    using BitsKit.BitFields;
    ";

    public static ulong ReadBitsLSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ulong result = 0;
        for (int i = bitOffset; i < bitOffset + bitCount; i++)
            result |= (ulong)((source[i >> 3] >> (i & 7)) & 0x1) << (i - bitOffset);

        return result;
    }

    public static ulong ReadBitsMSB(ReadOnlySpan<byte> source, int bitOffset, int bitCount)
    {
        ulong result = 0;
        for (int i = bitOffset; i < bitOffset + bitCount; i++)
            result |= (ulong)((source[i >> 3] >> (7 - (i & 7))) & 0x1) << (bitCount - 1 - (i - bitOffset));

        return result;
    }

    public static void WriteBitsLSB(Span<byte> source, int bitOffset, ulong value, int bitCount)
    {
        for (int i = bitOffset; i < bitOffset + bitCount; i++)
        {
            if (((value >> (i - bitOffset)) & 1) != 0)
                source[i >> 3] |= (byte)(1 << (i & 7));
            else
                source[i >> 3] &= (byte)~(1 << (i & 7));
        }
    }

    public static void WriteBitsMSB(Span<byte> source, int bitOffset, ulong value, int bitCount)
    {
        for (int i = bitOffset; i < bitOffset + bitCount; i++)
        {
            if (((value >> (bitCount - 1 - (i - bitOffset))) & 1) != 0)
                source[i >> 3] |= (byte)(1 << (7 - (i & 7)));
            else
                source[i >> 3] &= (byte)~(1 << (7 - (i & 7)));
        }
    }

    public static string ToBinary<T>(T value) where T : unmanaged
    {
        int bitSize = Unsafe.SizeOf<T>() * 8;
        long int64 = Convert.ToInt64(value);

        return Convert.ToString(int64, 2).PadLeft(bitSize, '0');
    }

    public static IEnumerable<object[]> GetTestOffsetParams()
    {
        return Enumerable.Range(0, 16).Select(size => new object[] { size });
    }

    public static IEnumerable<object[]> GetTestOffsetAndSizeParams()
    {
        // all bit offsets up to two bytes
        // covering aligned, unaligned and crossing byte boundaries
        var bitOffsets = Enumerable.Range(0, 16);

        // generate bitsizes for each byte position +/- 2 within a ulong
        // covering min, max, aligned and unaligned for each integral type
        // i.e. [0,1,2,6,7,8,9,10..64]
        var bitSizes = Enumerable
            .Range(0, 8)
            .SelectMany(s => new[]
            {
                s * 8 - 2,
                s * 8 - 1,
                s * 8 - 0,
                s * 8 + 1,
                s * 8 + 2,
            })
            .Where(size => size is > 0 and <= 64);

        return bitOffsets.SelectMany(offset =>
        {
            return bitSizes
                   .Where(size => offset + size <= 64)
                   .Select(size => new object[] { offset, size });
        });
    }

    public static bool StrEqualExWhiteSpace(string? s1, string? s2)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            return s1 == s2;

        string normalisedS1 = RemoveWhiteSpaceRegex.Replace(s1, "");
        string normalisedS2 = RemoveWhiteSpaceRegex.Replace(s2, "");

        return string.Equals(normalisedS1, normalisedS2);
    }
}
