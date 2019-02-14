using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELIMS_MVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace ELIMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "NAU's Environmental Engineering Lab";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            ViewData["Message"] = "Environmental Laboratory Informatics and Management System Cookies and Privacy Policies";
            return View();
        }

        //[HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //[Authorize]
        public IActionResult AuthorizedPage()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return View();
        }

        public IActionResult RequestForm()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet("login")]
        public async Task Login(string returnUrl)
        {
            var props = new AuthenticationProperties { RedirectUri = returnUrl };
            await HttpContext.ChallengeAsync("CAS", props);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
