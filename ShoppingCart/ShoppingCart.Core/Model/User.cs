using System;

namespace ShoppingCart.Model
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public abstract EnumRole Role { get; }
        public string Password { get; set; }
    }


    public class Owner : User
    {
        public override EnumRole Role { get { return EnumRole.OWNER; } }
    }

    public class Customer : User
    {
        public override EnumRole Role { get { return EnumRole.CUSTOMER; } }
    }
}



   
