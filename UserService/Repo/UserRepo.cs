using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using UserService.Abstractions;
using UserService.DB;
using UserService.DTO;
using UserService.Models;
using UserService.Service;

namespace UserService.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly IMapper _mapper;
        private UserContext _userContext; 
        private readonly IConfiguration _config;

        public UserRepo(UserContext userContext)    //
        {
            _userContext = userContext;
        }

        public UserRepo(IMapper mapper, UserContext userContext, IConfiguration config)
        {
            _mapper = mapper;
            _userContext = userContext;
            _config = config;
        }

        public void UserAdd(string email, string password, RoleId roleId)
        {
            using (var context = new UserContext())
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == email);
                var adminCount = _userContext.Users.Count(x => x.RoleId == RoleId.Admin);
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

                            _userContext.Add(newUser);
                            _userContext.SaveChanges();
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
            using (_userContext)
            {
                var users = _userContext.Users.Select(_mapper.Map<UserModel>).ToList();
                return users;
            }
        }

        public void DeleteUser(string email)
        {
            using (var context = new UserContext())
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    if (user.RoleId != RoleId.Admin)
                    {
                        _userContext.Users.Remove(user);
                        _userContext.SaveChanges();
                    }
                    else throw new Exception("Administrator cannot be deleted");
                }
                else throw new Exception("This user is not exist");
            }
        }

        public string Login(UserModel userModel)
        {
            var roleId = UserCheck(userModel.Email, userModel.Password);
            var user = new UserModel { Email = userModel.Email, Role = RoleIdUserRole.RoleIdToUserRole(roleId) };
            var token = TokenService.GenerateToken(user, _config, _userContext);
            return token;
        }

        public RoleId UserCheck(string name, string password)
        {
            using (var context = new UserContext())
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == name);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var bpassword = shaM.ComputeHash(data);

                if (user.Password.SequenceEqual(bpassword))
                {
                    return user.RoleId;
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
        }
    }
}
