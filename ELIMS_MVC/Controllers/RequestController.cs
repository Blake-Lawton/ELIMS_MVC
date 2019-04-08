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
using ELIMS_MVC.Authorization;
using ELIMS_MVC.Data;

namespace ELIMS_MVC.Controllers
{
    public class RequestController : Controller
    {
        private readonly ELIMS_MVCContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;


        public RequestController(ELIMS_MVCContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: Requests
        [Authorize]
        public async Task<IActionResult> Index(string search, string requestStatus)
        {
            // Use LINQ to get list of topics
            var list = new List<string>() { "Approved", "Denied", "Pending"};
            var requestQuery = list.AsQueryable();


            var requests = from r in _context.Request select r;

            var isAuthorized = User.IsInRole(Constants.ELIMSManagersRole) || User.IsInRole(Constants.ELIMSAdministratorsRole);

            var currentUserId = _userManager.GetUserId(User);

            // Requests are only shown if user is the owner or an admin/manager
            if (!isAuthorized)
            {
                requests = requests.Where(r => r.OwnerID == currentUserId);
            }

            //if (!string.IsNullOrEmpty(requestStatus))
            //{
            //    requests = requests.Where(s => s.Status == requestStatus);
            //}

            // Request = await requests.ToListAsync();

            if (!String.IsNullOrEmpty(search))
            {
                requests = requests.Where(s => s.LastName.Contains(search));
            }

            //var requestStatusVM = new RequestTopicViewModel
            //{
            //    Status = new SelectList(await requestQuery.Distinct().ToListAsync()),
            //    Requests = await requests.ToListAsync()
            //};

            return View(await requests.ToListAsync());
        }

        // GET: Requests/Details/5
        [Authorize]
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

            var isAuthorized = User.IsInRole(Constants.ELIMSManagersRole) || User.IsInRole(Constants.ELIMSAdministratorsRole);

            var currentUserId = _userManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != request.OwnerID
                && request.Status != RequestStatus.Approved)
            {
                return new ChallengeResult();
            }

            return View(request);
        }

        // POST: Requests/Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("Id,UserId,StartDate,EndDate,RequestMade,FirstName,LastName,NAUEmail,ProjectName,ProjectObjective,ContactName,ContactID,Funding,SponsorName,SponsorPhone,SponsorEmail,Chemicals,MeetingTimes,GroupMembers,ProjectFile,Status,OwnerID")] Request request)
        {
            var status = request.Status;

            var r = await _context.Request.FindAsync(id);


            if (r == null)
            {
                return NotFound();
            }

            if (id != request.Id)
            {
                return NotFound();
            }

            var requestOperation = (status == RequestStatus.Approved) ? ELIMSOperations.Approve : ELIMSOperations.Deny;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, r, requestOperation);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            r.Status = status;
            _context.Request.Update(r);
            await _context.SaveChangesAsync();

            //var autoEmail = new MimeMessage();
            //autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));

            //// Email the user who made the request that their request has been updated
            //autoEmail.To.Add(new MailboxAddress(request.NAUEmail));
            //autoEmail.Subject = "ELIMS Lab Request Notification";
            //autoEmail.Body = new TextPart("plain")
            //{
            //    Text = @"There has been an update to your lab usage request. Please click this link to view it: https://elims.azurewebsites.net/request. Do not reply to this email. Thank you!"
            //};

            //using (var client = new SmtpClient())
            //{
            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;


            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("donotreplyelims@gmail.com", "NAULabs123");
            //    client.Send(autoEmail);
            //    client.Disconnect(true);
            //}

            return RedirectToAction(nameof(Index));

        }

        // GET: Requests/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,StartDate,EndDate,RequestMade,FirstName,LastName,NAUEmail,ProjectName,ProjectObjective,ContactName,ContactID,Funding,SponsorName,SponsorPhone,SponsorEmail,Chemicals,MeetingTimes,GroupMembers,ProjectFile,Status,OwnerID")] Request request)
        {
            var autoEmail = new MimeMessage();
            autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));
            autoEmail.To.Add(new MailboxAddress("bsb232@nau.edu"));
            //Lab instructor emails
            // autoEmail.To.Add(new MailboxAddress("terry.baxter@nau.edu"));
            // autoEmail.To.Add(new MailboxAddress("adam.bringhurst@nau.edu"));
            autoEmail.Subject = "ELIMS Contact Notification";
            autoEmail.Body = new TextPart("plain")
            {
                Text = @"A new request for lab usage has been submitted. Please click this link to manage: https://elims.azurewebsites.net/request. Do not reply to this email. Thank you!"
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;


                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("donotreplyelims@gmail.com", "NAULabs123");
                client.Send(autoEmail);
                client.Disconnect(true);
            }

            if (ModelState.IsValid)
            {
                request.OwnerID = _userManager.GetUserId(User);

                var isAuthorized = await _authorizationService.AuthorizeAsync(User, request, ELIMSOperations.Create);

                // If not authorized
                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Requests/Edit/5
        [Authorize]
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
            var isAuthorized = User.IsInRole(Constants.ELIMSManagersRole) || User.IsInRole(Constants.ELIMSAdministratorsRole);

            var currentUserId = _userManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != request.OwnerID
                && request.Status != RequestStatus.Approved)
            {
                return new ChallengeResult();
            }

            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,StartDate,EndDate,RequestMade,FirstName,LastName,NAUEmail,ProjectName,ProjectObjective,ContactName,ContactID,Funding,SponsorName,SponsorPhone,SponsorEmail,Chemicals,MeetingTimes,GroupMembers,ProjectFile,Status,OwnerID")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //request.OwnerID = request.OwnerID;
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
        [Authorize]
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

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, request, ELIMSOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
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
