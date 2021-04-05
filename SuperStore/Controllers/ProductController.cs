using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public async Task<IActionResult> Create(ProductViewModel receivedProduct)
        {
            if (!string.IsNullOrEmpty(receivedProduct.Title))
            {
                return View(receivedProduct);
            }

            ModelState.Clear();
            var productViewModel = new ProductViewModel();
            productViewModel.CategorySelectList =
                new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", productViewModel);
            }

            bool validCategory = (await _categoryService.GetAllCategoriesAsync())
                .Any(c => c.CategoryId == productViewModel.CategoryId);

            if (!validCategory)
            {
                ModelState.AddModelError("CategoryId", "Pleas Enter a valid Category");
                return RedirectToAction("Create", productViewModel);
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

            if (await _productService.CreateProductAsync(productModel, User) == null)
            {
                return BadRequest();
            }


            

            return RedirectToAction("Index");
        }
    }
}
