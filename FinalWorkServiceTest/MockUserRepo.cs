using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Abstractions;
using UserService.DTO;
using UserService.Models;

namespace FinalWorkServiceTest
{
    internal class MockUserRepo : IUserRepo
    {
        public void UserAdd(string email, string password, RoleId roleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            throw new NotImplementedException();
        }

        public string Login(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public RoleId UserCheck(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
