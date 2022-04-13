using bootShop.Business;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Add(int id) 
        {
            if (await _productService.IsExists(id))
            {
                var product = await _productService.GetEntityById(id);
                return Json($"{product.Name }Sepete eklendi");
            }
            return NotFound();
        }
    }
}
