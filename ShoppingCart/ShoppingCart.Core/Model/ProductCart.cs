namespace ShoppingCart.Model
{
    public class ProductCart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Message { get; set; }
    }

}
