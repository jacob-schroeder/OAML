using OAML.Components.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendChatApp.Components
{
    public class MessageQueueItem
    {
        public bool isSelf { get; set; }
        public UserNode Friend { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Processed { get; set; }
    }
}
