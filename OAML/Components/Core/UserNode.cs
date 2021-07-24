using OAML.Components.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Core
{
    public class UserNode : IHostAddress, IEncryptionKey, ISignatureKey
    {
        public long ID { get; set; }

        public string DisplayName { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        //Encryption, Signatures, Methods
        public string EncryptionMethod { get; set; }
        public string DataKeyPath { get; set; }

        public string SignatureAlgo { get; set; }
        public string SignKeyPath { get; set; }
    }
}
