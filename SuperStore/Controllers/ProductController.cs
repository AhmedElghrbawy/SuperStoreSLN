using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperStore.Data.Models;
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
        private readonly ProductService _productService;

        public ProductController(CategoryService categoryService, ProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();

            var productViewModels = products.Select(p => new ProductViewModel
            {
                AmountAvailable = p.AmountAvailable,
                Title = p.Title,
                Description = p.Description,
                CategoryId = p.CategoryId,
                ImageData = p.Image,
                Id = p.Id,
                OwnerId = p.OwnerId,
                Price = p.Price,
                Reviews = p.Reviews
            });

            return View(productViewModels);
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
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }
            productViewModel.ImageFormFile = Request.Form.Files["ImageFile"];
            var productModel = new Product
            {
                Title = productViewModel.Title,
                Description = productViewModel.Description,
                AmountAvailable = productViewModel.AmountAvailable,
                CategoryId = productViewModel.CategoryId,
                Image = productViewModel.ImageData,
                Price = productViewModel.Price,
            };

            await _productService.CreateProductAsync(productModel, User);

            

            return RedirectToAction("Index");
        }
    }
}
