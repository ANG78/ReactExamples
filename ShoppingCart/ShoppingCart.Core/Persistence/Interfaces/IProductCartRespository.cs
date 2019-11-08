using ShoppingCart.Model;

namespace ShoppingCart.Core.Persistence
{
    public interface IProductCartRespository
    {
        void Save(Cart cart);
    }
}