using bootShop.Business;
using bootShop.Dtos.Requests;
using bootShop.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
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
            ViewBag.SelectedCategory = await GetCategoriesForDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Name", "Price", "Discount", "Description", "CategoryId", "ImageUrl")] AddProductRequest request)
        public async Task<IActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {
                int addedProductId = await _productService.AddEntity(product);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.SelectedCategory = await GetCategoriesForDropDown();
            if (await _productService.IsExists(id))
            {
                var product = await _productService.GetEntityByIdforUpdate(id);
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductRequest product)
        {
            if (ModelState.IsValid)
            {
                int affectedRows = await _productService.UpdateEntityDto(product);
                if (affectedRows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            ViewBag.Categories = GetCategoriesForDropDown();
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _productService.IsExists(id))
            {
                await _productService.SoftDelete(id);
                return RedirectToAction("Index", "Products");
            }

            return NotFound();
        }

        private async Task<List<SelectListItem>> GetCategoriesForDropDown()
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
            return selectedCategory;
        }
    }
}
