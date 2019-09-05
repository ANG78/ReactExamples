using Interfaces.BusinessLogic.Entities;

namespace Interfaces.BusinessLogic.Services
{
    public interface IUserService
    {
        bool Validate(string user, EnumPermission permission);
    }
}
