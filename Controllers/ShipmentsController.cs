using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThienAnFuni.Models;

namespace ThienAnFuni.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly TAF_DbContext _context;

        public ShipmentsController(TAF_DbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
