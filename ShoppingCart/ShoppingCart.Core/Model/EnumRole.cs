namespace ShoppingCart.Model
{
    public enum EnumRole
    {
        CUSTOMER,
        OWNER
    }

    public static class ExtensionEnumRole
    {
        
        public static bool BeAbleToCheckProductsOutOfStock(this EnumRole role)
        {
            return role == EnumRole.OWNER;
        }

        public static bool IsAllowedToShop(this EnumRole role)
        {
            return role == EnumRole.CUSTOMER;
        }
    }
 }


   
