using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt4")]
    public int Write4BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsMSB(WriteBuffer, i, 0xFF, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt4")]
    public int Write4BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt8MSB(WriteBuffer, i, 0xFF, 4);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt16")]
    public int Write16BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsMSB(WriteBuffer, i, 0xFFFF, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt16")]
    public int Write16BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt16MSB(WriteBuffer, i, 0xFFFF, 16);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt32")]
    public int Write32BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsMSB(WriteBuffer, i, uint.MaxValue, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt32")]
    public int Write32BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt32MSB(WriteBuffer, i, uint.MaxValue, 32);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt64")]
    public int Write64BitMSB_Loop()
    {
        for (int i = 0; i < 8; i++)
            Helpers.WriteBitsMSB(WriteBuffer, i, ulong.MaxValue, 64);

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt64")]
    public int Write64BitMSB_BitsKit()
    {
        for (int i = 0; i < 8; i++)
            BitPrimitives.WriteUInt64MSB(WriteBuffer, i, ulong.MaxValue, 64);

        return 0;
    }
}
