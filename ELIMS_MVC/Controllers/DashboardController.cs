using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELIMS_MVC.Authorization;
using ELIMS_MVC.Data;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELIMS_MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ELIMS_MVCContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;


        public DashboardController(ELIMS_MVCContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["UserName"] = User.Identity.Name;
            ViewData["Message"] = "Your Dashboard";

            var requests = from r in _context.Request select r;
            requests = requests.Where(r => (r.Status).ToString() == "Pending");
            //var contacts = from c in _context.ContactForm select c;

            var isAuthorized = User.IsInRole(Constants.ELIMSManagersRole) || User.IsInRole(Constants.ELIMSAdministratorsRole);

            var currentUserId = _userManager.GetUserId(User);

            // Requests or contact form submissions are only shown if user is the owner or an admin/manager
            if (!isAuthorized)
            {
                requests = requests.Where(r => r.OwnerID == currentUserId);
                //contacts = contacts.Where(c => c.OwnerID == currentUserId);
            }

            return View(await requests.ToListAsync());

            //return View();
        }
    }
}