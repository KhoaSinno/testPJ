using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using ThienAnFuni.Models;

[Route("admin/products")]
public class AdminProductsController : Controller
{
    private readonly TAF_DbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    // Constructor duy nhất cho controller
    public AdminProductsController(TAF_DbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
        return View(products);
    }
    //public async Task<IActionResult> Create()
    //{
    //    var caterories = await _context.Categories.ToListAsync();
    //    return View(caterories);
    //}
    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        try
        {
            // Lấy danh sách danh mục và nhà cung cấp từ cơ sở dữ liệu
            var categories = await _context.Categories.ToListAsync();
            var suppliers = await _context.Suppliers.ToListAsync();

            // Kiểm tra danh mục
            if (categories == null || !categories.Any())
            {
                ViewBag.CategoryErrorMessage = "Không có danh mục nào trong cơ sở dữ liệu.";
            }

            // Kiểm tra nhà cung cấp
            if (suppliers == null || !suppliers.Any())
            {
                ViewBag.SupplierErrorMessage = "Không có nhà cung cấp nào trong cơ sở dữ liệu.";
            }

            // Truyền danh sách danh mục và nhà cung cấp
            ViewBag.Categories = categories;
            ViewBag.Suppliers = suppliers;

            // Khởi tạo một đối tượng Product mới
            return View(new Product());
        }
        catch (Exception ex)
        {
            // Ghi log exception và hiển thị lỗi tổng quát
            ViewBag.ErrorMessage = "Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message;
            return View(new Product());
        }
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create(Product model, IFormFile ImageUpload)
    {
        //if (ModelState.IsValid)
        //{
        if (ImageUpload != null && ImageUpload.Length > 0)
        {
            // Đường dẫn thư mục lưu ảnh
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "adminThienAn/image_product");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Tạo tên file duy nhất cho ảnh
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUpload.FileName);

            // Đường dẫn đầy đủ đến file
            string filePath = Path.Combine(uploadDir, fileName);

            // Upload file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUpload.CopyToAsync(fileStream);
            }

            // Lưu tên file vào thuộc tính MainImg
            model.MainImg = fileName;
        }
        else
        {
            // Nếu không có ảnh tải lên, dùng ảnh mặc định
            model.MainImg = "default.png";
        }

        // Thêm sản phẩm vào database
        _context.Products.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
        //}

        return View(model);
    }

}
