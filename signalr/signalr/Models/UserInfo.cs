using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace signalr.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            ConversationInfoUserfrstNavigations = new HashSet<ConversationInfo>();
            ConversationInfoUsersecondNavigations = new HashSet<ConversationInfo>();
            MessageInfos = new HashSet<MessageInfo>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<ConversationInfo> ConversationInfoUserfrstNavigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<ConversationInfo> ConversationInfoUsersecondNavigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<MessageInfo> MessageInfos { get; set; }
    }
}
