using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.BusinessLogic;
using ShoppingCart.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactVersion.Controllers
{
    [Route("api/[controller]")]
    public class ProductStockController : Controller
    {
        IProductStockService service { get; set; }
        public ProductStockController(IProductStockService pservice)
        {
            service = pservice;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<ProductStock>> Get(int idUser, string text)
        {
            try
            {
                IEnumerable<ProductStock> result = service.Get(idUser, text);
                return Ok(result);
            }
            catch (ShoppingCartProcessingException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
