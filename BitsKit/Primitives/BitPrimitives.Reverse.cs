namespace BitsKit.Primitives;

public partial class BitPrimitives
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint ReverseBitOrder(uint value)
    {
        // swap odd and even bits
        value = ((value >> 1) & 0x55555555) | ((value & 0x55555555) << 1);
        // swap consecutive pairs
        value = ((value >> 2) & 0x33333333) | ((value & 0x33333333) << 2);
        // swap nibbles 
        value = ((value >> 4) & 0x0F0F0F0F) | ((value & 0x0F0F0F0F) << 4);

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong ReverseBitOrder(ulong value)
    {
        // swap odd and even bits
        value = ((value >> 1) & 0x5555555555555555) | ((value & 0x5555555555555555) << 1);
        // swap consecutive pairs
        value = ((value >> 2) & 0x3333333333333333) | ((value & 0x3333333333333333) << 2);
        // swap nibbles 
        value = ((value >> 4) & 0x0F0F0F0F0F0F0F0F) | ((value & 0x0F0F0F0F0F0F0F0F) << 4);

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static UInt128 ReverseBitOrder(UInt128 value)
    {
        return new(
            ReverseBitOrder((ulong)(value >> 0x40)),
            ReverseBitOrder((ulong)(value >> 0x00))
        );
    }
}
