using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Abstractions;
using UserService.DTO;
using UserService.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinalWorkTest
{
    internal class MockUserService : IUserRepo
    {
        //private Queue<UserModel> _users = new Queue<UserModel>();

        //public MockUserService()
        //{
        //    _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.Administrator });
        //    _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.User });
        //    _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.User });
        //    _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.User });
        //}

        public void UserAdd(string email, string password, RoleId roleId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string email)
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
    }
}
