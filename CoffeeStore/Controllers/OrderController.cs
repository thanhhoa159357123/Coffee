using CoffeeStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            KeepSession();
            var model = new Order();
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return RedirectToAction("Login", "Login");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Phone == phoneNumber);

            if (customer == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Truyền thông tin từ Cart vào model Order
            model.Customer = customer;
            model.OrderDate = DateTime.Now;
            model.Lines = _cart.Lines.ToArray();
            model.TotalAmount = _cart.ComputeTotalValue();

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Xin lỗi, giỏ hàng của bạn đang trống!");
            }

            if (ModelState.IsValid)
            {
                order.CustomerID = _context.Customers.FirstOrDefault(c => c.Phone == HttpContext.Session.GetString("PhoneNumber")).CustomerID;
                order.Lines = _cart.Lines.ToArray();
                order.TotalAmount = _cart.ComputeTotalValue();
                order.OrderDate = DateTime.Now;

                _repository.SaveOrder(order);
                _cart.Clear();

                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }

            return View(order);
        }

        public IActionResult HistoryOrder()
        {
            KeepSession();

            var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return RedirectToAction("Login", "Login");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Phone == phoneNumber);
            if (customer == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var orders = _repository.Orders
                .Include(o => o.Lines)
                .Where(o => o.CustomerID == customer.CustomerID)
                .ToList();

            return View(orders);
        }

    }
}
