using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories
{
    public class CategoryRepository(AppDbContext context)
        : GenericRepository<Category, int>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.ID == id);
        }

        public Task<List<Category>> GetCategoryWithProductsAsync()
        {
            return context.Categories.Include(x => x.Products).ToListAsync();
        }

        public IQueryable<Category> GetCategoryWithProducts()
        // Include methodu veritabanından veri çekerken ilgili
        // (ilişkilendirilmiş) verileri de yüklemek için kullanılır.
        {
            return context.Categories.Include(x => x.Products).AsQueryable();
        }
    }
}
