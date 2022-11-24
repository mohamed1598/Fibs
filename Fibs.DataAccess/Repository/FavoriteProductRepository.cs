using Fibs.Data;
using Fibs.DataAccess.Repository.IRepository;
using Fibs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository
{
    public class FavoriteProductRepository : Repository<FavoriteProduct>, IFavoriteProductRepository
    {
        public FavoriteProductRepository(ApplicationDbContext db) : base(db, db.FavoriteProducts)
        {
        }

        // get all user's favorites
        public async Task<IEnumerable<Product>> GetUsersFavorites(string userId)
        {
            var favoriteProducts = await dbSet.Where(m => m.UserId == userId).Select(e => e.Product).ToListAsync();
            return favoriteProducts;
        }

        // toggle product from favorite to not favorite and vice versa
        public async Task Toggle(string userId ,Guid productId)
        {
            FavoriteProduct favoriteProduct = CreateFavoriteModel(userId, productId);
            if (await IsFavorite(userId, productId))
            {
                var favoriteProducts = dbSet.Where(m => m.UserId == favoriteProduct.UserId && m.ProductId == favoriteProduct.ProductId);
                RemoveRange(favoriteProducts);
            }
            else
                await base.Add(favoriteProduct);
        }

        // check the product is favorite or not
        public async Task<bool> IsFavorite(string userId, Guid productId)
        {
            
            return await GetFirstOrDefault(m => m.UserId == userId && m.ProductId == productId)!= null ? true : false;
        }

        // create productfavorite model
        private FavoriteProduct CreateFavoriteModel(string userId, Guid productId)
        {
            FavoriteProduct favoriteProduct = new FavoriteProduct()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                UserId = userId
            };
            return favoriteProduct;
        }
    }
}
