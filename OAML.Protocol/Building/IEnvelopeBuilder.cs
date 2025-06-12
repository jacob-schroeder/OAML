using OAML.Domain.Cryptography;
using OAML.Domain.Nodes;
using OAML.Domain.Protocol;

namespace OAML.Protocol.Building;

public interface IEnvelopeBuilder
{
    void SetEngine(ICryptoEngine engine);
    Envelope Build(Node node);
}