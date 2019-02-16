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

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("login")]
        public async Task Login(string returnUrl)
        {
            var props = new AuthenticationProperties { RedirectUri = returnUrl };
            await HttpContext.ChallengeAsync("CAS", props);

        }

        [Authorize]
        public IActionResult AuthorizedPage()
        {
            ViewData["UserName"] = User.Identity.Name;
            ViewData["Message"] = "Your Dashboard";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "NAU's Environmental Engineering Lab";

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "Environmental Laboratory Informatics and Management System Cookies and Privacy Policies";
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult AdminOnly()
        {
            return View();
        }

        public IActionResult RequestForm()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
