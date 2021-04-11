using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Services.Services;
using SuperStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public CartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> Index()
        {
            var UserCart = await _shoppingCartService.GetUserShoppingCartAsync(this.User);

            var cartViewModel = new ShoppingCartViewModel
            {
                Id = UserCart.Id,
                OwnerId = UserCart.OwnerId,
                Items = UserCart.Items.Select(item => new ShoppingCartItemViewModel
                {
                    Id = item.Id,
                    Amount = item.Amount,
                    Product = new ProductViewModel
                    {
                        AmountAvailable = item.Product.AmountAvailable,
                        Title = item.Product.Title,
                        Description = item.Product.Description,
                        CategoryId = item.Product.CategoryId,
                        ImageData = item.Product.Image,
                        Id = item.Product.Id,
                        OwnerId = item.Product.OwnerId,
                        Price = item.Product.Price,
                        Reviews = item.Product.Reviews,
                        Owner = item.Product.Owner,
                        Category = item.Product.Category
                    }
                })

            };

            return View(cartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int amount)
        {
            if (await _shoppingCartService.AddProductAsync(productId, amount, this.User) == null)
                return BadRequest();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int productId)
        {
            await _shoppingCartService.RemoveProductAsync(productId, this.User);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAmount(int shoppingCartItemId, int newAmount)
        {
            if (await _shoppingCartService.ModifyShoppingCartItemAmountAsync(shoppingCartItemId, newAmount, User) == null)
                return BadRequest();

            return RedirectToAction("Index");
        }
    }


    
}

