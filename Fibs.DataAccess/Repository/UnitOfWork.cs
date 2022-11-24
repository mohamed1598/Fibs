using Fibs.Data;
using Fibs.DataAccess.Repository.IRepository;
using Fibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Products = new ProductRepository(db);
            FavoriteProducts = new FavoriteProductRepository(db);
            ShoppingBags = new ShoppingBagRepository(db); 
        }
        public IProductRepository Products { get; set; }
        public IFavoriteProductRepository FavoriteProducts { get; set; }
        public IShoppingBagRepository ShoppingBags { get; set; }
        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
