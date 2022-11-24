using Fibs.Data;
using Fibs.DataAccess.Repository.IRepository;
using Fibs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db) : base(db, db.Products)
        {

        }
        public async Task Add(Product product, IFormFileCollection images)
        {
            await UpdateProductImage(product, images);
            await Add(product);

        }

        public async Task DecreaseProductFavorites(Guid productId)
        {
            var currentProduct = await GetFirstOrDefault(m => m.Id == productId);
            currentProduct.FavoritesNo -= 1;
        }

        public async Task IncreaseProductFavorites(Guid productId)
        {
            var currentProduct = await GetFirstOrDefault(m => m.Id == productId);
            currentProduct.FavoritesNo += 1;
        }

        public async Task Update(Product product, IFormFileCollection images)
        {
            var currentProduct = await GetFirstOrDefault(m => m.Id == product.Id);
            currentProduct.Name = product.Name;
            currentProduct.Price = product.Price;
            currentProduct.NoOfPieces = product.NoOfPieces;
            currentProduct.Material = product.Material;
            currentProduct.Details = currentProduct.Details;
            currentProduct.Size = product.Size;
            await UpdateProductImage(currentProduct, images);
        }

        private async Task UpdateProductImage(Product product, IFormFileCollection images)
        {
            if (images.Count > 0)
            {
                var file = images.FirstOrDefault();
                using (var datastream = new MemoryStream())
                {
                    await file.CopyToAsync(datastream);
                    product.Picture = datastream.ToArray();
                }
            }
        }
    }
}
