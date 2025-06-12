using K4os.Hash.xxHash;

namespace Common.Security;

public static class Crc32
{
    public static uint GetHash<T>(T obj)
    {
        var random = new Random();
        int value = random.Next();           // Any non-negative int
        int bounded = random.Next(0, 1000);  // 0 to 999

        return (uint)bounded;

        //byte[] data = Serializers.Serialize.SerializeToBytes(obj);
        //return XXH32.DigestOf(data);
    }
}