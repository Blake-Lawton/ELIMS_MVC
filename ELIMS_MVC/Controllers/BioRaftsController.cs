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
    public class BioRaftsController : Controller
    {
        private readonly ELIMS_MVCContext _context;

        public BioRaftsController(ELIMS_MVCContext context)
        {
            _context = context;
        }

        // GET: BioRafts
        public async Task<IActionResult> Index()
        {
            return View(await _context.BioRaft.ToListAsync());
        }

        // GET: BioRafts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bioRaft = await _context.BioRaft
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bioRaft == null)
            {
                return NotFound();
            }

            return View(bioRaft);
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
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate")] BioRaft bioRaft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bioRaft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bioRaft);
        }

        // GET: BioRafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bioRaft = await _context.BioRaft.FindAsync(id);
            if (bioRaft == null)
            {
                return NotFound();
            }
            return View(bioRaft);
        }

        // POST: BioRafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate")] BioRaft bioRaft)
        {
            if (id != bioRaft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bioRaft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BioRaftExists(bioRaft.Id))
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
            return View(bioRaft);
        }

        // GET: BioRafts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bioRaft = await _context.BioRaft
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bioRaft == null)
            {
                return NotFound();
            }

            return View(bioRaft);
        }

        // POST: BioRafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bioRaft = await _context.BioRaft.FindAsync(id);
            _context.BioRaft.Remove(bioRaft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BioRaftExists(int id)
        {
            return _context.BioRaft.Any(e => e.Id == id);
        }
    }
}
