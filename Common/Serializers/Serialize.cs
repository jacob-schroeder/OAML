using System.Text.Json;

namespace Common.Serializers;

public static class Serialize
{
    public static byte[] SerializeToBytes<T>(T obj)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}