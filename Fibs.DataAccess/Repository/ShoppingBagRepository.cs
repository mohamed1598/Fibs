using Fibs.Data;
using Fibs.DataAccess.Repository.IRepository;
using Fibs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository
{
    public class ShoppingBagRepository : Repository<ShoppingBag>, IShoppingBagRepository
    {
        public ShoppingBagRepository(ApplicationDbContext db) : base(db, db.ShoppingBags)
        {

        }

        public async Task AddProductToBag(string userId, Guid productId)
        {
            var CurrentBag = await GetFirstOrDefault(m => m.UserId == userId && m.ProductId == productId && m.isCheckedOut == false);
            var bag = createShoppingBagModel(userId, productId);
            bag.count++;
            if (CurrentBag != null)
                CurrentBag.count++;
            else
                await Add(bag);
        }

        public async Task<List<ShoppingBag>> GetBag(string userId)
        {
            return await dbSet.Where(m => m.UserId == userId && m.isCheckedOut == false).Include(m => m.Product).ToListAsync();
        }

        public async Task<int> GetProductCountOnBag(string userId, Guid productId)
        {
            var bag = await GetFirstOrDefault(m=>m.UserId == userId && m.ProductId == productId && m.isCheckedOut==false);
            return bag != null? bag.count : 0;
        }
        public async Task Checkout(string userId)
        {
            var shoppingBags = await dbSet.Where(m => m.UserId == userId && m.isCheckedOut == false).ToListAsync();
            foreach(var bag in shoppingBags)
            {
                bag.isCheckedOut = true;
                bag.BuyingTime = DateTime.Now;
            }
        }
        private ShoppingBag createShoppingBagModel(string userId,Guid productId)
        {
            ShoppingBag bag = new ShoppingBag()
            {
                UserId = userId,
                ProductId = productId,
                count = 0,
                isCheckedOut = false,
                BuyingTime = DateTime.Now
            };
            return bag;
        }
    }
}
