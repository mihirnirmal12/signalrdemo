using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace signalr.Models
{
    public partial class ConversationInfo
    {
        public ConversationInfo()
        {
            MessageInfos = new HashSet<MessageInfo>();
        }

        public int Convid { get; set; }
        public int? Userfrst { get; set; }
        public int? Usersecond { get; set; }
        [JsonIgnore]
        public virtual UserInfo? UserfrstNavigation { get; set; }
        [JsonIgnore]
        public virtual UserInfo? UsersecondNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<MessageInfo> MessageInfos { get; set; }
    }
}
