using ShoppingCart.Model;

namespace ShoppingCart.Core.BusinessLogic
{
    public interface IUserService
    {
        User Login(string user, string password);
        User Get(int idUser);
    }
}
