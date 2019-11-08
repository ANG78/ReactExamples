

using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.BusinessLogic;
using ShoppingCart.Model;

namespace ShoppingCart.Core.Persistence
{
    public class ProductStockRespository : DataRepository, IProductStockRespository
    {
        public IEnumerable<ProductStock> GetProductByName(string textIncludedInName, bool bringProductsInStockOutOfStock)
        {
            IEnumerable<ProductStock> result = null;

            if (bringProductsInStockOutOfStock)
            {
                result = ProductsInStock;
            }
            else
            {
                result = ProductsInStock
                            .Where(z => z.Quantity > 0 ||
                                   (bringProductsInStockOutOfStock))
                            .ToList();

            }

            if (string.IsNullOrWhiteSpace(textIncludedInName))
            {
                return result;
            }
            textIncludedInName = textIncludedInName.Trim();
            return result.Where(x => x.Product.Name.Contains(textIncludedInName));
        }

        public IList<ProductStock> GetStockOfProducts(List<int> listIdsOfProducts)
        {
            return ProductsInStock.Where(x => listIdsOfProducts.Contains(x.Product.Id)).ToList();
        }

        public void RemoveStock(List<ProductStock> reqToBook)
        {
            foreach(var it in reqToBook)
            {
                var productStock = ProductsInStock.FirstOrDefault(x => x.Product.Id == it.Product.Id);
                if ( productStock.Quantity - it.Quantity < 0)
                {
                    throw new ShoppingCartProcessingException(" productStock.Quantity will be negative ");
                }
                productStock.Quantity = productStock.Quantity - it.Quantity;
            }
        }
    }
}
