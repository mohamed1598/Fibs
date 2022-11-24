using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; set; }
        public IFavoriteProductRepository FavoriteProducts { get; set; }
        public IShoppingBagRepository ShoppingBags { get; set; }
        void Save();
        Task SaveAsync();
    }
}
