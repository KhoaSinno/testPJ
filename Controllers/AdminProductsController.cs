using Microsoft.AspNetCore.Mvc;
using ThienAnFuni.Models;

namespace ThienAnFuni.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly TAF_DbContext _context;

        public AdminProductsController(TAF_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
