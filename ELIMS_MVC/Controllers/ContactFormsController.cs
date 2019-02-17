using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace ELIMS_MVC.Controllers
{
    public class ContactFormsController : Controller
    {
        private readonly ELIMS_MVCContext _context;

        public ContactFormsController(ELIMS_MVCContext context)
        {
            _context = context;
        }

        // GET: ContactForms
        public async Task<IActionResult> Index(string entryTopic, string search)
        {
            // Use LINQ to get list of topics
            IQueryable<string> topicQuery = from i in _context.ContactForm orderby i.Topic select i.Topic;

            var items = from i in _context.ContactForm select i;

            if (!string.IsNullOrEmpty(entryTopic))
            {
                items = items.Where(s => s.Topic == entryTopic);
            }

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(s => s.LastName.Contains(search));
            }

            var entryTopicVM = new ContactTopicViewModel
            {
                Topics = new SelectList(await topicQuery.Distinct().ToListAsync()),
                ContactForms = await items.ToListAsync()
            };

            return View(entryTopicVM);
        }

        // GET: ContactForms/Details/5
        // Can be accessed by Admin, Manager, and specific User who made the contact request
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // GET: ContactForms/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserId,NAUEmail,Topic,Message,ContactDate")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactForm);
        }

        // GET: ContactForms/Edit/5
        // MUST BE ADMIN ONLY
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserId,NAUEmail,Topic,Message,ContactDate")] ContactForm contactForm)
        {
            if (id != contactForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactFormExists(contactForm.Id))
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
            return View(contactForm);
        }

        // GET: ContactForms/Delete/5
        // Must be admin
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactForm = await _context.ContactForm.FindAsync(id);
            _context.ContactForm.Remove(contactForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
            return _context.ContactForm.Any(e => e.Id == id);
        }

        [HttpPost]
        public string Index(string search, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + search;
        }
    }
}
