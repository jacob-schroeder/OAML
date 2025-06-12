using OAML.Domain.Cryptography.Keys;

namespace OAML.Domain.Cryptography;

public interface ICryptoEngine
{
    protected KeyUsage KeyUsage { get; }
    protected int KeySize { get; }
    public KeyPair Keys { get; set; }
    
    byte[] Encrypt(byte[] data);

    byte[] Decrypt(byte[] data);

    void LoadKeys(KeyPair Keys);

    KeyPair GenerateKeys();
}