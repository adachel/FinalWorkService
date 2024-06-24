using MessageService.Abstraction;
using MessageService.DTO;
using MessageService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepo _messageRepo;

        public MessageController(IMessageRepo messageRepo)
        {
            _messageRepo = messageRepo;
        }

        [HttpPost]
        [Route("SendMessage")]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult SendMessage([FromBody] MessageViewModel messageViewModel)
        {
            try
            {
                var userIdClaimValue = User.FindFirstValue("Id");

                var userId = Guid.Parse(userIdClaimValue);

                _messageRepo.SendMessage(messageViewModel.Text, userId, messageViewModel.ToUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [HttpPost]
        [Route("ReceiveMessage")]
        //[Authorize(Roles = "Administrator, User")]
        public ActionResult ReceiveMessage()
        {
            try
            {
                var userIdClaimValue = User.FindFirstValue("Id");

                var userId = Guid.Parse(userIdClaimValue);

                return Ok(_messageRepo.ReceiveMessage(userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }





    }
}
