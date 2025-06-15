using OAML.Domain.Cryptography;
using OAML.Domain.Nodes;
using OAML.Domain.Protocol;

namespace OAML.Protocol.Parsing;

public interface IEnvelopeParser
{
    bool CanParse(ReadOnlySpan<byte> data);
    Envelope Parse(ReadOnlySpan<byte> data);
}