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
        private readonly ShoppingCartService _shoppingCartService;

        public ProductController(CategoryService categoryService, ProductService productService, ShoppingCartService shoppingCartService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            var userCart = await _shoppingCartService.GetUserShoppingCartAsync(this.User);
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
                Reviews = p.Reviews,
                Owner = p.Owner,
                InCart = userCart.Items?.Any(item => item.ProductId == p.Id) ?? false,
                Category = p.Category
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
            var productViewModel = new ProductViewModel
            {
                CategorySelectList =
                new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName")
            };
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _productService.GetProductByIdAsync((int) id);

            if (product == null)
            {
                return NotFound();
            }    

            var userCart = await _shoppingCartService.GetUserShoppingCartAsync(this.User);

            var productViewModel = new ProductViewModel
            {
                AmountAvailable = product.AmountAvailable,
                Title = product.Title,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ImageData = product.Image,
                Id = product.Id,
                OwnerId = product.OwnerId,
                Price = product.Price,
                Reviews = product.Reviews,
                Owner = product.Owner,
                InCart = userCart.Items?.Any(item => item.ProductId == product.Id) ?? false,
                Category = product.Category
            };

            return View(productViewModel);
        }


    }
}
