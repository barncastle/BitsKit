using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt4")]
    public int Write4BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsLSB(WriteBuffer, i, 0xFF, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt4")]
    public int Write4BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt8LSB(WriteBuffer, i, 0xFF, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt16")]
    public int Write16BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsLSB(WriteBuffer, i, 0xFFFF, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt16")]
    public int Write16BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt16LSB(WriteBuffer, i, 0xFFFF, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt32")]
    public int Write32BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsLSB(WriteBuffer, i, uint.MaxValue, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt32")]
    public int Write32BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt32LSB(WriteBuffer, i, uint.MaxValue, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt64")]
    public int Write64BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsLSB(WriteBuffer, i, ulong.MaxValue, 64);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt64")]
    public int Write64BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt64LSB(WriteBuffer, i, ulong.MaxValue, 64);

        return 0;
    }
}
