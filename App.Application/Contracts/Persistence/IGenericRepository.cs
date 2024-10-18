using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence
{
    public interface IGenericRepository<T, TId> where T : class where TId : struct
    {
        public Task<bool> AnyAsync(TId id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);

        IQueryable<T> Where(Expression<Func<T,bool>> predicate);

        //method overloading
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        ValueTask<T?> GetByIDAsync(int id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
