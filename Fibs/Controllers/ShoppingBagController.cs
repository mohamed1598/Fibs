using Fibs.DataAccess.Repository.IRepository;
using Fibs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fibs.Controllers
{
    [Authorize]
    public class ShoppingBagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingBagController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            List<ShoppingBag> bags = await _unitOfWork.ShoppingBags.GetBag(userId);
            return View(bags);
        }
        public async Task<IActionResult> Add(Guid Id,string ControllerName)
        {
            await _unitOfWork.ShoppingBags.AddProductToBag(_userManager.GetUserId(User), Id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index",ControllerName);
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var bag = await _unitOfWork.ShoppingBags.GetFirstOrDefault(m => m.Id == Id);
            _unitOfWork.ShoppingBags.Remove(bag);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeQuantity(Guid Id , int Count)
        {
            var bag = await _unitOfWork.ShoppingBags.GetFirstOrDefault(m => m.Id == Id);
            bag.count = Count;
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Checkout()
        {
            await _unitOfWork.ShoppingBags.Checkout(_userManager.GetUserId(User));
            var bags = await _unitOfWork.ShoppingBags.GetBag(_userManager.GetUserId(User));
            manageNumberOfProducts(bags);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }
        private void manageNumberOfProducts(List<ShoppingBag> bags)
        {
            foreach(var bag in bags)
            {
                bag.Product.NoOfPieces-=bag.count;
            }
        }
    }
}
