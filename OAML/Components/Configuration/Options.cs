using OAML.Components.Configuration.Interfaces;
using OAML.Components.Core;
using OAML.Components.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Configuration
{
    public class Options : IServerOptions, IFriendOptions, IEncryptionKey, ISignatureKey
    {
        public int Port { get; set; }

        public string EncryptionMethod { get; set; }
        public string DataKeyPath { get; set; }

        public string SignatureAlgo { get; set; }
        public string SignKeyPath { get; set; }

        public ICollection<UserNode> Friends { get; set; }
    }
}
