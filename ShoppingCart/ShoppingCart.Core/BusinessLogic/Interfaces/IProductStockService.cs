using System.Collections.Generic;
using ShoppingCart.Model;

namespace ShoppingCart.Core.BusinessLogic
{
    public interface IProductStockService
    {
        IEnumerable<ProductStock> Get(int idUser, string text);
        CartProcessingResult ValidateStock(List<ProductCart> products);
        void UpdateStock(List<ProductCart> products);
    }
}
