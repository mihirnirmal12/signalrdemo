using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace signalr.Models
{
    public partial class MessageInfo
    {
        public int Messageid { get; set; }
        public string? Messagecontent { get; set; }
        public int? Conid { get; set; }
        public int? Texter { get; set; }
        [JsonIgnore]
        public virtual ConversationInfo? Con { get; set; }
        [JsonIgnore]
        public virtual UserInfo? TexterNavigation { get; set; }
    }
}
