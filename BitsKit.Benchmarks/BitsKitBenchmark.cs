namespace BitsKit.Benchmarks;

public partial class BitsKitBenchmark
{
    private const int BufferSize = 10000;

    private readonly byte[] ReadBuffer = new byte[BufferSize];
    private readonly byte[] WriteBuffer = new byte[BufferSize];

    public BitsKitBenchmark()
    {
        FillBuffer(ReadBuffer);
        FillBuffer(WriteBuffer);
    }

    private static void FillBuffer(Span<byte> buffer)
    {
        uint h = 2166136261u;

        for (int i = 0; i < buffer.Length; i++)
        {
            h ^= (h << 5) + (h >> 2) + (uint)i;
            buffer[i] = (byte)h;
        }
    }
}
