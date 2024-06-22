using UserService.DTO;
using UserService.Models;

namespace UserService.Abstractions
{
    public interface IUserRepo
    {
        public void UserAdd(string email, string password, RoleId roleId);
        public RoleId UserCheck(string email, string password);
        public IEnumerable<UserModel> GetUsers();
        public void DeleteUser(string email);

        public string Login(UserModel userModel);
    }
}
