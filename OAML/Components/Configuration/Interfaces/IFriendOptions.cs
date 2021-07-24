using OAML.Components.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Components.Configuration.Interfaces
{
    public interface IFriendOptions
    {
        ICollection<UserNode> Friends { get; set; }
    }
}
