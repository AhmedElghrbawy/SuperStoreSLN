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

        public async Task<ShoppingCart> GetUserShoppingCartAsync(ClaimsPrincipal userClaim)
        {
            var user = await _userManager.GetUserAsync(userClaim);

            var userCart = await _storeDbContext.ShoppingCarts.Where(sh => sh.OwnerId == user.Id)
                .Include(sh => sh.Items)
                .ThenInclude(item => item.Product)
                .ThenInclude(p => p.Owner)
                .FirstOrDefaultAsync();

            if (userCart != null)
                return userCart;

            userCart = new ShoppingCart { OwnerId = user.Id };
            _storeDbContext.ShoppingCarts.Add(userCart);
            await _storeDbContext.SaveChangesAsync();

            return userCart;
        }

        public async Task<ShoppingCart> AddProductAsync(int productId, ClaimsPrincipal userClaim)
        {

            var userCart = await this.GetUserShoppingCartAsync(userClaim);

            userCart.Items.Add(new ShoppingCartItem { ProductId = productId, ShoppingCartId = userCart.Id});

            await _storeDbContext.SaveChangesAsync();

            return userCart;
        }

        public async Task<ShoppingCart> RemoveProductAsync(int productId, ClaimsPrincipal userClaim)
        {

            var userCart = await this.GetUserShoppingCartAsync(userClaim);

            var item = await _storeDbContext.ShoppingCartItems.Where(item =>
             item.ProductId == productId && item.ShoppingCartId == userCart.Id)
                .FirstOrDefaultAsync();

            if (item == null)
                return userCart;

            _storeDbContext.ShoppingCartItems.Remove(item);

            await _storeDbContext.SaveChangesAsync();

            return userCart;
        }
    }
}
