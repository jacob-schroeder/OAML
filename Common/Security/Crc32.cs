namespace Common.Security;

public static class Crc32
{
    private static readonly uint[] Table = Enumerable.Range(0, 256).Select(i =>
    {
        uint c = (uint)i;
        for (int j = 0; j < 8; j++)
            c = (c & 1) != 0 ? 0xEDB88320U ^ (c >> 1) : c >> 1;
        return c;
    }).ToArray();

    public static uint Compute(byte[] data)
    {
        uint crc = 0xFFFFFFFF;
        foreach (var b in data)
            crc = Table[(crc ^ b) & 0xFF] ^ (crc >> 8);
        return crc ^ 0xFFFFFFFF;
    }
}