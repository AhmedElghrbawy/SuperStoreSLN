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
    public class ProductService
    {
        private readonly StoreDBContext _dbContext;
        private readonly UserManager<User> _userManager;

        public ProductService(StoreDBContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        
        public async Task<Product> CreateProductAsync(Product product, ClaimsPrincipal UserClaim)
        {
            var user = await _userManager.GetUserAsync(UserClaim);
            product.ViewCount = 0;
            product.OwnerId = user.Id;


            /* TODO
             * 
             * set Product image here or in view
             */


            _dbContext.Add(product);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return product;
        }
    }
}
