using OAML.Domain.Protocol;
using OAML.Domain.Nodes;

namespace OAML.Infrastructure.Protocol;

public class BasicEnvelopeHandler : IEnvelopeHandler
{
    public bool CanHandle(Envelope envelope, Node node) 
        => node.ConfiguredFor(envelope.Payload.payloadEngineId);
    
    public byte[] Decrypt(Envelope envelope, Node node)
    {
        var engine = node.LoadEngine(envelope.Payload.payloadEngineId);

        return engine.Decrypt(envelope.Payload.payload);
    }
}