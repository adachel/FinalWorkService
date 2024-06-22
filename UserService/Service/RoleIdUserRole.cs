using UserService.DTO;
using UserService.Models;

namespace UserService.Service
{
    public static class RoleIdUserRole
    {
        public static UserRole RoleIdToUserRole(RoleId id)
        {
            if (id == RoleId.Admin)
            {
                return UserRole.Administrator;
            }
            else return UserRole.User;
        }
    }
}
