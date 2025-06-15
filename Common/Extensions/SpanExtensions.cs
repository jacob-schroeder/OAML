namespace Common.Extensions;

public static class SpanExtensions
{
    public static uint ReadUInt32(this ReadOnlySpan<byte> data, ref int offset)
    {
        uint value = BitConverter.ToUInt32(data.Slice(offset, 4));
        offset += 4;
        return value;
    }

    public static int ReadInt32(this ReadOnlySpan<byte> data, ref int offset)
    {
        int value = BitConverter.ToInt32(data.Slice(offset, 4));
        offset += 4;
        return value;
    }
}