    using CoffeeStore.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    namespace CoffeeStore.Controllers
    {
        public class LoginController : Controller
        {
            private readonly ICustomerRepository _repository;

            public LoginController(ICustomerRepository repository)
            {
                _repository = repository;
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

            // Đăng xuất
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            // Trang đăng nhập
            public IActionResult Login()
            {
                return View();
            }

            // Xử lý đăng nhập
            [HttpPost]
            public IActionResult Login(string phoneNumber, string? returnUrl)
            {
                if (_repository.CheckPhoneNumber(phoneNumber))
                {
                    // Tạo mã OTP ngẫu nhiên
                    Random rand = new Random();
                    int otpCode = rand.Next(100000, 999999);

                    // Lưu mã OTP và số điện thoại vào session
                    HttpContext.Session.SetInt32("OTPCode", otpCode);
                    HttpContext.Session.SetString("PhoneNumber", phoneNumber);

                    // Lấy thông tin người dùng từ repository
                    var user = _repository.Customers.FirstOrDefault(u => u.Phone == phoneNumber);
                    if (user != null)
                    {
                        // Lưu tên người dùng vào session
                        HttpContext.Session.SetString("CustomerName", $"{user.LastName} {user.FirstName}");
                    }

                    // Lưu returnUrl vào session nếu có giá trị
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        HttpContext.Session.SetString("returnUrl", returnUrl);
                    }

                    return RedirectToAction("OTP");
                }
                else
                {
                    ViewBag.ErrorMessage = "Số điện thoại chưa đăng ký";
                    return View();
                }
            }

            // Xác nhận OTP
            [HttpGet]
            public IActionResult OTP()
            {
                var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
                var otpCode = HttpContext.Session.GetInt32("OTPCode");

                if (phoneNumber != null && otpCode != null)
                {
                    ViewBag.OTPCode = otpCode;
                    KeepSession();
                    return View();
                }
                else
                {
                    TempData["OTPErrorMessage"] = "Không có mã OTP nào được tạo. Vui lòng thử lại.";
                    return RedirectToAction("Login");
                }
            }

            // Xử lý xác nhận OTP
            [HttpPost]
            public IActionResult ConfirmOTP(int otpCode)
            {
                KeepSession();

                // Kiểm tra mã OTP nhập vào
                if (HttpContext.Session.GetInt32("OTPCode") == otpCode)
                {
                    HttpContext.Session.Remove("OTPCode");
                    string returnUrl = HttpContext.Session.GetString("returnUrl");

                    // Lấy thông tin khách hàng từ session
                    var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        var user = _repository.Customers.FirstOrDefault(u => u.Phone == phoneNumber);
                        if (user != null)
                        {
                            ViewBag.CustomerName = $"{user.LastName} {user.FirstName}";
                        }
                    }

                    // Chuyển hướng đến returnUrl nếu có
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Mã OTP không hợp lệ";
                    return View("OTP");
                }
            }

            // Thông tin khách hàng
            public IActionResult InformationCustomers()
            {
                var phoneNumber = HttpContext.Session.GetString("PhoneNumber");
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    var user = _repository.Customers.FirstOrDefault(u => u.Phone == phoneNumber);
                    if (user != null)
                    {
                        ViewBag.Customer = user;
                        ViewBag.CustomerName = $"{user.LastName} {user.FirstName}";
                        ViewBag.NgaySinhFormatted = user.NgaySinh.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Không tìm thấy người dùng.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Số điện thoại không tồn tại.";
                }

                return View();
            }

            // Cập nhật thông tin khách hàng
            [HttpPost]
            public IActionResult UpdateCustomer(Customer updatedCustomer)
            {
                var phoneNumber = HttpContext.Session.GetString("PhoneNumber");

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    var user = _repository.Customers.FirstOrDefault(u => u.Phone == phoneNumber);

                    if (user != null)
                    {
                        user.LastName = updatedCustomer.LastName;
                        user.FirstName = updatedCustomer.FirstName;

                        _repository.UpdateCustomer(user);

                        ViewBag.Customer = user;
                        return RedirectToAction("InformationCustomers");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Không tìm thấy người dùng.";
                        return View("InformationCustomers");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Số điện thoại không tồn tại.";
                    return View("InformationCustomers");
                }
            }
        }
    }
