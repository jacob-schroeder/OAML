using OAML.Domain.Cryptography;
using OAML.Domain.Cryptography.Keys;
using OAML.Domain.Network;

namespace OAML.Domain.Nodes;

public class Node
{
    public uint Id => 0; //TODO: Implement crc32 of Node...
    public string Name  { get; set; } //john
    public Endpoint Endpoint { get; set; }
    public List<CryptProvider> Crypts { get; set; } = [];

    //TODO: Cleanup these methods below... 
    public bool ConfiguredFor(string cryptName)
    {
        return Crypts.Any(c => c.Name == cryptName);
    }

    public bool ConfiguredFor(uint engineId)
    {
        return Crypts.Any(c => c.EngineId == engineId);
    }
    
    public ICryptoEngine LoadEngine(string cryptName)
    {
        var provider = Crypts.Single(c  => c.Name == cryptName);

        return LoadEngine(provider);
    }

    public ICryptoEngine LoadEngine(uint engineId)
    {
        var provider = Crypts.Single(c  => c.EngineId == engineId);

        return LoadEngine(provider);
    }

    private ICryptoEngine LoadEngine(CryptProvider provider)
    {
        var engine = provider.LoadDllAsEngine();
  
        KeyPair keys = engine.KeyUsage == KeyUsage.Symmetric ? 
            KeyPair.CreateSymmetric(provider.PublicKeyPath) : 
            KeyPair.CreateAsymmetric(provider.PublicKeyPath, provider.PrivateKeyPath);
        
        engine.LoadKeys(keys);

        return engine;
    }
}