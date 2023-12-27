using Point_of_Sale_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Point_of_Sale_website.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
        }

        // Hiển thị trang đăng nhập
        public IActionResult Login()
        {
            return View();
        }

        // Hiển thị trang banner trang chủ
        public IActionResult HomeBanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                ModelState.AddModelError("UserId", "Tên người dùng không hợp lệ.");
                return View();
            }

            var userRole = await _context.UserRoles
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserId == user.Id);

            if (userRole == null)
            {
                ModelState.AddModelError("UserId", "Người dùng không có vai trò.");
                return View();
            }

            if (user.Password != password)
            {
                ModelState.AddModelError("Password", "Mật khẩu không đúng.");
                return View();
            }

            if (user.IsLocked)
            {
                ModelState.AddModelError("UserId", "Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên.");
                return View();
            }

            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetInt32("UserId", user.Id);

            await _context.SaveChangesAsync();

            if (userRole.Role.Name == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (userRole.Role.Name == "Salespeople")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("UserId", "Bạn là tài khoản khách hàng.");
                return View();
            }
        }

        // Hiển thị trang đăng ký
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý yêu cầu đăng ký khi gửi dữ liệu POST
        [HttpPost]
        public IActionResult Register(User user)
        {
            // Kiểm tra xem đã tồn tại người dùng có cùng Email chưa
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại.");
                return View(user);
            }

            // Lưu thông tin người dùng vào cơ sở dữ liệu
            _context.Users.Add(user);
            _context.SaveChanges();

            // Lấy ID của tài khoản mới sau khi nó đã được lưu
            var newUserId = user.Id;

            // Lấy giá trị được chọn cho role (ví dụ: "Salespeople")
            var selectedRole = "Salespeople";

            // Tìm role dựa trên tên
            var role = _context.Roles.FirstOrDefault(r => r.Name == selectedRole);

            if (role != null)
            {
                // Tạo UserRoles và gán role cho người dùng
                var userRole = new UserRole { UserId = newUserId, RoleId = role.Id };
                _context.UserRoles.Add(userRole);
                _context.SaveChanges(); // Lưu thay đổi gán vai trò
            }

            return RedirectToAction("Login");
        }


    }
}
