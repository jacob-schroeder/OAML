namespace OAML.Domain.Protocol;

public record Envelope(
    EnvelopeHeader Header,
    EnvelopePayload Payload
    //EnvelopeSignature Signature
)
{
    public byte[] ToBytes()
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);

        // Serialize header
        bw.Write(Header.Magic);                      // 4 bytes
        bw.Write(Header.Version);                   // 4 bytes
        bw.Write((int)Header.EnvelopeType);          // 4 bytes

        // Serialize payload
        bw.Write(Payload.payload.Length);           // 4 bytes
        bw.Write(Payload.payloadEngineId);          // 4 bytes
        bw.Write(Payload.payload);
        
        // Serialize Signature
        //if (Payload.isSigned)
        {
            //bw.Write(Header.SenderSignature.Length);    // 4 bytes
            //bw.Write(Header.SenderSignature);           // N bytes
        }

        // Add a basic checksum (CRC32 or simple hash — placeholder here)
        int checksum = CalculateChecksum(Payload.payload);
        bw.Write(checksum);

        return ms.ToArray();
    }

    [Obsolete]
    //TODO: Remove this method, instead utilize IEnvelopeParser
    public static Envelope FromBytes(byte[] data)
    {
        using var ms = new MemoryStream(data);
        using var br = new BinaryReader(ms);

        uint magic = br.ReadUInt32();
        int version = br.ReadInt32();
        EnvelopeType messageType = (EnvelopeType)br.ReadInt32();
        
        // Payload
        uint payloadLength = br.ReadUInt32();
        uint  engineId = br.ReadUInt32();
        byte[] payloadData = br.ReadBytes((int)payloadLength);
        
        // Signature
        //if (isSigned)
        //{
            //int signatureLength = br.ReadInt32();
            //byte[] signature = br.ReadBytes(signatureLength);
        //}

        int checksum = br.ReadInt32();
        if (checksum != CalculateChecksum(payloadData))
            throw new InvalidDataException("Checksum mismatch");

        var header = new EnvelopeHeader(magic, version, messageType);
        var payload = new EnvelopePayload(payloadLength, engineId, payloadData);
        return new Envelope(header, payload);
    }

    public void WriteTo(Stream stream)
    {
        var bytes = ToBytes();
        stream.Write(bytes, 0, bytes.Length);
    }

    public static async Task<Envelope> ReadFromAsync(Stream stream)
    {
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        return FromBytes(ms.ToArray());
    }

    private static int CalculateChecksum(byte[] data)
    {
        // Simple checksum: sum of bytes modulo 2^31
        int checksum = 0;
        foreach (byte b in data)
            checksum = (checksum + b) & 0x7FFFFFFF;
        return checksum;
    }
}