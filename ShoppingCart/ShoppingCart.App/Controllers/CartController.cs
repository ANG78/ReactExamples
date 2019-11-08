using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.BusinessLogic;
using ShoppingCart.Model;

namespace ReactVersion.Controllers
{

   



    [Route("api/[controller]")]
    public class ProductCartController : Controller
    {
        IProductCartService service { get; set; }
        public ProductCartController(IProductCartService pservice)
        {
            service = pservice;
        }

        [HttpPost]
        public ActionResult<CartViewModel> Post([FromBody] CartViewModel cart)
        {
            try
            {
                //mapping viemodel
                Cart cartPar = new Cart
                {
                    Owner = new Customer
                    {
                        Id = cart.IdUser
                    },
                    PurchasingDate = DateTime.Now,
                    Products = cart.Products.Select(x => new ProductCart
                    {
                        Product = new Product
                        {
                            Id = x.IdProduct
                        },
                        Quantity = x.Quantity
                    }).ToList()
                };

                var result = service.ProcessCart(cart.IdUser, cartPar);

                CartProcessingResultViewModel resultViewModel = new CartProcessingResultViewModel
                {
                    IsOk = result.IsOk,
                    Results = result.Result.Select(x => new ProductCartProcessingResultViewModel
                    {
                        IdProduct = x.Product.Id,
                        Message = x.Message
                    }).ToList()
                };

                return Ok(resultViewModel);
            }
            catch (ShoppingCartProcessingException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
