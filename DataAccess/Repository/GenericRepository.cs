using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public async Task AddAsync(T entity)
        {
            await using var con = new Context();
            await con.AddAsync(entity);
            await con.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await using var con = new Context();
            con.Remove(entity);
            await con.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            await using var con = new Context();
            return await con.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            await using var con = new Context();
            return await con.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            await using var con = new Context();
            return await con.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await using var con = new Context();
            con.Update(entity);
            await con.SaveChangesAsync();
        }
    }
}
