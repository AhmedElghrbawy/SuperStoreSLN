using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperStore.Services.Services;
using SuperStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly CategoryService _categoryService;

        public ProductController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.CategorySelectList =
                new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            productViewModel.ImageFormFile = Request.Form.Files["ImageFile"];

            return View("Index");
        }
    }
}
