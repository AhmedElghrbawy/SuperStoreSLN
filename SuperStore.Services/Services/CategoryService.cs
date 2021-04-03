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
    public class CategoryService
    {
        private readonly StoreDBContext _dbContext;

        public CategoryService(StoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }    
        

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync();
        }
    }
}
