using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(CategoryService categoryService, ProductService productService, ShoppingCartService shoppingCartService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            var userCart = await _shoppingCartService.GetUserShoppingCartAsync(this.User);
            var productViewModels = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products, opt => opt.Items["cart"] = userCart);

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
            

            var productModel = _mapper.Map<Product>(productViewModel);

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

            var productViewModel = _mapper.Map<ProductViewModel>(product, opt => opt.Items["cart"] = userCart);
            var viewModel = new ProductDetailsViewModel { Product = productViewModel };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(Review review)
        {

            if (await _productService.AddReviewAsync(review, User) == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Details", new { id = review.ProductId });
        }

    }
}
