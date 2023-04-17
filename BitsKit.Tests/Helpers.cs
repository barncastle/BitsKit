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
}
