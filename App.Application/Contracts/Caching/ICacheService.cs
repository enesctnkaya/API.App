using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Contracts.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string cachekey);

        Task AddAsync<T>(string cachekey, T value, TimeSpan exprTimeSpan);

        Task RemoveAsync(string cachekey);
    }
}
