using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using signalr.Models;

namespace signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly dotnetinternalContext _context;
        public MessageController(dotnetinternalContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [EnableCors("demo")]
        public ActionResult getMessage(int id)
        {
            var msgs = from m in _context.MessageInfos.ToList()
                       join u in _context.UserInfos.ToList()
                       on m.Texter equals u.UserId
                       where m.Conid == id
                       select new
                       {
                           message = m.Messagecontent,
                           writer = u.UserId,
                           name = u.UserName
                       };

            return Ok(msgs);
                        
        }

    }
}
