using OAML.Components.Security.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Security
{
    public interface IEncryptionManager
    {
        EncryptionMethod _CryptMethod { get; }

        int _KeySize { get; }

        KeyPair _Keys { get; }

        byte[] Encrypt(string key, byte[] data);

        byte[] Decrypt(string key, byte[] data);

        void LoadKeys(KeyPair Keys);

        KeyPair GenerateKeys();
    }
}
