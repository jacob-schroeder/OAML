using OAML.Domain.Cryptography.Keys;

namespace OAML.Domain.Cryptography;

public interface ICryptoEngine
{
    public uint EngineId { get; }
    public KeyUsage KeyUsage { get; }
    protected int KeySize { get; }
    public KeyPair Keys { get; set; }
    
    byte[] Encrypt(byte[] data);

    byte[] Decrypt(byte[] data);

    void LoadKeys(KeyPair Keys);

    KeyPair GenerateKeys();
}