using Fibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository.IRepository
{
    public interface IShoppingBagRepository:IRepository<ShoppingBag>
    {
        Task AddProductToBag(string userId, Guid productId);
        Task<List<ShoppingBag>> GetBag(string userId);
        Task<int> GetProductCountOnBag(string userId, Guid productId);
        Task Checkout(string userId);
    }
}
