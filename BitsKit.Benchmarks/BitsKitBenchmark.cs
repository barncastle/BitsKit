namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    private readonly byte[] ReadBuffer = new byte[]
    {
        239, 128, 162, 245, 92, 32, 202, 103, 218,
        251, 248, 150, 234, 92, 161, 192, 27
    };

    private readonly byte[] WriteBuffer = new byte[9];
}
