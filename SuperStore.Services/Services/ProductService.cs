using Microsoft.EntityFrameworkCore;
using SuperStore.Data;
using SuperStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Services.Services
{
    public class ProductService
    {
        private readonly StoreDBContext _dbContext;

        public ProductService(StoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        
        public async Task<Product> AddProductAsync(Product product)
        {
            product.ViewCount = 0;
            /*  
             *    Check for current User here
             * 
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
