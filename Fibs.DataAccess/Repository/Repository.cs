using Fibs.Data;
using Fibs.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db, DbSet<T> dbSet)
        {
            _db = db;
            this.dbSet = dbSet;
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet.Where(filter);
            if (query == null) return null;
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
