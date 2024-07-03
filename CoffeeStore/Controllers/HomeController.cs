using CoffeeStore.Models;
using CoffeeStore.Models.ViewModels; // Import ViewModel
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoffeeStore.Controllers
{
    public class HomeController : KeepLoginController
    {
        private readonly ICoffeeRepository _repository;

        public HomeController(ICoffeeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var products = _repository.Products.ToList(); // Lấy danh sách sản phẩm từ repository

            var viewModel = new ProductsListViewModel
            {
                Products = products // Gán danh sách sản phẩm vào ViewModel
            };

            return View(viewModel); // Trả về View với ViewModel
        }
    }
}
