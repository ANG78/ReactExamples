using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public bool Validate(string user, EnumPermission permission)
        {
            return true;
        }
    }
}
