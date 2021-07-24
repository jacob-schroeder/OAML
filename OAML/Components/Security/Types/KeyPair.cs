using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Security.Types
{
    public class KeyPair
    {
        public byte[] PublicKey { get; private set; }

        public byte[] PrivateKey { get; private set; }

        public static KeyPair Create(byte[] pubk, byte[] pvtk)
        {
            return new KeyPair
            {
                PublicKey = pubk,
                PrivateKey = pvtk
            };
        }
    }
}
