using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt1")]
    public int Read1BitLSB_Loop()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt1")]
    public int Read1BitLSB_BitsKit()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
            _ = BitPrimitives.ReadBitLSB(ReadBuffer, offset++);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt4")]
    public int Read4BitLSB_Loop()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt4")]
    public int Read4BitLSB_BitsKit()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt8LSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt16")]
    public int Read16BitLSB_Loop()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt16")]
    public int Read16BitLSB_BitsKit()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt16LSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt32")]
    public int Read32BitLSB_Loop()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt32")]
    public int Read32BitLSB_BitsKit()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt32LSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "Loop", "UInt64")]
    public int Read64BitLSB_Loop()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
            _ = Helpers.ReadBitsLSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesReadLSB", "BitsKit", "UInt64")]
    public int Read64BitLSB_BitsKit()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
            _ = BitPrimitives.ReadUInt64LSB(ReadBuffer, offset++, bitCount);
        }

        return 0;
    }
}
