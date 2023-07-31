using Microsoft.AspNetCore.SignalR;
using signalr.Dto;
using signalr.Models;

namespace signalr.Hubs
{
    public class ChatHub:Hub
    {
        private readonly dotnetinternalContext _context;

        private static Dictionary<int, string> userConnectionStore = new Dictionary<int, string>();
        public ChatHub(dotnetinternalContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;

            int UserId =Convert.ToInt32(Context.GetHttpContext().Request.Query["userId"]);
            userConnectionStore[UserId] = connectionId;
            await Clients.All.SendAsync("global", new
            {
                UserId
            }); 
            await base.OnConnectedAsync();
        }   

        public  async Task SendMessage(MessageDto msg)
        {

            var m = new MessageInfo() {
                Conid = msg.Convid,
                Messagecontent=msg.message,
                Texter=msg.writer,
            };


            _context.MessageInfos.Add(m);
            _context.SaveChanges();

            var message = new
            {
                message = m.Messagecontent,
                author = m.Texter,
                convId=m.Conid,
                name = _context.UserInfos.ToList().FirstOrDefault(u => u.UserId == m.Texter).UserName,
            };

            //await Clients.Client(userConnectionStore[msg.receiver]).SendAsync("receive",msg.message);
            //await Clients.Client(userConnectionStore[msg.writer]).SendAsync("sender", msg.message);

            if (userConnectionStore.TryGetValue(msg.receiver, out string senderConnectionId))
            {
                await Clients.Client(senderConnectionId).SendAsync("receive", message);
            }

            if (userConnectionStore.TryGetValue(msg.writer, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("receive", message);
            }
        }

        
    }
}
