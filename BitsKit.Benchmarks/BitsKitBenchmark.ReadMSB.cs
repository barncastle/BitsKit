using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt1")]
    public int Read1BitMSB_Loop()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt1")]
    public int Read1BitMSB_BitsKit()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitMSB(ReadBuffer, offset++);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt4")]
    public int Read4BitMSB_Loop()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt4")]
    public int Read4BitMSB_BitsKit()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8MSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt16")]
    public int Read16BitMSB_Loop()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt16")]
    public int Read16BitMSB_BitsKit()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16MSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt32")]
    public int Read32BitMSB_Loop()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt32")]
    public int Read32BitMSB_BitsKit()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32MSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "Loop", "UInt64")]
    public int Read64BitMSB_Loop()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsMSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadMSB", "BitsKit", "UInt64")]
    public int Read64BitMSB_BitsKit()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64MSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }
}
