using ShoppingCart.Core.Persistence;
using ShoppingCart.Model;

namespace ShoppingCart.Core.BusinessLogic
{
    public class UserService : IUserService
    {
        private IUserRepository repository { get; set; }
        public UserService(IUserRepository userRepository)
        {
            repository = userRepository;
        }
        public User Login(string suser, string spassword)
        {
            if ( string.IsNullOrWhiteSpace( suser))
            {
                throw new ShoppingCartProcessingException("indentifier must be supplied");
            }
            
            User user = repository.Get(suser, spassword);
            if ( user == null || user.Id <= 0)
                throw new ShoppingCartProcessingException("User Not Found");

            return user;
        }

        public User Get(int idUser)
        {
            User user = repository.Get(idUser);
            if (user == null || user.Id <= 0)
                throw new ShoppingCartProcessingException("User Not Found");

            return user;
        }
    }
}
