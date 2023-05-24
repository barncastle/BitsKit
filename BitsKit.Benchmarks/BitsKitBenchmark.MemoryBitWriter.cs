using BenchmarkDotNet.Attributes;
using BitsKit.IO;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    [Benchmark]
    [BenchmarkCategory("MemoryBitWriter", "UInt1")]
    public int MemoryBitWriterBit()
    {
        var maxNumIterations = BufferSize;
        var writer = new MemoryBitWriter(WriteBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            writer.WriteBitMSB(false);
            writer.WriteBitMSB(true);
            writer.WriteBitMSB(false);
            writer.WriteBitMSB(true);
            writer.WriteBitMSB(false);
            writer.WriteBitMSB(true);
            writer.WriteBitMSB(false);
            writer.WriteBitMSB(true);
        }

        return 0;
    }
    [Benchmark]
    [BenchmarkCategory("MemoryBitWriter", "UInt4")]
    public int MemoryBitWriterUInt04()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;
        var writer = new MemoryBitWriter(WriteBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            writer.WriteUInt8MSB((byte)numIterations, 3);
            writer.WriteUInt8MSB((byte)numIterations, 4);
            writer.WriteUInt8MSB((byte)numIterations, 3);
            writer.WriteUInt8MSB((byte)numIterations, 4);
            writer.WriteUInt8MSB((byte)numIterations, 3);
            writer.WriteUInt8MSB((byte)numIterations, 4);
            writer.WriteUInt8MSB((byte)numIterations, 3);
            writer.WriteUInt8MSB((byte)numIterations, 4);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitWriter", "UInt8")]
    public int MemoryBitWriterUInt08()
    {
        const int bitCount = 8;
        int maxNumIterations = BufferSize / bitCount;
        var writer = new MemoryBitWriter(WriteBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            writer.WriteUInt8MSB((byte)numIterations, 1);
            writer.WriteUInt8MSB((byte)numIterations, 2);
            writer.WriteUInt8MSB((byte)numIterations, 3);
            writer.WriteUInt8MSB((byte)numIterations, 4);
            writer.WriteUInt8MSB((byte)numIterations, 5);
            writer.WriteUInt8MSB((byte)numIterations, 6);
            writer.WriteUInt8MSB((byte)numIterations, 7);
            writer.WriteUInt8MSB((byte)numIterations, 8);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitWriter", "UInt16")]
    public int MemoryBitWriterUInt16()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;
        var writer = new MemoryBitWriter(WriteBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            writer.WriteUInt16MSB((ushort)numIterations, 15);
            writer.WriteUInt16MSB((ushort)numIterations, 16);
            writer.WriteUInt16MSB((ushort)numIterations, 15);
            writer.WriteUInt16MSB((ushort)numIterations, 16);
            writer.WriteUInt16MSB((ushort)numIterations, 15);
            writer.WriteUInt16MSB((ushort)numIterations, 16);
            writer.WriteUInt16MSB((ushort)numIterations, 15);
            writer.WriteUInt16MSB((ushort)numIterations, 16);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitWriter", "UInt32")]
    public int MemoryBitWriterUInt32()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;
        var writer = new MemoryBitWriter(WriteBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            writer.WriteUInt32MSB((uint)numIterations, 31);
            writer.WriteUInt32MSB((uint)numIterations, 32);
            writer.WriteUInt32MSB((uint)numIterations, 31);
            writer.WriteUInt32MSB((uint)numIterations, 32);
            writer.WriteUInt32MSB((uint)numIterations, 31);
            writer.WriteUInt32MSB((uint)numIterations, 32);
            writer.WriteUInt32MSB((uint)numIterations, 31);
            writer.WriteUInt32MSB((uint)numIterations, 32);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitWriter", "UInt64")]
    public int MemoryBitWriterUInt64()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;
        var writer = new MemoryBitWriter(WriteBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            writer.WriteUInt64MSB((ulong)numIterations, 63);
            writer.WriteUInt64MSB((ulong)numIterations, 64);
            writer.WriteUInt64MSB((ulong)numIterations, 63);
            writer.WriteUInt64MSB((ulong)numIterations, 64);
            writer.WriteUInt64MSB((ulong)numIterations, 63);
            writer.WriteUInt64MSB((ulong)numIterations, 64);
            writer.WriteUInt64MSB((ulong)numIterations, 63);
            writer.WriteUInt64MSB((ulong)numIterations, 64);
        }

        return 0;
    }
}
