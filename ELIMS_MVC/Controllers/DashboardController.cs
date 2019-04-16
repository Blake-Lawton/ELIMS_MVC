using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //// GET: Requests/Details/5
        //[Authorize]
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    // Find the specific user by Id
        //    var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var isAuthorized = User.IsInRole("ADMINISTRATORS");

        //    if (!isAuthorized)
        //    {
        //        return new ChallengeResult();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Details
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Details(int id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,FirstName,LastName,Role,LabAccess,TrainingsCompleted,LastLogin")] ApplicationUser user)
        //{
        //    var u = await _context.Users.FindAsync(id);

        //    if (u == null)
        //    {
        //        return NotFound();
        //    }

        //    var uid = Int32.Parse(user.Id);

        //    if (id != uid)
        //    {
        //        return NotFound();
        //    }

        //    var isAuthorized = User.IsInRole("ADMINISTRATORS");
        //    if (!isAuthorized)
        //    {
        //        return new ChallengeResult();
        //    }

        //    _context.Users.Update(u);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        //// GET: Users/Create
        //[Authorize]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,FirstName,LastName,Role,LabAccess,TrainingsCompleted,LastLogin")] ApplicationUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var isAuthorized = User.IsInRole("ADMINISTRATORS");

        //        if (!isAuthorized)
        //        {
        //            return new ChallengeResult();
        //        }

        //        _context.Add(user);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        //// GET: Users/Edit
        //[Authorize]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var isAuthorized = User.IsInRole("ADMINISTRATORS");

        //    if (!isAuthorized)
        //    {
        //        return new ChallengeResult();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,FirstName,LastName,Role,LabAccess,TrainingsCompleted,LastLogin")] ApplicationUser user)
        //{
        //    var uid = Int32.Parse(user.Id);

        //    if (id != uid)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        ////GET: Users/Delete
        //[Authorize]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }


        //    var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var isAuthorized = User.IsInRole("ADMINISTRATORS");

        //    if (!isAuthorized)
        //    {
        //        return new ChallengeResult();
        //    }

        //    return View(user);
        //}

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }


}