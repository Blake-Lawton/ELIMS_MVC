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

namespace ELIMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            //var autoEmail = new MimeMessage();
            //autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));
            //autoEmail.To.Add(new MailboxAddress("bsb232@nau.edu"));
            //autoEmail.Subject = "Auto Email";
            //autoEmail.Body = new TextPart("plain")
            //{
            //    Text = "This is an automated testing email"
            //};

            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("donotreplyelims@gmail.com", "NAULabs123");
            //    client.Send(autoEmail);
            //    client.Disconnect(true);
            //}

            return View();
        }

        //[AllowAnonymous]
        //[Route("login")]
        //public async Task Login(string returnUrl)
        //{
        //    var props = new AuthenticationProperties { RedirectUri = returnUrl };
        //    await HttpContext.ChallengeAsync("CAS", props);

        //}

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

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminOnly()
        {
            ViewData["UserName"] = User.Identity.Name;
            ViewData["Message"] = "Your Dashboard";
            return View();
        }

        [Authorize]
        public IActionResult RequestForm()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
