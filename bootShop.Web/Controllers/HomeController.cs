using bootShop.Business;
using bootShop.Entities;
using bootShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace bootShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page=1, int? catId=0)
        {
            var productsFromService = await _productService.GetAllEntities();
            var products = catId == 0 ? productsFromService.ToList() : productsFromService.Where(x=> x.CategoryId == catId).ToList();
            var productsPerPage = 3;
            var paginatedProducts = products.OrderByDescending(p => p.Id)
                                            .Skip((page-1) * productsPerPage)
                                            .Take(productsPerPage);
            ViewBag.CurrentPage = page;
            ViewBag.Category = catId;
            ViewBag.TotalPages = Math.Ceiling((decimal)products.Count / productsPerPage);
            return View(paginatedProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
