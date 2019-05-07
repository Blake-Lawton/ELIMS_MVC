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
using MimeKit;
using MailKit.Net.Smtp;

// HomeController, corresponds to Home Views. Nothing important here, just displays the pages, no data manipulation necessary

namespace ELIMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        // Home page
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // About page
        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "NAU's Environmental Engineering Lab";

            return View();
        }

        // Privacy & cookie policy
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            ViewData["Message"] = "Environmental Laboratory Informatics and Management System Cookies and Privacy Policies";
            return View();
        }

        // Our 'Access Denied' page
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // In case of error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
