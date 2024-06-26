using Moq;
using UserService.Abstractions;
using UserService.DB;
using UserService.DTO;
using UserService.Models;

namespace TestProject1
{
    public class Tests
    {
        private string connectionString = "Host = localhost; Port=5432;Username=aaa;Password=1234;Database=FinalUserTest";


        [SetUp]
        public void Setup()
        {
            using (var context = new UserContext(connectionString))
            {
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task Test1()
        {
            var c = new Mock<IUserRepo>();
            var mock = new MockUserService();
            var admin = new UserModel { Email = "admin@example.com", Password = "1111", Role = UserRole.Administrator };
            var user2 = new UserModel() { Email = "222@gmail.com", Password = "1234" };

            c.Verify(x => x.UserAdd(admin.Email, admin.Password, (RoleId)admin.Role), connectionString);



            mock.UserAdd(admin.Email, admin.Password, (RoleId)admin.Role);

            //Assert.IsTrue(mock.UserAdd(admin.Email, admin.Password, (RoleId)admin.Role) == 1);
            //mock.AddUser(user2);
            //ClassicAssert.IsTrue(mock.UserCheck(user2.Email, user2.Password) == RoleId.User);
            //mock.DeleteUser("222@gmail.com");
            //ClassicAssert.IsTrue(mock.UserCheck(user2) == RoleId.Admin);
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