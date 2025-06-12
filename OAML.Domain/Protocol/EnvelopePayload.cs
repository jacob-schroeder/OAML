namespace OAML.Domain.Protocol;

public record EnvelopePayload(
    uint payloadSize, 
    bool isSigned, //thoughts: I really want to use a bitfield here to condense the envelope size
    byte[] payload
);