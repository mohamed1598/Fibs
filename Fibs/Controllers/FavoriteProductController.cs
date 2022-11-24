using Fibs.DataAccess.Repository.IRepository;
using Fibs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fibs.Controllers
{
    [Authorize]
    public class FavoriteProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public FavoriteProductController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var products = await _unitOfWork.FavoriteProducts.GetUsersFavorites(userId);
            return View(products);
        }
        public async Task<IActionResult> AddToFavorites(Guid Id)
        {
            await manageProductsFavoritesNumber(Id);
            await _unitOfWork.FavoriteProducts.Toggle(_userManager.GetUserId(User), Id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Details","Products",new {id = Id});
        }

        private async Task manageProductsFavoritesNumber(Guid productId)
        {
            if (await _unitOfWork.FavoriteProducts.IsFavorite(_userManager.GetUserId(User), productId))
                await _unitOfWork.Products.DecreaseProductFavorites(productId);
            else
                await _unitOfWork.Products.IncreaseProductFavorites(productId);
        }
    }
}
