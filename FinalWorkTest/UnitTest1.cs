using UserService.DB;
using UserService.DTO;
using UserService.Models;

namespace FinalWorkTest
{
    public class Tests
    {
        private Queue<UserModel> _users = new Queue<UserModel>();
        private List<User> _usersBD = new List<User>();

        [SetUp]
        public void Setup()
        {
            _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.Administrator });
            _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.User });
            _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.User });
            _users.Enqueue(new UserModel() { Email = "", Password = "", Role = UserRole.User });
        }

        [Test]
        public void Test1()
        {
            var mock = new MockUserService();
            foreach (var item in _users)
            {
                
            }




            Assert.Pass();
        }
    }
}