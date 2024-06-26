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

namespace TestProject1
{
    internal class MockUserService : IUserRepo
    {
        private string connectionString = "Host = localhost; Port=5432;Username=aaa;Password=1234;Database=FinalUserTest";

        public void UserAdd(string email, string password, RoleId roleId)
        {
            using (var context = new UserContext(connectionString))
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                var adminCount = context.Users.Count(x => x.RoleId == RoleId.Admin);
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

                            context.Add(newUser);
                            context.SaveChanges();
                        }
                    }
                }
                else
                {
                    throw new Exception("This user already exists");
                }
            }
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
