﻿
namespace ShoppingCart.Model
{

    public enum EnumProductStockState
    {
        AVAILABLE,
        BLOCKED,
        ASSIGNED
    }

    public class ProductStock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity {get;set;}
    }
}
