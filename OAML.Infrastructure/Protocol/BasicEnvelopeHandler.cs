using OAML.Domain.Protocol;
using OAML.Domain.Nodes;

namespace OAML.Infrastructure.Protocol;

//TODO: Change node here to HOST, want to validate if the HOST can support and decrypt envelope EngineID
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