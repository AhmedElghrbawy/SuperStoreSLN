using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Data.Models;
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
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, ProductService productService
            , ShoppingCartService shoppingCartService, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
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
            var productViewModels = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products, opt => opt.Items["cart"] = userCart);
            return View(productViewModels);
        }
    }
}
