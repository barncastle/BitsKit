using BenchmarkDotNet.Attributes;
using BitsKit.IO;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{

    [Benchmark]
    [BenchmarkCategory("MemoryBitReader", "UInt1")]
    public int MemoryBitReaderBit()
    {
        var maxNumIterations = BufferSize;
        var reader = new MemoryBitReader(ReadBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
            _ = reader.ReadBitMSB();
        }

        return 0;
    }
    [Benchmark]
    [BenchmarkCategory("MemoryBitReader", "UInt4")]
    public int MemoryBitReaderUInt04()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new MemoryBitReader(ReadBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitReader", "UInt8")]
    public int MemoryBitReaderUInt08()
    {
        const int bitCount = 8;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new MemoryBitReader(ReadBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
            _ = reader.ReadUInt8MSB(bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitReader", "UInt16")]
    public int MemoryBitReaderUInt16()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new MemoryBitReader(ReadBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
            _ = reader.ReadUInt16MSB(bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitReader", "UInt32")]
    public int MemoryBitReaderUInt32()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new MemoryBitReader(ReadBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
            _ = reader.ReadUInt32MSB(bitCount);
        }

        return 0;
    }

    [Benchmark]
    [BenchmarkCategory("MemoryBitReader", "UInt64")]
    public int MemoryBitReaderUInt64()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new MemoryBitReader(ReadBuffer);

        for (var numIterations = 0; numIterations < maxNumIterations; numIterations++)
        {
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
            _ = reader.ReadUInt64MSB(bitCount);
        }

        return 0;
    }
}
