using Microsoft.AspNetCore.Mvc;
using Point_of_Sale_website.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Point_of_Sale_website.Controllers;

namespace Point_of_Sale_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Hiển thị trang chính
        public IActionResult Index()
        {
            var products = _context.Products.Include(b => b.Category).ToList();
            return View(products);
        }

        public IActionResult Search(string keyword)
        {
            var products = _context.Products
                .Include(b => b.Category)
                .Where(p => p.Title.Contains(keyword) || p.Author.Contains(keyword))
                .ToList();

            return View("Index", products);
        }

        // Hiển thị trang quyền riêng tư
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ProductDetail(int id)
        {
            var products = _context.Products.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            if (products != null)
            {
                return View(products);
            }
            return NotFound();
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

                var existingItem = cart.FirstOrDefault(item => item.ProductId == product.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Title,
                        Price = product.Price,
                        Quantity = 1
                    });
                }

                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index");
        }



        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

            if (cart != null)
            {
                var itemToRemove = cart.FirstOrDefault(item => item.ProductId == id);

                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.SetObject("Cart", cart);
                }
            }

            return RedirectToAction("Checkout");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

            // Lấy danh sách khách hàng
            var customers =
             _context.UserRoles
                    .Where(ur => ur.Role.Name == "Customers")
                    .Select(ur => ur.User)
                    .ToList();
            // Đặt danh sách khách hàng vào ViewBag để sử dụng trong dropdownlist
            ViewBag.Customers = customers;

            return View(cart);
        }

        public IActionResult ProcessCheckout(int customerId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

            if (cart != null && cart.Count > 0)
            {
                // Tính tổng tiền từ giỏ hàng
                decimal totalAmount = 0;

                foreach (var item in cart)
                {
                    totalAmount += item.Price * item.Quantity;
                }

                // Tạo đơn đặt hàng từ giỏ hàng và thông tin khách hàng
                var order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    ShippingAddress = "Default"
                    // Thêm các thông tin khác như tổng tiền, địa chỉ giao hàng, v.v.
                };

                // Lưu đơn đặt hàng vào cơ sở dữ liệu
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Lưu chi tiết đơn hàng (sản phẩm trong đơn hàng)
                foreach (var item in cart)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);

                    if (product != null)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id, // Id của đơn đặt hàng vừa tạo
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Product = product
                        };
                        _context.OrderDetails.Add(orderDetail);
                    }
                    else
                    {
                        // Xử lý khi sản phẩm không tồn tại
                    }
                }

                _context.SaveChanges();


                // Xóa giỏ hàng sau khi đã đặt hàng thành công
                HttpContext.Session.SetObject("Cart", new List<CartItem>());

                return RedirectToAction("Index");
            }

            return View("CheckoutError");
        }

        public IActionResult Profile()
        {
            var customerId = HttpContext.Session.GetInt32("UserId");
            var customer = _context.Users.FirstOrDefault(u => u.Id == customerId);
            if (customer != null)
            {
                return View(customer);
            }

            // Handle when the customer is not found
            return NotFound();
        }

        // GET: Hiển thị trang cập nhật thông tin cá nhân
        public IActionResult UpdateProfile()
        {
            var customerId = HttpContext.Session.GetInt32("UserId");
            var customer = _context.Users.FirstOrDefault(u => u.Id == customerId);

            if (customer != null)
            {
                return View(customer);
            }

            // Handle when the customer is not found
            return NotFound();
        }

        // POST: Xử lý cập nhật thông tin cá nhân
        [HttpPost]
        public IActionResult UpdateProfile(User updatedUser)
        {
                var existingUser = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

                if (existingUser != null)
                {
                    // Cập nhật thông tin cá nhân từ dữ liệu được nhập
                    existingUser.FullName = updatedUser.FullName;
                    existingUser.Email = updatedUser.Email;
                    existingUser.Phone = updatedUser.Phone;
                    existingUser.Gender = updatedUser.Gender;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    return RedirectToAction("Profile");
                }

                // Handle when the customer is not found
                return NotFound();
        }

        public IActionResult Help()
        {
            var helpTopics = new List<string>
            {
                "Cách đặt hàng",
                "Hướng dẫn thanh toán",
                "Sử dụng giỏ hàng",
                "Chính sách hoàn trả",
            };

            return View(helpTopics);
        }


        // Xử lý trang lỗi
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Customers()
        {
            var customers = await Task.Run(() =>
                _context.UserRoles
                    .Where(ur => ur.Role.Name == "Customers")
                    .Select(ur => ur.User)
                    .ToList());

            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                var orders = _context.Orders.Where(o => o.CustomerId == id).ToList();

                var viewModel = new UserDetailsViewModel
                {
                    User = user,
                    Orders = orders
                };

                return View(viewModel);
            }

            // Handle when the user is not found
            return NotFound();
        }

        public IActionResult OrderDetails(int id)
        {
            var order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                var viewModel = new OrderDetailsViewModel
                {
                    Order = order,
                    OrderDetails = order.OrderDetails
                };

                return View(viewModel);
            }

            // Handle when the order is not found
            return NotFound();
        }



    }
}
