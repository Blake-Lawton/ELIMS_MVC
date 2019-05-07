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
using Microsoft.AspNetCore.Identity;
using ELIMS_MVC.Data;
using ELIMS_MVC.Authorization;

namespace ELIMS_MVC.Controllers
{
    public class ContactFormsController : Controller
    {
        private readonly ELIMS_MVCContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactFormsController(ELIMS_MVCContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: ContactForms
        [Authorize]
        public async Task<IActionResult> Index(string entryTopic, string search)
        {

            // Use LINQ to get list of topics
            IQueryable<string> topicQuery = from i in _context.ContactForm orderby i.Topic select i.Topic;

            var items = from i in _context.ContactForm select i;

            var isAuthorized = User.IsInRole("MANAGERS") || User.IsInRole("ADMINISTRATORS");

            var currentUserId = _userManager.GetUserId(User);

            // If the user isn't a manager or admin, check if they own the submission
            if (!isAuthorized)
            {
                items = items.Where(c => c.OwnerID == currentUserId);
            }

            if (!string.IsNullOrEmpty(entryTopic))
            {
                items = items.Where(s => s.Topic == entryTopic);
            }

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(s => s.Message.Contains(search));
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
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.ContactForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole("MANAGERS") || User.IsInRole("ADMINISTRATORS");

            var currentUserId = _userManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != items.OwnerID)
            {
                return new ChallengeResult();
            }

            return View(items);
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
        public async Task<IActionResult> Create([Bind("Id,CFirstName,CLastName,UserId,CEmail,Topic,Message,ContactDate")] ContactForm contactForm)
        {
            //var autoEmail = new MimeMessage();
            //autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));
            //autoEmail.To.Add(new MailboxAddress("bsb232@nau.edu"));
            ////Lab instructor emails
            //// autoEmail.To.Add(new MailboxAddress("terry.baxter@nau.edu"));
            //// autoEmail.To.Add(new MailboxAddress("adam.bringhurst@nau.edu"));
            //autoEmail.Subject = "ELIMS Contact Notification";
            //autoEmail.Body = new TextPart("plain")
            //{
            //    Text = @"You have recieved a contact information form from the ELIMS webpage. Please click this link to manage: https://elims.azurewebsites.net/ContactForms. Do not reply to this email."
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
                contactForm.OwnerID = _userManager.GetUserId(User);

                var isAuthorized = await _authorizationService.AuthorizeAsync(User, contactForm, ELIMSOperations.Create);

                // If not authorized
                if (!isAuthorized.Succeeded)
                {
                    _context.Add(contactForm);
                    await _context.SaveChangesAsync();
                    return View("./Views/Home/Index.cshtml");
                }

                _context.Add(contactForm);
                await _context.SaveChangesAsync();
                return View("./Views/Home/Index.cshtml");
            }
            return View("/Home/Index");
        }

        // GET: ContactForms/Edit/5
        // MUST BE ADMIN ONLY
        [Authorize]
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

            var isAuthorized = User.IsInRole("MANAGERS") || User.IsInRole("ADMINISTRATORS");

            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CFirstName,CLastName,UserId,CEmail,Topic,Message,ContactDate")] ContactForm contactForm)
        {
            if (id != contactForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string date = contactForm.ContactDate;
                    
                    _context.Update(contactForm);
                    contactForm.ContactDate = date;
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
        [Authorize]
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

            var isAuthorized = User.IsInRole("MANAGERS") || User.IsInRole("ADMINISTRATORS");

            if(!isAuthorized)
            {
                return new ChallengeResult();
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
