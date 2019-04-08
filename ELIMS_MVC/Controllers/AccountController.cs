using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELIMS_MVC.Data;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ELIMS_MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ELIMS_MVCContext _context;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, ELIMS_MVCContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

    }
}