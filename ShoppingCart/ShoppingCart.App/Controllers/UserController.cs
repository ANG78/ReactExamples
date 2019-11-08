using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.BusinessLogic;
using ShoppingCart.Model;

namespace ReactVersion.Controllers
{
    public class UserViewModel{
        public int id;    
        public string name = "" ;
        public string role;
    }

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IUserService service { get; set; }

        public UserController(IUserService pservice)
        {
            service = pservice;
        }
        
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<UserViewModel> Get(string user, string password)
        {
            try
            {
                User result = service.Login(user, password);
                return new UserViewModel {
                    id = result.Id,
                    name = result.Identifier,
                    role = result.Role.ToString()
                };
            }
            catch (ShoppingCartProcessingException ex)
            {
                return NotFound(ex.Message);
            }
        }
       
    }
}
