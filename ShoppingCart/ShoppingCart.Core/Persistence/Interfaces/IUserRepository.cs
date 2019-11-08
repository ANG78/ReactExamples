using ShoppingCart.Model;

namespace ShoppingCart.Core.Persistence
{
    public interface IUserRepository
    {
        User Get(string intentifier, string password);
        User Get(int id);
    }
}
