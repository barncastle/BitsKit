using BenchmarkDotNet.Attributes;
using BitsKit.Primitives;
using BitsKit.Tests;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt1")]
    public int Write1BitMSB_Loop()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 0, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 0, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 0, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 1, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, 0, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt1")]
    public int Write1BitMSB_BitsKit()
    {
        const int bitCount = 1;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, false);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, false);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, false);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, true);
            BitPrimitives.WriteBitMSB(WriteBuffer, offset++, false);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt4")]
    public int Write4BitMSB_Loop()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, byte.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt4")]
    public int Write4BitMSB_BitsKit()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MinValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MaxValue, bitCount);
            BitPrimitives.WriteUInt8MSB(WriteBuffer, offset++, byte.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt16")]
    public int Write16BitMSB_Loop()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt16")]
    public int Write16BitMSB_BitsKit()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MaxValue, bitCount);
            BitPrimitives.WriteUInt16MSB(WriteBuffer, offset++, ushort.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt32")]
    public int Write32BitMSB_Loop()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, uint.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt32")]
    public int Write32BitMSB_BitsKit()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MinValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MaxValue, bitCount);
            BitPrimitives.WriteUInt32MSB(WriteBuffer, offset++, uint.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "Loop", "UInt64")]
    public int Write64BitMSB_Loop()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            Helpers.WriteBitsMSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("BitPrimitivesWriteMSB", "BitsKit", "UInt64")]
    public int Write64BitMSB_BitsKit()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;

        int offset = 0;
        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MaxValue, bitCount);
            BitPrimitives.WriteUInt64MSB(WriteBuffer, offset++, ulong.MinValue, bitCount);
        }

        return 0;
    }
}
