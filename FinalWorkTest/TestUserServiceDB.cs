using AutoMapper;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Reflection.Metadata;
using UserService.Abstractions;
using UserService.Controllers;
using UserService.DB;
using UserService.DTO;
using UserService.Models;
using UserService.Repo;

namespace FinalWorkTest
{
    public class TestsUserServiceDB
    {
        private List<User> _users = new List<User>()
        {
        new User() { Email = "admin@example.com", Password = new byte[16], Salt = new byte[16], RoleId = RoleId.Admin },
        new User() { Email = "user1@example.com", Password = new byte[16], Salt = new byte[16], RoleId = RoleId.User },
        new User() { Email = "user2@example.com", Password = new byte[16], Salt = new byte[16], RoleId = RoleId.User }
        };
        private string connectionString = "Host = localhost; Port=5432;Username=aaa;Password=1234;Database=FinalUserTest";


        [SetUp]
        public void Setup()
        {
            using (var context = new UserContext(connectionString))
            {
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();

                foreach (var item in _users)
                {
                    context.Users.Add(item);
                }
                context.SaveChanges();
            }
        }

        [Test]
        public void AddUserTest()
        {
            using (var context = new UserContext(connectionString))
            {
                var admin = new UserModel { Email = "admin1@example.com", Password = "1111", Role = UserRole.Administrator };
                var user = new UserModel { Email = "user1@example.com", Password = "1111", Role = UserRole.User };
                var newUser = new UserModel { Email = "user3@example.com", Password = "1111", Role = UserRole.User };

                var arr = new List<UserModel>() { admin, user, newUser };

                var service = new UserRepo(context);
                foreach (var item in arr)
                {
                    try
                    {
                        service.UserAdd(item.Email, item.Password, (RoleId)item.Role);
                    }
                    catch (Exception)
                    {
                    }
                }

                var adminCount = context.Users.Count(x => x.RoleId == RoleId.Admin);
                Assert.IsTrue(adminCount == 1); // admin только один

                var doubleUser = context.Users.Count(x => x.Email == user.Email);
                Assert.IsTrue(doubleUser == 1); // дубликат

                var userCount = context.Users.Count(x => x.RoleId == RoleId.User);
                Assert.IsTrue(userCount == 3); // добавление
            }
        }

        [Test]
        public void DeleteUserTest()
        {
            using (var context = new UserContext(connectionString))
            {
                var admin = new UserModel { Email = "admin@example.com", Password = "", Role = UserRole.Administrator };
                var user = new UserModel { Email = "user1@example.com", Password = "", Role = UserRole.User };
                var arr = new List<UserModel>() { admin, user };

                var service = new UserRepo(context);

                foreach (var item in arr)
                {
                    try
                    {
                        service.DeleteUser(item.Email);
                    }
                    catch (Exception)
                    {
                    }
                }

                var adminCount = context.Users.Count(x => x.RoleId == RoleId.Admin);
                Assert.IsTrue(adminCount == 1); // admin удалить нельзя

                var userCount = context.Users.Count(x => x.RoleId == RoleId.User);
                Assert.IsTrue(userCount == 1); // удаление
            }
        }

        [TearDown]
        public void Teardown()
        {
            using (var context = new UserContext(connectionString))
            {
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
            }
        }
    }
}