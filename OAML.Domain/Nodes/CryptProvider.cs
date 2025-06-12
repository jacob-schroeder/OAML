using OAML.Domain.Cryptography;

namespace OAML.Domain.Nodes;

public class CryptProvider
{
    public uint Id => Common.Security.Crc32.GetHash(this);
    public string Name { get; set; }
    public string DllPath { get; set; }
    
    public string PublicKeyPath { get; set; }
    public string PrivateKeyPath { get; set; }

    
    public ICryptoEngine LoadDllAsEngine()
    {
        return new AesCryptoEngine();
    }
}