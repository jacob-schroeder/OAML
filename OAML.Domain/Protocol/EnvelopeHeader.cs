namespace OAML.Domain.Protocol;

/*
 * Thoughts....
 *
 * because we're serializing and sending over TCP,
 * we may want to think of a strategy for sending multiple envelopes
 * to a client, such as a large file that needs to be sent over multiple envelopes
 * because of size restrictions by OS
 */
public record EnvelopeHeader(
    uint Magic,
    float Version,
    EnvelopeType EnvelopeType
);