
using System.Collections.Generic;
using ShoppingCart.Model;

namespace ShoppingCart.Core.Persistence
{
    public interface IProductStockRespository
    {
        IEnumerable<ProductStock> GetProductByName(string text, bool v);
        IList<ProductStock> GetStockOfProducts(List<int> list);
        void RemoveStock(List<ProductStock> reqToBook);
    }
}