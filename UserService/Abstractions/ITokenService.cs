using UserService.DTO;

namespace UserService.Abstractions
{
    public interface ITokenService
    {
        public string GenerateToken(UserModel user, IConfiguration config);
    }
}
