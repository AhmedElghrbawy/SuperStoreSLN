using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperStore.Data;
using SuperStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Services.Services
{
    public class ShoppingCartService
    {
        private readonly StoreDBContext _storeDbContext;
        private readonly UserManager<User> _userManager;

        public ShoppingCartService(StoreDBContext storeDBContext, UserManager<User> userManager)
        {
            _storeDbContext = storeDBContext;
            _userManager = userManager;
        }

        public async Task<ShoppingCart> GetUserShoppingCartAsync(User user)
        {
            var userCart = await _storeDbContext.ShoppingCarts.Where(sh => sh.OwnerId == user.Id).FirstOrDefaultAsync();

            if (userCart != null)
                return userCart;

            userCart = new ShoppingCart { OwnerId = user.Id };
            _storeDbContext.ShoppingCarts.Add(userCart);
            await _storeDbContext.SaveChangesAsync();

            return userCart;
        }

        public async Task<ShoppingCart> AddProductAsync(Product product, ClaimsPrincipal UserClaim)
        {
            var user = await _userManager.GetUserAsync(UserClaim);

            var userCart = await this.GetUserShoppingCartAsync(user);

            userCart.Items.Add(new ShoppingCartItem { ProductId = product.Id, ShoppingCartId = userCart.Id});

            await _storeDbContext.SaveChangesAsync();

            return userCart;
        }

        public async Task<ShoppingCart> RemoveProductAsync(Product product, ClaimsPrincipal UserClaim)
        {
            var user = await _userManager.GetUserAsync(UserClaim);

            var userCart = await this.GetUserShoppingCartAsync(user);

            var item = await _storeDbContext.ShoppingCartItems.Where(item =>
             item.ProductId == product.Id && item.ShoppingCartId == userCart.Id).FirstOrDefaultAsync();

            _storeDbContext.ShoppingCartItems.Remove(item);

            await _storeDbContext.SaveChangesAsync();

            return userCart;
        }
    }
}
