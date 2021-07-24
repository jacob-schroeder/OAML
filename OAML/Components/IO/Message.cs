using OAML.Components.IO.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.IO
{
    public class Message
    {
        public string _magic { get; set; }

        public float _version { get; set; }

        public MessageType _type { get; set; }

        public bool _encrypted { get; set; }

        public bool _signed { get; set; }

        public int _length => _data?.Length ?? 0;
        public byte[] _data { get; set; }
        public int _siglength => _signature?.Length ?? 0;
        public byte[] _signature { get; set; }
    }
}
