using bootShop.Business;
using bootShop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllEntities();
            
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var selectedCategory = new List<SelectListItem>();
            var items = await _categoryService.GetAllEntities();
            foreach (var item in items)
            {
                selectedCategory.Add
                    (
                    new SelectListItem { Text = item.Name, Value = item.Id.ToString() }
                    );
            }

            ViewBag.SelectedCategory = selectedCategory;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product) 
        {

            if (ModelState.IsValid) 
            {
                if (product.Id != 0)
                {
                    int updatedProduct = await _productService.UpdateEntity(product);
                    return RedirectToAction(nameof(Index));
                }
                int addedProductId = await _productService.AddEntity(product);
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        public async Task<IActionResult> Update(int id) 
        {
            var selectedCategory = new List<SelectListItem>();
            var items = await _categoryService.GetAllEntities();
            foreach (var item in items)
            {
                selectedCategory.Add
                    (
                    new SelectListItem { Text = item.Name, Value = item.Id.ToString() }
                    );
            }
            ViewBag.SelectedCategory = selectedCategory;
            var product = await _productService.GetEntityById(id);
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Index", "Products");
        }
    }
}
