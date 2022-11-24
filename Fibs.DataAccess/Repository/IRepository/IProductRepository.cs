using Fibs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.DataAccess.Repository.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        Task Update(Product product, IFormFileCollection images);
        Task Add(Product product, IFormFileCollection images);
        Task IncreaseProductFavorites(Guid productId);
        Task DecreaseProductFavorites(Guid productId);
    }
}
