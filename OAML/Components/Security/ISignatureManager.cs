using OAML.Components.Security.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Security
{
    public interface ISignatureManager
    {
        byte[] Sign(byte[] key, byte[] data);

        bool Verify(byte[] key, byte[] data, byte[] signature);

        void LoadKeys(KeyPair Keys);

        KeyPair GenerateKeys();
    }
}
