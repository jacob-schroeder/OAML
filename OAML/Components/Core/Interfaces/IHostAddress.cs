using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Core.Interfaces
{
    public interface IHostAddress
    {
        string IPAddress { get; set; }

        int Port { get; set; }
    }
}
