using Microsoft.AspNetCore.Mvc;
using ThienAnFuni.Models;
using Microsoft.EntityFrameworkCore;
namespace ThienAnFuni.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly TAF_DbContext _context;

        public AdminProductsController(TAF_DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy danh sách sản phẩm cùng thông tin danh mục từ cơ sở dữ liệu
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
