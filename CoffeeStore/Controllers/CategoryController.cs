using CoffeeStore.Models;
using CoffeeStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoffeeStore.Controllers
{
    public class CategoryController : KeepLoginController
    {
        private ICoffeeRepository repository;

        public CategoryController(ICoffeeRepository repo)
        {
            repository = repo;
        }

        public IActionResult Category(string category = "")
        {
            var categories = repository.Products.Select(p => p.Category).Distinct().ToList();
            var products = string.IsNullOrEmpty(category) ?
                repository.Products.ToList() :
                repository.Products.Where(p => p.Category == category).ToList();

            var viewModel = new ProductsListViewModel
            {
                Categories = categories,
                Products = products,
                CurrentCategory = category
            };

            ViewBag.Category = category;
            return View(viewModel);
        }

        public IActionResult Cafe()
        {
            var coffeeProducts = repository.Products.Where(p => p.Category == "Cà phê").ToList();
            var viewModel = new ProductsListViewModel
            {
                Categories = repository.Products.Select(p => p.Category).Distinct().ToList(),
                Products = coffeeProducts,
                CurrentCategory = "Cà phê"
            };
            return View(viewModel);
        }

        public IActionResult Tea()
        {
            var teaProducts = repository.Products.Where(p => p.Category == "Trà").ToList();
            var viewModel = new ProductsListViewModel
            {
                Categories = repository.Products.Select(p => p.Category).Distinct().ToList(),
                Products = teaProducts,
                CurrentCategory = "Trà"
            };
            return View(viewModel);
        }

        public IActionResult Crushice()
        {
            var iceProducts = repository.Products.Where(p => p.Category == "Đá xay").ToList();
            var viewModel = new ProductsListViewModel
            {
                Categories = repository.Products.Select(p => p.Category).Distinct().ToList(),
                Products = iceProducts,
                CurrentCategory = "Đá xay"
            };
            return View(viewModel);
        }

        public IActionResult _CafeCategory()
        {
            var coffeeProducts = repository.Products.Where(p => p.Category == "Cà phê").ToList();
            var viewModel = new ProductsListViewModel
            {
                Categories = repository.Products.Select(p => p.Category).Distinct().ToList(),
                Products = coffeeProducts,
                CurrentCategory = "Cà phê"
            };
            return PartialView(viewModel);
        }

        public IActionResult _TeaCategory()
        {
            var teaProducts = repository.Products.Where(p => p.Category == "Trà").ToList();
            var viewModel = new ProductsListViewModel
            {
                Categories = repository.Products.Select(p => p.Category).Distinct().ToList(),
                Products = teaProducts,
                CurrentCategory = "Trà"
            };
            return PartialView(viewModel);
        }

        public IActionResult _CrushiceCategory()
        {
            var iceProducts = repository.Products.Where(p => p.Category == "Đá xay").ToList();
            var viewModel = new ProductsListViewModel
            {
                Categories = repository.Products.Select(p => p.Category).Distinct().ToList(),
                Products = iceProducts,
                CurrentCategory = "Đá xay"
            };
            return PartialView(viewModel);
        }
        public IActionResult ProductDetail(int id)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
