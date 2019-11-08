using System;
using System.Collections.Generic;

namespace ShoppingCart.Model
{

    public class Cart
    {
        public int Id{get;set;}
        public User Owner { get; set; }
        public DateTime PurchasingDate { get; set; }
        public List<ProductCart> Products { get; set; } = new List<ProductCart>();
        public Cart() {

        }
    }

}
