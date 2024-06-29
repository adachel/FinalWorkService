using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Abstractions;
using UserService.DB;
using UserService.DTO;
using UserService.Models;
using XSystem.Security.Cryptography;

namespace FinalWorkTest
{
    internal class MockUserService : IUserRepo
    {
        public List<User> Users = new List<User>()
        {
        new User() { Email = "admin@example.com", Password = new byte[16], Salt = new byte[16], RoleId = RoleId.Admin },
        new User() { Email = "user1@example.com", Password = new byte[16], Salt = new byte[16], RoleId = RoleId.User },
        new User() { Email = "user2@example.com", Password = new byte[16], Salt = new byte[16], RoleId = RoleId.User }
        };

        public void UserAdd(string email, string password, RoleId roleId)
        {
            using (var context = new UserContext())
            {
                var user = Users.FirstOrDefault(x => x.Email == email);
                var adminCount = Users.Count(x => x.RoleId == RoleId.Admin);
                if (user == null)
                {
                    if (adminCount != 0 && roleId == RoleId.Admin)
                    {
                        throw new Exception("There can only be one administrator");
                    }
                    else
                    {
                        if (adminCount == 0 && roleId == RoleId.User)
                        {
                            throw new Exception("Administrator should be first");
                        }
                        else
                        {
                            var newUser = new User();
                            newUser.Email = email;
                            newUser.RoleId = roleId;

                            newUser.Salt = new byte[16];
                            new Random().NextBytes(newUser.Salt);
                            var data = Encoding.UTF8.GetBytes(password).Concat(newUser.Salt).ToArray();

                            newUser.Password = new SHA512Managed().ComputeHash(data);

                            Users.Add(newUser);
                            //_userContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    throw new Exception("This user already exists");
                }
            }
        }

        public IEnumerable<UserModel> GetUsers()
        {
            throw new Exception();
        }

        public void DeleteUser(string email)
        {
            var user = Users.FirstOrDefault(x => x.Email == email);    
            if (user != null) 
            {
                Users.Remove(user);
            }
        }

        public RoleId UserCheck(string email, string password)
        {
            throw new Exception();
        }


        public string Login(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        
    }
}
