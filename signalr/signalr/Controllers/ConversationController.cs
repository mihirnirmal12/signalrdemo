using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using signalr.Dto;
using signalr.Models;

namespace signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly dotnetinternalContext _context;
        public ConversationController(dotnetinternalContext context)
        {
            _context = context;
        }

        [HttpPost]
        [EnableCors("demo")]
        public ActionResult addConversation(ConversationDto conv)
        {

            var newconversation = new ConversationInfo
            {
                Userfrst = conv.userfirst,
                Usersecond = conv.usersecond
            };

            var found = _context.ConversationInfos.ToList().Find(x => (x.Userfrst == conv.userfirst && 
                    x.Usersecond == conv.usersecond) || (x.Userfrst == conv.usersecond && x.Usersecond == conv.userfirst));

            if (found == null)
            {
                _context.ConversationInfos.Add(newconversation);
                _context.SaveChanges();
                return Ok(conv);
            }
            else
            {
                return BadRequest("converstion is exists");
            }
        }

        [HttpGet("{id}")]
        [EnableCors("demo")]
        public ActionResult getMyConversation(int id)
        {

            var user = _context.UserInfos.ToList().Find(x=>x.UserId==id);
            if(user == null)
            {
                return BadRequest("user not found");
            }

            var data = (from c in _context.ConversationInfos.ToList()
                       join u in _context.UserInfos.ToList() on c.Usersecond equals u.UserId
                       where c.Userfrst == id || c.Usersecond==id
                       let ID = user.UserId == c.Userfrst ? c.Usersecond : c.Userfrst

                       select (from u in _context.UserInfos
                               where u.UserId == ID
                               select new
                        
                                {
                                convid=c.Convid,
                                userid=u.UserId,
                                username = u.UserName,

                                }).First()).ToList();
            return Ok(data);
        }


    }
    
    
}

