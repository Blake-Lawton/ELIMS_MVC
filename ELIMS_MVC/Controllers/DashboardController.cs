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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,FirstName,LastName,Email,PhoneNumber,Status, LabAccess")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    return NotFound();
                }

                var edited_user = await _userManager.FindByIdAsync(id);
                edited_user.Id = user.Id;
                edited_user.FirstName = user.FirstName;
                edited_user.LastName = user.LastName;
                edited_user.UserName = user.UserName;
                edited_user.Email = user.Email;
                edited_user.PhoneNumber = user.PhoneNumber;
                edited_user.Status = user.Status;
                edited_user.LabAccess = user.LabAccess;

                if (edited_user.Status == "Lab administrator")
                {
                    await _userManager.AddToRoleAsync(edited_user, "ADMINISTRATORS");

                }
                else if (edited_user.Status == "Student manager" || edited_user.Status =="Lab manager (non-student)")
                {
                    await _userManager.AddToRoleAsync(edited_user, "MANAGERS");

                }
                else if (edited_user.Status == "Student" || edited_user.Status == "Researcher (non-student)")
                {
                    await _userManager.AddToRoleAsync(edited_user, "USERS");

                }

                var result = await _userManager.UpdateAsync(edited_user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
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