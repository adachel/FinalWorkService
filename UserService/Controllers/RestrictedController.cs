using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedController : ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.Role}");
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.Role}");
        }

        [HttpGet]
        [Route("UserID")]
        public ActionResult GetUserID()
        {
            var userIdClaimValue = User.FindFirstValue("Id");
            return Ok($"id = {userIdClaimValue}");
        }


        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
                };
            }
            return null;
        }
    }
}
