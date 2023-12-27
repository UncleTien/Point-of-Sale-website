using Point_of_Sale_website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Point_of_Sale_website.Controllers
{
    public class AdminController : Controller
    {

        private readonly MyDbContext _context;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var customers = await Task.Run(() =>
                _context.UserRoles
                    .Where(ur => ur.Role.Name == "Salespeople")
                    .Select(ur => ur.User)
                    .ToList());

            return View(customers);
        }


        public IActionResult ResetPassword(int id)
        {
            var customer = _context.Users.FirstOrDefault(u => u.Id == id);
            if (customer != null)
            {
                return View(customer);
            }

            // Xử lý khi không tìm thấy khách hàng
            return NotFound();
        }

        [HttpPost]
        public IActionResult ResetPassword(User customer)
        {
                // Tìm khách hàng theo Id
                var existingCustomer = _context.Users.FirstOrDefault(u => u.Id == customer.Id);
                if (existingCustomer != null)
                {
                    // Cập nhật mật khẩu mới cho khách hàng
                    existingCustomer.Password = customer.Password;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Chuyển hướng về trang danh sách khách hàng sau khi đặt lại mật khẩu
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý khi không tìm thấy khách hàng
                    return NotFound();
                }

        }

        public IActionResult Details(int id)
        {
            var customer = _context.Users.FirstOrDefault(u => u.Id == id);
            if (customer != null)
            {
                return View(customer);
            }

            // Handle when the customer is not found
            return NotFound();
        }

        [HttpPost]
        public IActionResult ToggleLock(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Toggle the lock status
                user.IsLocked = !user.IsLocked;

                // Save changes to the database
                _context.SaveChanges();

                // Redirect back to the index page
                return RedirectToAction("Index");
            }

            // Handle when the user is not found
            return NotFound();
        }

        public async Task<IActionResult> HQAdministrator()
        {
            var storeOwners = await Task.Run(() =>
                _context.Users
                .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.Role.Name == "HQAdministrator"))
                .ToList());

            return View(storeOwners);
        }

        public IActionResult ProductList()
        {
            // Hiển thị danh sách sách và cho phép thêm, cập nhật, tìm kiếm hoặc xóa sách
            var products = _context.Products.ToList();

            // Lấy danh sách danh mục từ cơ sở dữ liệu
            var categories = _context.Categories.ToList();

            // Truyền danh sách danh mục vào ViewBag
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Hiển thị form thêm sách
            // Lấy danh sách danh mục từ cơ sở dữ liệu
            var categories = _context.Categories.ToList();

            // Truyền danh sách danh mục vào ViewBag
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product products)
        {
            // Thêm sách vào cơ sở dữ liệu
            _context.Products.Add(products);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var products = _context.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }
            // Lấy danh sách danh mục từ cơ sở dữ liệu
            var categories = _context.Categories.ToList();

            // Truyền danh sách danh mục vào ViewBag
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(products);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (id == products.Id)
            {
                _context.Entry(products).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var products = _context.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var products = _context.Products.Find(id);
            _context.Products.Remove(products);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Reports(DateTime? startDate, DateTime? endDate)
        {
            // Nếu không có ngày bắt đầu được cung cấp, mặc định là hôm nay
            startDate ??= DateTime.Today;

            // Nếu không có ngày kết thúc được cung cấp, mặc định là ngày hiện tại
            endDate ??= DateTime.Today;

            // Tính toán ngày kết thúc cho khoảng thời gian "hôm nay"
            if (endDate == DateTime.Today && startDate == DateTime.Today)
            {
                endDate = DateTime.Now; // Nếu xem báo cáo trong ngày hiện tại, chọn ngày hiện tại và thời điểm hiện tại
            }
            else
            {
                endDate = endDate.Value.AddDays(1).AddSeconds(-1); // Chọn ngày cuối cùng của ngày đã chọn
            }

            var orders = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToList();

            var viewModel = new ReportsViewModel
            {
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                Orders = orders
            };

            return View(viewModel);
        }

        public IActionResult Dashboard()
        {
            // Retrieve statistics
            var totalCustomers = _context.UserRoles.Count(ur => ur.Role.Name == "Salespeople");
            var totalProducts = _context.Products.Count();
            var totalOrders = _context.Orders.Count();
            var totalSalesAmount = _context.Orders.Sum(o => o.TotalAmount);

            // Create a DashboardViewModel to pass data to the view
            var dashboardViewModel = new DashboardViewModel
            {
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalSalesAmount = totalSalesAmount
            };

            return View("Dashboard", dashboardViewModel);
        }

    }

}
