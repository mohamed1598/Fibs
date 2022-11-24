using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
