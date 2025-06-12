using OAML.Domain.Cryptography;
using OAML.Domain.Nodes;
using OAML.Domain.Protocol;

namespace OAML.Protocol.Building;

public class TextBuilder : IEnvelopeBuilder
{
    private string _message;
    private ICryptoEngine _cryptoEngine;
    
    public TextBuilder SetMessage(string message)
    {
        _message = message;
        
        return this;
    }

    public void SetEngine(ICryptoEngine cryptoEngine)
    {
        _cryptoEngine =  cryptoEngine;
    }
    
    public Envelope Build(Node node)
    {
        //var signature = signPayload(payload);
        
        
        byte[] plaintext = System.Text.Encoding.UTF8.GetBytes(_message);
        
        byte[] payloadData = _cryptoEngine.Encrypt(plaintext);
        
        //encrypt payload now
        
        //unset message
        _message = string.Empty;

        var header = new EnvelopeHeader(
            Magic: Constants.Magic,
            Version: Constants.Version,
            EnvelopeType: EnvelopeType.Message
            //SenderSignature: signature
        );

        var payload = new EnvelopePayload((uint)payloadData.Length,
            false, //is signed
            payloadData);

        return new Envelope(header, payload);
    }
}