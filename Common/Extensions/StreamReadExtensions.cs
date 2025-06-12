namespace Common.Extensions;

public static class StreamReadExtensions
{
    //to do: check if can read bytes, if not, return empty array.
    public static byte[] ReadBytes(this Stream stream, int len)
    {
        byte[] buffer = new byte[len];
        stream.Read(buffer, 0, len);

        //return [];
        return buffer;
    }

    //to do: check if can read bytes, if not, return empty string.
    public static string ReadString(this Stream stream, int len)
    {
        byte[] buffer = new byte[len];
        stream.Read(buffer, 0, len);

        return System.Text.Encoding.UTF8.GetString(buffer);

        //return string.Empty;
    }

    public static int ReadInt32(this Stream stream)
    {
        byte[] buffer = new byte[4];
        stream.Read(buffer, 0, 4);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(buffer);

        return BitConverter.ToInt32(buffer, 0);
    }

    public static float ReadFloat(this Stream stream)
    {
        byte[] buffer = new byte[4];
        //what is this??
        //stream.ReadExactly(buffer, 0, 4);
        stream.Read(buffer, 0, 4);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(buffer);

        return BitConverter.ToSingle(buffer, 0);
    }

    public static bool ReadBoolean(this Stream stream)
    {
        return stream.ReadByte() > 0;
    }
}