namespace OAML.Domain.Cryptography.Keys;

public class KeyPair
{
    public KeyUsage KeyUsage { get; private set; }
    
    public byte[] PublicKey { get; private set; }
    public byte[] PrivateKey { get; private set; }

    public static KeyPair CreateSymmetric(byte[] key)
    {
        return new KeyPair()
        {
            KeyUsage = KeyUsage.Symmetric,
            
            PublicKey = key,
            PrivateKey = key
        };
    }

    //To Do: exception if not found
    public static KeyPair CreateSymmetric(string keyPath)
    {
        byte[] contents = File.ReadAllBytes(keyPath);
        return CreateSymmetric(contents);
    }
    
    public static KeyPair CreateAsymmetric(byte[] publicKey, byte[] privateKey)
    {
        return new KeyPair()
        {
            KeyUsage = KeyUsage.Asymmetric,
            
            PublicKey = publicKey,
            PrivateKey = privateKey
        };
    }
    
    //To do: exception if not found
    public static KeyPair CreateAsymmetric(string publicKeyPath, string privateKeyPath)
    {
        byte[] publicContents = File.ReadAllBytes(publicKeyPath);
        byte[] privateContents = File.ReadAllBytes(privateKeyPath);
        
        return CreateAsymmetric(publicContents, privateContents);
    }
}