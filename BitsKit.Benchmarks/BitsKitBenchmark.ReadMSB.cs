using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt4")]
    public int Read4BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsMSB(ReadBuffer, i, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt4")]
    public int Read4BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, i, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt16")]
    public int Read16BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsMSB(ReadBuffer, i, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt16")]
    public int Read16BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, i, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt32")]
    public int Read32BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsMSB(ReadBuffer, i, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt32")]
    public int Read32BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, i, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt64")]
    public int Read64BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            _ = Helpers.ReadBitsMSB(ReadBuffer, i, 64);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt64")]
    public int Read64BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, i, 64);

        return 0;
    }
}
