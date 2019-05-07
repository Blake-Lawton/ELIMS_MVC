using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ELIMS_MVC.Data;
using ELIMS_MVC.Authorization;

namespace ELIMS_MVC.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ELIMS_MVCContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public InventoriesController(ELIMS_MVCContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: Inventories, main page that lists the whole inventory
        public async Task<IActionResult> Index(string search)
        {

            var items = from i in _context.Inventory
                        select i;


            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(s => s.Name.Contains(search));
            }

            return View(await items.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CASNum,Manufacturer,HasMSDS,HealthHazard,FlammableHazard,ReactiveHazard,OtherHazard,CurrentQty,Unit,DateReceived,IsCheckedOut,DateCheckedOut,DateReturned,Location,Consumed,HazWaste,Disposed")] Inventory inventory)
        {
            //var autoEmail = new MimeMessage();
            //autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));
            //autoEmail.To.Add(new MailboxAddress("bsb232@nau.edu"));
            ////Lab instructor emails
            //// autoEmail.To.Add(new MailboxAddress("terry.baxter@nau.edu"));
            //// autoEmail.To.Add(new MailboxAddress("adam.bringhurst@nau.edu"));
            //autoEmail.Subject = "ELIMS Inventory Notification";
            //autoEmail.Body = new TextPart("plain")
            //{
            //    Text = @"A new inventory entry has been entered on the ELIMS webpage. Please click this link to manage: https://elims.azurewebsites.net/inventories. Do not reply to this email."
            //};

            //using (var client = new SmtpClient())
            //{
            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("donotreplyelims@gmail.com", "NAULabs123");
            //    client.Send(autoEmail);
            //    client.Disconnect(true);
            //}

            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CASNum,Manufacturer,HasMSDS,HealthHazard,FlammableHazard,ReactiveHazard,OtherHazard,CurrentQty,Unit,DateReceived,IsCheckedOut,DateCheckedOut,DateReturned,Location,Consumed,HazWaste,Disposed")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
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
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Check if inventory entry exists
        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }

        // Works w/ search
        [HttpPost]
        public string Index(string search, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + search;
        }
    }
}