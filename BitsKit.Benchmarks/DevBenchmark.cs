using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

#pragma warning disable

namespace BitsKit.Benchmarks;

[MemoryDiagnoser]
public class DevBenchmark
{
    //[Params(1, 2, 7, 8)]
    [Params(62, 63, 64)]
    public int BitCount { get; set; }

    private readonly byte[] Data = new byte[]
    {
        239, 128, 162, 245, 92, 32, 202, 103, 218,
        251, 248, 150, 234, 92, 161, 192, 27, 103
    };

    private int[][] OffsetSizes;
    private ulong v1;

    [GlobalSetup]
    public void GlobalSetup()
    {
        OffsetSizes = GetBitOffsetSizeParams<ulong>().ToArray();
        v1 = Unsafe.ReadUnaligned<ulong>(ref Data[3]);
    }

    [Benchmark]
    public int WriteBitsPrimtives()
    {
        ulong u1 = 0;
        BitPrimitives.WriteUInt64LSB(ref u1, 0, v1, BitCount);

        return 0;
    }

    [Benchmark]
    public int WriteBitsLoop()
    {
        Span<byte> u1 = stackalloc byte[8];
        Helpers.WriteBitsLSB(u1, 0, v1, BitCount);

        return 0;
    }

    private static IEnumerable<int[]> GetBitOffsetSizeParams<T>() where T : unmanaged
    {
        var bytes = Unsafe.SizeOf<T>();
        var bits = bytes * 8;

        return Enumerable
            .Range(0, 16) // arbitary n bit sizes [0..15]
            .SelectMany(size =>
                Enumerable.Range(0, bytes + 1).SelectMany(offset => // n bytes in T
                {
                    // generate every bit offset +/-2 of
                    // the byte offset for the size
                    return new[]
                    {
                        new[] { offset * 8 - 2, size },
                        new[] { offset * 8 - 1, size },
                        new[] { offset * 8 + 0, size },
                        new[] { offset * 8 + 1, size },
                        new[] { offset * 8 + 2, size }
                    };
                }))
            .Where(o => o[0] >= 0 && o[1] > 0 && o[0] + o[1] <= bits); // filter to valid pairs
    }
}
