using System;

namespace ShoppingCart.Core.BusinessLogic
{
    public class ShoppingCartProcessingException : Exception
    {
        public ShoppingCartProcessingException(string message) : base(message) {
            
        }

    }
}
