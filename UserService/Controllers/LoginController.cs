using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Abstractions;
using UserService.DTO;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepo _userRepository;
        public LoginController(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] UserModel userModel)
        {
            try
            {
                _userRepository.UserAdd(userModel.Email, userModel.Password, RoleId.Admin);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] UserModel userModel)
        {
            try
            {
                _userRepository.UserAdd(userModel.Email, userModel.Password, RoleId.User);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserModel userModel)
        {
            try
            {
                return Ok(_userRepository.Login(userModel));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet(template: "GetUsers")]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            try
            {
                return Ok(_userRepository.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteUser([FromBody] UserModel user)
        {
            try
            {
                _userRepository.DeleteUser(user.Email);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
