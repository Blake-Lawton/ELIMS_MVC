using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;

namespace ELIMS_MVC.Controllers
{
    public class RequestController : Controller
    {
        private readonly ELIMS_MVCContext _context;

        public RequestController(ELIMS_MVCContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index(string search)
        {
            var requests = from r in _context.Request select r;

            if (!String.IsNullOrEmpty(search))
            {
                requests = requests.Where(s => s.LastName.Contains(search));
            }

            return View(await requests.ToListAsync());
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,StartDate,EndDate,RequestMade,FirstName,LastName,NAUEmail,ProjectName,ProjectObjective,ContactName,ContactID,Funding,SponsorName,SponsorPhone,SponsorEmail,Chemicals,MeetingTimes,GroupMembers,ProjectFile,RequestStatus")] Request request)
        {
            var autoEmail = new MimeMessage();
            autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));
            autoEmail.To.Add(new MailboxAddress("btl45@nau.edu"));
            //Lab instructor emails
            // autoEmail.To.Add(new MailboxAddress("terry.baxter@nau.edu"));
            // autoEmail.To.Add(new MailboxAddress("adam.bringhurst@nau.edu"));
            autoEmail.Subject = "ELIMS Lab Request";
            autoEmail.Body = new TextPart("plain")
            {
                Text = "You have received a lab request throught the ELIMS web application. Please click this link to manage: https://elims.azurewebsites.net/request "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("donotreplyelims@gmail.com", "NAULabs123");
                client.Send(autoEmail);
                client.Disconnect(true);
            }
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,StartDate,EndDate,RequestMade,FirstName,LastName,NAUEmail,ProjectName,ProjectObjective,ContactName,ContactID,Funding,SponsorName,SponsorPhone,SponsorEmail,Chemicals,MeetingTimes,GroupMembers,ProjectFile,RequestStatus")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Request.FindAsync(id);
            _context.Request.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }

        [HttpPost]
        public string Index(string search, bool notUsed)
        {
          return "From [HttpPost]Index: filter on " + search;
        }
    }
}
