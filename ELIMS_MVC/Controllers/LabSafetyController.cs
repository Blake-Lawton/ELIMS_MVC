using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;

namespace ELIMS_MVC.Controllers
{
    public class LabSafetyController : Controller
    {
        private readonly ELIMS_MVCContext _context;

        public LabSafetyController(ELIMS_MVCContext context)
        {
            _context = context;
        }

        // GET: BioRafts
        public async Task<IActionResult> Index()
        {
            return View(await _context.LabSafety.ToListAsync());
        }

        // GET: BioRafts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labSafety = await _context.LabSafety
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labSafety == null)
            {
                return NotFound();
            }

            return View(labSafety);
        }

        // GET: BioRafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BioRafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate")] LabSafety labSafety)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labSafety);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labSafety);
        }

        // GET: BioRafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labSafety = await _context.LabSafety.FindAsync(id);
            if (labSafety == null)
            {
                return NotFound();
            }
            return View(labSafety);
        }

        // POST: BioRafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate")] LabSafety labSafety)
        {
            if (id != labSafety.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labSafety);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabSafetyExists(labSafety.Id))
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
            return View(labSafety);
        }

        // GET: BioRafts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labSafety = await _context.LabSafety
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labSafety == null)
            {
                return NotFound();
            }

            return View(labSafety);
        }

        // POST: BioRafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labSafety = await _context.LabSafety.FindAsync(id);
            _context.LabSafety.Remove(labSafety);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabSafetyExists(int id)
        {
            return _context.LabSafety.Any(e => e.Id == id);
        }
    }
}
