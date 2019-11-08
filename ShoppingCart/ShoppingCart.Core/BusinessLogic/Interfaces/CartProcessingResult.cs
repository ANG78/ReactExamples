using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Core.BusinessLogic
{
    public class ProductCartResult {
        public Product Product { get; set; }
        public string Message { get; set; }
    }
    public class CartProcessingResult
    {
        private List<ProductCartResult> items = new List<ProductCartResult>();

        public IEnumerable<ProductCartResult> Result { get { return items; } }
        public bool IsOk => items.Count == 0;
        public void Add(Product product, string messageError)
        {
            items.Add( new ProductCartResult() { Product = product, Message= messageError });
        }
    }
}
