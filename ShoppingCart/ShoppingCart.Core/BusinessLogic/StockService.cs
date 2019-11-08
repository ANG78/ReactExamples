using System;
using ShoppingCart.Core.Persistence;
using ShoppingCart.Model;

namespace ShoppingCart.Core.BusinessLogic
{
    public class ProductCartService : IProductCartService
    {
        public static object Locker = new object();
        IProductCartRespository repository { get; set; }
        IProductStockService productStockService { get; set; }

        IUserService userService { get; set; }
        public ProductCartService(IProductCartRespository prepository, 
                                  IProductStockService pproductStockService,
                                  IUserService puserService) {
            repository = prepository;
            productStockService = pproductStockService;
            userService = puserService;
        }

       
        public CartProcessingResult ProcessCart(int idUser, Cart cart)
        {
            CartProcessingResult result = CompleteAndValidateObject(idUser, cart);
            if ( result.IsOk)
            {
                Save(cart);
            }
            
            return result;
        }

        /// <summary>
        /// complete the user and the ids of the products
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="cart"></param>
        private CartProcessingResult CompleteAndValidateObject(int idUser, Cart cart) {

            User user = userService.Get(idUser);
            if (!user.Role.IsAllowedToShop())
            {
                throw new ShoppingCartProcessingException("Purchase is not allowed");
            }

            cart.Owner = user;
            cart.PurchasingDate = DateTime.Now;

            return productStockService.ValidateStock(cart.Products);
        }

        private void Save(Cart cart) {
            lock (Locker)
            {    
                productStockService.UpdateStock(cart.Products);
                repository.Save(cart);
            }
        }
    }
}
