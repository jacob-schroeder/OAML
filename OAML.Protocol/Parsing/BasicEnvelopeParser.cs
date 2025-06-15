using Common.Extensions;
using OAML.Domain.Protocol;

namespace OAML.Protocol.Parsing;

public class BasicEnvelopeParser : IEnvelopeParser
{
    public bool CanParse(ReadOnlySpan<byte> data)
    {
        if (data.Length < 4) 
            return false;

        uint magic = BitConverter.ToUInt32(data.Slice(0, 4));
        return Constants.Magic == magic;
    }

    public Envelope Parse(ReadOnlySpan<byte> data)
    {
        int offset = 0;

        //parse header (move to it's own method)
        uint magic = data.ReadUInt32(ref offset);
        int version = data.ReadInt32(ref offset);
        EnvelopeType messageType = (EnvelopeType)data.ReadInt32(ref offset);

        //parse payload
        uint payloadLength = data.ReadUInt32(ref offset);
        uint engineId = data.ReadUInt32(ref offset);

        if (data.Length < offset + payloadLength + sizeof(int)) // check if enough data remains
            throw new InvalidDataException("Incomplete payload");

        ReadOnlySpan<byte> payloadDataSpan = data.Slice(offset, (int)payloadLength);
        byte[] payloadData = payloadDataSpan.ToArray(); // convert only payload to array
        offset += (int)payloadLength;

        int checksum = data.ReadInt32(ref offset);
        //if (checksum != CalculateChecksum(payloadData))
        //    throw new InvalidDataException("Checksum mismatch");

        var header = new EnvelopeHeader(magic, version, messageType);
        var payload = new EnvelopePayload(payloadLength, engineId, payloadData);
        return new Envelope(header, payload);
    }
}
