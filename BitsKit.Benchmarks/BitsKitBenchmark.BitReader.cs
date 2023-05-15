using BenchmarkDotNet.Attributes;
using BitsKit.IO;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{

    [Benchmark]
    [BenchmarkCategory("BitReader", "UInt1")]
    public int BitReaderBit()
    {
        var maxNumIterations = BufferSize;
        var reader = new BitReader(ReadBuffer);

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
    [BenchmarkCategory("BitReader", "UInt4")]
    public int BitReaderUInt04()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new BitReader(ReadBuffer);

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
    [BenchmarkCategory("BitReader", "UInt8")]
    public int BitReaderUInt08()
    {
        const int bitCount = 8;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new BitReader(ReadBuffer);

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
    [BenchmarkCategory("BitReader", "UInt16")]
    public int BitReaderUInt16()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new BitReader(ReadBuffer);

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
    [BenchmarkCategory("BitReader", "UInt32")]
    public int BitReaderUInt32()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new BitReader(ReadBuffer);

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
    [BenchmarkCategory("BitReader", "UInt64")]
    public int BitReaderUInt64()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;
        var reader = new BitReader(ReadBuffer);

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
