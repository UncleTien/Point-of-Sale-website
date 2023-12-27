using Point_of_Sale_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Point_of_Sale_website.Controllers
{
    public class SalespeopleController : Controller
    {
        private readonly MyDbContext _context;

        public SalespeopleController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Hiển thị danh sách sách và cho phép thêm, cập nhật, tìm kiếm hoặc xóa sách
            var products = _context.Products.ToList();

            // Lấy danh sách danh mục từ cơ sở dữ liệu
            var categories = _context.Categories.ToList();

            // Truyền danh sách danh mục vào ViewBag
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(products);
        }

    }
}
