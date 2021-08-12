using OAML.Components.Security;
using OAML.Components.Security.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Security.Signature
{
    public class ECDSAManager : ISignatureManager
    {
        public KeyPair _Keys { get; private set; }

        public KeyPair GenerateKeys()
        {
            using (ECDsaCng dsa = new ECDsaCng())
            {
                dsa.HashAlgorithm = CngAlgorithm.Sha256;

                return KeyPair.Create(dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob), dsa.Key.Export(CngKeyBlobFormat.EccPrivateBlob));
            }
        }

        public void LoadKeys(KeyPair Keys)
        {
            _Keys = Keys;
        }

        public byte[] Sign(byte[] key, byte[] data)
        {
            //use _Keys.PrivateKey instead of key here..
            using (ECDsaCng dsa = new ECDsaCng(CngKey.Import(key, CngKeyBlobFormat.EccPrivateBlob)))
            {
                dsa.HashAlgorithm = CngAlgorithm.Sha256;

                return dsa.SignData(data);
            }
        }

        public bool Verify(byte[] key, byte[] data, byte[] signature)
        {
            //use _Keys.PublicKey instead of key here..
            try
            {
                using (ECDsaCng mngr = new ECDsaCng(CngKey.Import(key, CngKeyBlobFormat.EccPublicBlob)))
                {
                    return mngr.VerifyData(data, signature);
                }
            }
            catch(PlatformNotSupportedException ex)
            {

            }
            catch(CryptographicException ex)
            {

            }

            return false;
        }
    }
}