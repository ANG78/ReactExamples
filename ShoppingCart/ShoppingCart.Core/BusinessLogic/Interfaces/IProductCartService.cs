using ShoppingCart.Model;

namespace ShoppingCart.Core.BusinessLogic
{
    public interface IProductCartService
    {
        CartProcessingResult ProcessCart(int idUser, Cart cart);
    }
}
