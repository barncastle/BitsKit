using BenchmarkDotNet.Running;
using static BitsKit.Benchmarks.MultipleRuntimesConfig;

namespace BitsKit.Benchmarks;

public static class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitPrimitivesReadLSB"));
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitPrimitivesReadMSB"));

        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitPrimitivesWriteLSB"));
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitPrimitivesWriteMSB"));

        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitReader"));
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "MemoryBitReader"));
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitStreamReader"));

        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitWriter"));
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "MemoryBitWriter"));
        BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitStreamWriter"));

        Console.ReadLine();
    }
}
