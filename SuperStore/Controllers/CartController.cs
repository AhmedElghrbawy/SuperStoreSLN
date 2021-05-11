using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public CartController(ShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var userCart = await _shoppingCartService.GetUserShoppingCartAsync(this.User);
            var cartViewModel = _mapper.Map<ShoppingCart, ShoppingCartViewModel>(userCart, opt => opt.Items["cart"] = userCart);


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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearNotifications()
        {
            await _shoppingCartService.ClearNotificationsAsync(User);


            return RedirectToAction("Index");
        }
    }


    
}

