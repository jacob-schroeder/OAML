using OAML.Domain.Nodes;

namespace OAML.Infrastructure.Configuration;

public class CryptoConfig
{
    public string Name { get; set; }
    public string Source { get; set; }
    public string Private { get; set; }
    public string Public { get; set; }

    public CryptProvider ToCryptProvider()
    {
        return new CryptProvider()
        {
            Name = Name,
            DllPath = Source,
            PublicKeyPath = Public,
            PrivateKeyPath = Private
        };
    }
}