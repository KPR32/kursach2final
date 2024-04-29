using FurnitureStore3.Domain.Entities;
using System.Linq.Expressions;

namespace FurnitureStore3.Domain.Services
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> FindAsync(int Id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync1();
        Task<List<T>> FindWhere(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindWhere1(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> AddAsync1(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
