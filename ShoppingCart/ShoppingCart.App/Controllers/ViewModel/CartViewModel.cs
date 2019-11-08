using System.Collections.Generic;

namespace ReactVersion.Controllers
{
    public class CartViewModel
    {
        public int IdUser { get; set; }
        public List<ProductCartViewModel> Products { get; set; }
    }
}
