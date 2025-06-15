using OAML.Domain.Protocol;
using OAML.Domain.Nodes;

namespace OAML.Infrastructure.Protocol;

public interface IEnvelopeHandler
{
    bool CanHandle(Envelope envelope, Node node);
    byte[] Decrypt(Envelope envelope, Node node);
}