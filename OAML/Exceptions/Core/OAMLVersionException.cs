using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Exceptions.Core
{

    [Serializable]
    public class OAMLVersionException : Exception
    {
        public float AppVersion => Metadata.Version;

        public float MsgVersion { get; }

        public OAMLVersionException() { }

        public OAMLVersionException(string message)
            : base(message) { }

        public OAMLVersionException(string message, Exception inner)
            : base(message, inner) { }

        public OAMLVersionException(string message, float version)
            : this(message)
        {
            MsgVersion = version;
        }
    }
}
