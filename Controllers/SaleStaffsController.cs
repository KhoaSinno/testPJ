using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThienAnFuni.Models;

namespace ThienAnFuni.Controllers
{
    public class SaleStaffsController : Controller
    {
        private readonly TAF_DbContext _context;

        public SaleStaffsController(TAF_DbContext context)
        {
            _context = context;
        }

        // GET: SaleStaffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleStaffs.ToListAsync());
        }

        // GET: SaleStaffs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleStaff = await _context.SaleStaffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleStaff == null)
            {
                return NotFound();
            }

            return View(saleStaff);
        }

        // GET: SaleStaffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SaleStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenId,IssuingDate,IssuingPlace,Id,FullName,PhoneNumber,Address,Gender,DateOfBirth,Password")] SaleStaff saleStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleStaff);
        }

        // GET: SaleStaffs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleStaff = await _context.SaleStaffs.FindAsync(id);
            if (saleStaff == null)
            {
                return NotFound();
            }
            return View(saleStaff);
        }

        // POST: SaleStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CitizenId,IssuingDate,IssuingPlace,Id,FullName,PhoneNumber,Address,Gender,DateOfBirth,Password")] SaleStaff saleStaff)
        {
            if (id != saleStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleStaffExists(saleStaff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saleStaff);
        }

        // GET: SaleStaffs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleStaff = await _context.SaleStaffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleStaff == null)
            {
                return NotFound();
            }

            return View(saleStaff);
        }

        // POST: SaleStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var saleStaff = await _context.SaleStaffs.FindAsync(id);
            if (saleStaff != null)
            {
                _context.SaleStaffs.Remove(saleStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleStaffExists(string id)
        {
            return _context.SaleStaffs.Any(e => e.Id == id);
        }
    }
}
