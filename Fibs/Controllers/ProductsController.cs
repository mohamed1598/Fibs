using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fibs.Data;
using Fibs.Models;
using Fibs.DataAccess.Repository;
using Fibs.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Fibs.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        public ProductsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAll();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _unitOfWork.Products.GetFirstOrDefault(m => m.Id == id);
            bool isFavorite = await _unitOfWork.FavoriteProducts.IsFavorite(_userManager.GetUserId(User),id);
            ViewData["isFavorite"] = isFavorite;
            if (product == null)
                return NotFound();
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Size,NoOfPieces,Material,Details,FavoritesNo")] Product product)
        {
            await _unitOfWork.Products.Add(product, Request.Form.Files);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var product = await _unitOfWork.Products.GetFirstOrDefault(m => m.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            try
            {
                await _unitOfWork.Products.Update(product, Request.Form.Files);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return View(product);
            }
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var product = await _unitOfWork.Products.GetFirstOrDefault(m=>m.Id==id);
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
