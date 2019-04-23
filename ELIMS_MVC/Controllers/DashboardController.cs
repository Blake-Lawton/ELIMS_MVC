using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELIMS_MVC.Authorization;
using ELIMS_MVC.Data;
using ELIMS_MVC.Models;
using ELIMS_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELIMS_MVC.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ELIMS_MVCContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public ApplicationUser AppUser { get; set; }

        public DashboardController(ELIMS_MVCContext context, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: Users
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["UserName"] = User.Identity.Name;

            var users = from u in _context.Users select u;

            var isAuthorized = User.IsInRole("ADMINISTRATORS");

            if (!isAuthorized)
            {
                return View("./AccessDenied");
            }

            return View(await users.ToListAsync());
        }

        // GET: Dashboard/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole("ADMINISTRATORS") || User.IsInRole("MANAGERS");

            if(!isAuthorized)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Delete
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            //var AppUsers = _userManager.Users.ToList();
            var user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }

            return RedirectToPage("Index");
        }

        // GET: Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Edit
        public async Task<IActionResult> OnPostEdit(string id, [Bind("Id,UserName,FirstName,LastName,Email,PhoneNumber,Status")] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    return NotFound();
                }
                else if (user != null)
                {
                    
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole("ADMINISTRATORS") || User.IsInRole("MANAGERS");

            if (!isAuthorized)
            {
                return NotFound();
            }

            return View(user);
        }


        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }

}