using bootShop.Business;
using bootShop.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _categoryService.GetAllEntities();
            return View(values);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id != 0)
                {
                    var id = await _categoryService.UpdateEntity(category);
                    return RedirectToAction("Index", "Categories");
                }
                else
                {
                    var id = await _categoryService.AddEntity(category);
                    return RedirectToAction("Index", "Categories");
                }
            }
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetEntityById(id);
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("Index", "Categories");
        }

    }
}
