using OAML.Domain.Cryptography.Keys;
using System.Security.Cryptography;

namespace OAML.Domain.Cryptography;

public class AesCryptoEngine : ICryptoEngine
{
    public KeyUsage KeyUsage { get; }
    public int KeySize { get; }
    
    public KeyPair Keys { get; set; }

    public byte[] Encrypt(byte[] data)
    {
        byte[] nonce = RandomNumberGenerator.GetBytes(12);  // GCM nonce
        byte[] tag = new byte[16];                          // 128-bit tag
        byte[] ciphertext = new byte[data.Length];

        using var aesGcm = new AesGcm(Keys.PublicKey);
        aesGcm.Encrypt(nonce, data, ciphertext, tag);

        // Combine: [nonce | tag | ciphertext]
        byte[] result = new byte[nonce.Length + tag.Length + ciphertext.Length];
        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length, tag.Length);
        Buffer.BlockCopy(ciphertext, 0, result, nonce.Length + tag.Length, ciphertext.Length);

        return result;
    }

    public byte[] Decrypt(byte[] data)
    {
        int nonceLength = 12;
        int tagLength = 16;

        byte[] nonce = new byte[nonceLength];
        byte[] tag = new byte[tagLength];
        byte[] ciphertext = new byte[data.Length - nonceLength - tagLength];

        Buffer.BlockCopy(data, 0, nonce, 0, nonceLength);
        Buffer.BlockCopy(data, nonceLength, tag, 0, tagLength);
        Buffer.BlockCopy(data, nonceLength + tagLength, ciphertext, 0, ciphertext.Length);

        byte[] plaintext = new byte[ciphertext.Length];

        using var aesGcm = new AesGcm(Keys.PublicKey);
        aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);

        return plaintext;
    }

    public void LoadKeys(KeyPair Keys)
    {
        this.Keys = Keys;
    }

    public KeyPair GenerateKeys()
    {
        byte[] key = RandomNumberGenerator.GetBytes(32); // 32 bytes = 256 bits

        return KeyPair.CreateSymmetric(key);
    }
}