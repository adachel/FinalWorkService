using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Abstractions;
using UserService.Controllers;
using UserService.DB;
using UserService.DTO;
using UserService.Models;
using XAct.Users;

namespace FinalWorkTest
{
    internal class TestUserService
    {
        private MockUserService _mockUserService;
        [SetUp]
        public void Setup()
        {
            _mockUserService = new MockUserService();
        }

        [Test]
        public void AddUserTest()
        {
            var admin = new UserModel { Email = "admin2@example.com", Password = "1111", Role = UserRole.Administrator };
            var user = new UserModel { Email = "user4@example.com", Password = "1111", Role = UserRole.User };

            try
            {
                _mockUserService.UserAdd(admin.Email, admin.Password, (RoleId)admin.Role);
            }
            catch (Exception)
            {
                var resAdmin = _mockUserService.Users.FirstOrDefault(x => x.Email == admin.Email);
                Assert.IsNull(resAdmin);
            }

            _mockUserService.UserAdd(user.Email, user.Password, (RoleId)user.Role);
            var resUser = _mockUserService.Users.FirstOrDefault(x => x.Email == user.Email);
            Assert.IsNotNull(resUser);
        }

        [Test]
        public void DeleteUserTest()
        {
            string email = "user1@example.com";
            _mockUserService.DeleteUser(email);
            var resUser = _mockUserService.Users.FirstOrDefault(x => x.Email == email);
            Assert.IsNull(resUser);
        }
    }
}
