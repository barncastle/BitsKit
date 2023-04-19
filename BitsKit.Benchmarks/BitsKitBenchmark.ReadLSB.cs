using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt4")]
    public int Read4BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsLSB(ReadBuffer, i, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt4")]
    public int Read4BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, i, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt16")]
    public int Read16BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsLSB(ReadBuffer, i, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt16")]
    public int Read16BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, i, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt32")]
    public int Read32BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsLSB(ReadBuffer, i, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt32")]
    public int Read32BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, i, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt64")]
    public int Read64BitLSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsLSB(ReadBuffer, i, 64);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt64")]
    public int Read64BitLSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, i, 64);

        return 0;
    }
}
