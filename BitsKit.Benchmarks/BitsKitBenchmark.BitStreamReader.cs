using BenchmarkDotNet.Attributes;
using BitsKit.IO;

namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{

    [Benchmark]
    [BenchmarkCategory("BitStreamReader", "UInt1")]
    public int BitStreamReaderBit()
    {
        var maxNumIterations = BufferSize;
        var stream = new MemoryStream(ReadBuffer);
        var reader = new BitStreamReader(stream);

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
    [BenchmarkCategory("BitStreamReader", "UInt4")]
    public int BitStreamReaderUInt04()
    {
        const int bitCount = 4;
        int maxNumIterations = BufferSize / bitCount;
        var stream = new MemoryStream(ReadBuffer);
        var reader = new BitStreamReader(stream);

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
    [BenchmarkCategory("BitStreamReader", "UInt8")]
    public int BitStreamReaderUInt08()
    {
        const int bitCount = 8;
        int maxNumIterations = BufferSize / bitCount;
        var stream = new MemoryStream(ReadBuffer);
        var reader = new BitStreamReader(stream);

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
    [BenchmarkCategory("BitStreamReader", "UInt16")]
    public int BitStreamReaderUInt16()
    {
        const int bitCount = 16;
        int maxNumIterations = BufferSize / bitCount;
        var stream = new MemoryStream(ReadBuffer);
        var reader = new BitStreamReader(stream);

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
    [BenchmarkCategory("BitStreamReader", "UInt32")]
    public int BitStreamReaderUInt32()
    {
        const int bitCount = 32;
        int maxNumIterations = BufferSize / bitCount;
        var stream = new MemoryStream(ReadBuffer);
        var reader = new BitStreamReader(stream);

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
    [BenchmarkCategory("BitStreamReader", "UInt64")]
    public int BitStreamReaderUInt64()
    {
        const int bitCount = 64;
        int maxNumIterations = BufferSize / bitCount;
        var stream = new MemoryStream(ReadBuffer);
        var reader = new BitStreamReader(stream);

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
