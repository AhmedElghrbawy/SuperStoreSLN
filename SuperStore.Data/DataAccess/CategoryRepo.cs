using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Data.DataAccess
{
    public class CategoryRepo
    {
        private readonly StoreDBContext _db;

        public CategoryRepo(StoreDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _db.Categories.AsNoTracking().ToListAsync();
        }
    }
}
