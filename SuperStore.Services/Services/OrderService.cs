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
    public class OrderService
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly StoreDBContext _storeDbContext;
        private readonly UserManager<User> _userManager;

        public OrderService(ShoppingCartService shoppingCartService, 
            StoreDBContext storeDBContext, UserManager<User> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _storeDbContext = storeDBContext;
            _userManager = userManager;
        }


        public async Task<Order> MakeAnOrderAsync(ClaimsPrincipal userClaim)
        {
            var user = await _userManager.GetUserAsync(userClaim);
            var userCart = await _shoppingCartService.GetUserShoppingCartAsync(userClaim);

            var order = new Order
            {
                Owner = user,
                Items = userCart.Items.Select(shoppingCartItem => new OrderItem
                {
                    Amount = shoppingCartItem.Amount,
                    Product = shoppingCartItem.Product,
                })
            };

            _storeDbContext.Add(order);
            await _storeDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrderHistoryAsync(ClaimsPrincipal userClaim)
        {
            var user = await _userManager.GetUserAsync(userClaim);

            return await _storeDbContext.Orders.Where(ord => ord.OwnerId == user.Id)
                .Include(ord => ord.Items)
                .ToListAsync();
        }
    }
}
