using Fibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository.IRepository
{
    public interface IFavoriteProductRepository:IRepository<FavoriteProduct>
    {
        Task Toggle(string userId, Guid productId);
        Task<IEnumerable<Product>> GetUsersFavorites(string userId);
        Task<bool> IsFavorite(string userId,Guid productId);
    }
}
