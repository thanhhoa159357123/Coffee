using CoffeeStore.Models;
using CoffeeStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using CoffeeStore.Migrations;

namespace CoffeeStore.Controllers
{
    public class OrderController : KeepLoginController
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;
        private readonly CoffeeDbContext _context;

        public OrderController(IOrderRepository repository, Cart cart, CoffeeDbContext context)
        {
            _repository = repository;
            _cart = cart;
            _context = context;
        }

        // Kiểm tra và duy trì session
        private void KeepSession()
        {
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (phoneNumber != null)
            {
                HttpContext.Session.SetString("PhoneNumber", phoneNumber);
            }
            var otpCode = HttpContext.Session.GetInt32("OTPCode");
            if (otpCode.HasValue)
            {
                HttpContext.Session.SetInt32("OTPCode", otpCode.Value);
            }
        }

        public IActionResult Checkout()
        {
            // Duy trì session của người dùng đã đăng nhập
            KeepSession();

            // Lấy thông tin khách hàng từ session hoặc cơ sở dữ liệu
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber))
            {
                // Nếu không có số điện thoại trong session, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Login");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Phone == phoneNumber);

            if (customer == null)
            {
                // Xử lý khi không tìm thấy khách hàng
                return RedirectToAction("Error", "Home");
            }

            var model = new OrderViewModel
            {
                CustomerID = customer.CustomerID,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CustomerPhone = customer.Phone
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerID = model.CustomerID,
                    Address = model.Address,
                    Lines = _cart.Lines.ToArray()
                };

                _repository.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View(model);
            }
        }

        // Lịch sử đơn hàng
        public IActionResult HistoryOrder()
        {
            // Duy trì session của người dùng đã đăng nhập
            KeepSession();

            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            var customer = _context.Customers.FirstOrDefault(c => c.Phone == phoneNumber);
            // Lấy danh sách đơn hàng của người dùng cùng với chi tiết đơn hàng
            var orders = _repository.Orders
                        .Where(o => o.CustomerID == customer.CustomerID)
                        .Include(o => o.Lines)
                        .ThenInclude(l => l.Product)
                        .ToList();

            ViewBag.Orders = orders;

            return View();
        }
    }
}
