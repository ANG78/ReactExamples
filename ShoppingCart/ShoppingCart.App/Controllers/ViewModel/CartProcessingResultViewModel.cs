using System.Collections.Generic;

namespace ReactVersion.Controllers
{
    public class CartProcessingResultViewModel
    {
        public bool IsOk { get; set; }
        public IList<ProductCartProcessingResultViewModel> Results { get; set; }
    }
}
