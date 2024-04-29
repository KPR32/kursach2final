using FurnitureStore3.Data;
using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FurnitureStore3.Infrastructure
{
    public class EFRepository<T> : IRepository<T> where T : Entity
    {
        private readonly FurnitureContext context;

        public EFRepository(FurnitureContext context)
        {
            this.context = context;
        }
        public async Task<T?> FindAsync(int Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> FindWhere(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync1()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> FindWhere1(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> AddAsync1(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
