using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt1")]
    public int Write1BitLSB_Loop()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 0, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 0, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 0, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, 0, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt1")]
    public int Write1BitLSB_BitsKit()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, false);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, false);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, false);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitLSB(WriteBuffer, offset++, false);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt4")]
    public int Write4BitLSB_Loop()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, byte.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt4")]
    public int Write4BitLSB_BitsKit()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8LSB(WriteBuffer, offset++, byte.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt16")]
    public int Write16BitLSB_Loop()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt16")]
    public int Write16BitLSB_BitsKit()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16LSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt32")]
    public int Write32BitLSB_Loop()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, uint.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt32")]
    public int Write32BitLSB_BitsKit()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32LSB(WriteBuffer, offset++, uint.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "Loop", "UInt64")]
    public int Write64BitLSB_Loop()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsLSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteLSB", "BitsKit", "UInt64")]
    public int Write64BitLSB_BitsKit()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64LSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
        }

        return 0;
    }
}
