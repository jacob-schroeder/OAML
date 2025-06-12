using OAML.Domain.Cryptography;
using OAML.Domain.Cryptography.Keys;
using OAML.Domain.Network;

namespace OAML.Domain.Nodes;

public class Node
{
    public uint Id => Common.Security.Crc32.GetHash(this);
    public string Name  { get; set; } //john
    public Endpoint Endpoint { get; set; }
    public List<CryptProvider> Crypts { get; set; } = [];

    public bool ConfiguredFor(string cryptName)
    {
        return Crypts.Any(c => c.Name == cryptName);
    }

    //figure out... how do I map a string name, to a class in code??
    //Also I need to load classes from a dll or something.
    
    //load from dll into ICryptoEngine: https://chatgpt.com/c/68478308-3d6c-8009-af82-6714a1db75aa
    public ICryptoEngine LoadEngine(string cryptName)
    {
        //lets just have a simple example
        var provider = Crypts.Single(c  => c.Name == cryptName);

        var engine = provider.LoadDllAsEngine();
        
        //var keysGen = engine.GenerateKeys();
        //File.WriteAllBytes("/home/jacob/Documents/keys/aes.key", keysGen.PublicKey);

        var keyUsage = KeyUsage.Symmetric; //engine.KeyUsage;

        KeyPair keys = keyUsage == KeyUsage.Symmetric ? 
                                KeyPair.CreateSymmetric(provider.PublicKeyPath) : 
                                KeyPair.CreateAsymmetric(provider.PublicKeyPath, provider.PrivateKeyPath);
        
        engine.LoadKeys(keys);

        return engine;
    }
}