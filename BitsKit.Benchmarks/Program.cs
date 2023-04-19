using BenchmarkDotNet.Running;
using static BitsKit.Benchmarks.MultipleRuntimesConfig;

namespace BitsKit.Benchmarks;

public static class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<DevBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all));
        //BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "BitPrimitivesWriteMSB", "UInt32"));
        //BenchmarkRunner.Run<BitsKitBenchmark>(new MultipleRuntimesConfig(MultipleRuntimesFlags.all, "Loop", "BitPrimitivesWriteMSB", "UInt64"));
        Console.ReadLine();
    }
}
