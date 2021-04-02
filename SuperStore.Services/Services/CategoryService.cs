using SuperStore.Data;
using SuperStore.Data.DataAccess;
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
        private readonly CategoryRepo categoryRepo;

        public CategoryService(CategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await categoryRepo.GetCategories();
        }
    }
}
