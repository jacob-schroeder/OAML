namespace OAML.Domain.Protocol;

public record EnvelopePayload(
    uint payloadSize, 
    uint payloadEngineId,
    byte[] payload
);