using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Persistence;
using ShoppingCart.Model;

namespace ShoppingCart.Core.BusinessLogic
{

    public class ProductStockService : IProductStockService
    {
        public IUserService userService { get; set; }
        public IProductStockRespository repository { get; set; }
        public ProductStockService(IUserService puserService, IProductStockRespository pRepository) {
            userService = puserService;
            repository = pRepository;
        }
        public IEnumerable<ProductStock> Get(int idUser, string text)
        {
            if (idUser > 0)
            {
                User user = userService.Get(idUser);
                return repository.GetProductByName(text, user.Role.BeAbleToCheckProductsOutOfStock());
            }

            return repository.GetProductByName(text, false);
        }

        public CartProcessingResult ValidateStock(List<ProductCart> products)
        {
            CartProcessingResult result = new CartProcessingResult();

            IList<ProductStock> stocks = repository.GetStockOfProducts(products.Select(x => x.Product.Id).ToList());

            foreach (var prodCart in products) {
                var prodStock = stocks.FirstOrDefault(p => p.Product.Id == prodCart.Product.Id);
                if (prodStock == null)
                {
                    result.Add(prodCart.Product, "Product is not available in Stock" + prodCart.Product.Id);
                }
                    
                if (prodStock.Quantity < prodCart.Quantity) {
                    result.Add(prodCart.Product, "Quant. in Stock (" + prodStock.Quantity + ") is less than Quant. Required (" + prodCart.Quantity + ")");
                }
            }

            return result;
        }
        public void UpdateStock(List<ProductCart> productsInCart)
        {
            var reqToBook = productsInCart.Select(x => new ProductStock() { Product = x.Product, Quantity= x.Quantity}).ToList();
            repository.RemoveStock(reqToBook);
        }

        
    }
}
