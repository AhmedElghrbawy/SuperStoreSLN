using Microsoft.AspNetCore.Mvc;
using SuperStore.Services.Services;
using SuperStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly ShoppingCartService _shoppingCartService;

        public CategoryController(CategoryService categoryService, ProductService productService, ShoppingCartService shoppingCartService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategoriesAsync());
        }

        public async Task<IActionResult> Products(int categoryId)
        {
            ViewBag.CategoryName = (await _categoryService.GetCategoryByIdAsync(categoryId)).CategoryName;
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
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
                InCart = userCart.Items.Any(item => item.ProductId == p.Id),
                Category = p.Category
            });
            return View(productViewModels);
        }
    }
}
