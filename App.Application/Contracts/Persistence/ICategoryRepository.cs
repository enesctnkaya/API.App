using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {

        Task<Category?> GetCategoryWithProductsAsync(int id);
        Task<List<Category?>> GetCategoryWithProductsAsync();

    }
}
