
using ShoppingCart.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Core.Persistence
{

    public class DataRepository
    {
        private static int SequenceIds = 0;
        private static object Locker = new object();

        protected static IList<User> Users = null;
        protected static IList<Product> Products = null;
        protected static IList<ProductStock> ProductsInStock = null;
        protected static IList<Cart> Carts = new List<Cart>();

        protected static int GetId()
        {
            lock (Locker)
            {
                return ++SequenceIds;
            }
        }




        public DataRepository()
        {
            LoadScenarios();
        }

        private void LoadScenarios()
        {
            lock (Locker)
            {
                if (Users != null)
                {
                    return;
                }
            }
            Users = new List<User> {
                new Owner {
                    Id = GetId(),
                    Identifier = "Owner"
                },
                new Customer
                {
                    Id = GetId(),
                    Identifier = "user1"
                },
                new Customer
                {
                    Id = GetId(),
                    Identifier = "user2"
                }
            };

            Products = new List<Product> {
                new Product{
                    Id = GetId(),
                    Reference = "REF-0001",
                    Name = "Angular Ready",
                    Description = " book about Angular",
                    Price = 20.2
                },
                new Product{
                    Id = GetId(),
                    Reference = "REF-0002",
                    Name = "TypeScript in Action",
                    Description = " Book about TypeScript",
                    Price = 32.27
                },
                new Product{
                    Id = GetId(),
                    Reference = "REF-0003",
                    Name = "Asp.net Core jump start",
                    Description = " Book about asp.net core",
                    Price = 50.2
                },
                new Product{
                    Id = GetId(),
                    Reference = "REF-0004",
                    Name = "Docker for dummies",
                    Description = " Book about docker",
                    Price = 15.6
                }
            };

            ProductsInStock = new List<ProductStock>{
                new ProductStock{
                    Id = GetId(),
                    Product= Products.First(x=>x.Reference == "REF-0001" ),
                    Quantity= 100
                },
                 new ProductStock{
                    Id = GetId(),
                    Product= Products.First(x=>x.Reference == "REF-0002" ),
                    Quantity= 25
                },
                  new ProductStock{
                    Id = GetId(),
                    Product= Products.First(x=>x.Reference == "REF-0003" ),
                    Quantity= 26
                },
                   new ProductStock{
                    Id = GetId(),
                    Product= Products.First(x=>x.Reference == "REF-0004" ),
                    Quantity= 33
                }
            };

        }
    }
}
