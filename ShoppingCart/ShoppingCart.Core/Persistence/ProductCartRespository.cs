using ShoppingCart.Model;

namespace ShoppingCart.Core.Persistence
{
    public class ProductCartRespository : DataRepository, IProductCartRespository
    {
        public void Save(Cart cart)
        {
            cart.Id = GetId();
            Carts.Add(cart);
        }
    }


}
