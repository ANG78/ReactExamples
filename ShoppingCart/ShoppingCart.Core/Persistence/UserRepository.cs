using ShoppingCart.Model;
using System.Linq;

namespace ShoppingCart.Core.Persistence
{
    public class UserRepository : DataRepository, IUserRepository
    {
        public User Get(string user, string password)
        {
            User result = Users.ToList().FirstOrDefault(x => x.Identifier == user);
            if (result == null)
            {
                result = new Customer() { Id = GetId(), Identifier = user };
                Users.Add(result);
            }
            return result;
        }

        public User Get(int id)
        {
            return Users.ToList().FirstOrDefault(x => x.Id == id);
        }
    }
}
