using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELIMS_MVC.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace ELIMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var autoEmail = new MimeMessage();
            autoEmail.From.Add(new MailboxAddress("donotreplyelims@gmail.com"));
            autoEmail.To.Add(new MailboxAddress("btl45@nau.edu"));
            autoEmail.Subject = "Auto Email";
            autoEmail.Body = new TextPart("plain")
            {
                Text = "This is an automated testing email"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("donotreplyelims@gmail.com", "NAULabs123");
                client.Send(autoEmail);
                client.Disconnect(true);
            }


                return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "NAU's Environmental Engineering Lab";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Please reach out to Lab Management with questions or concerns";

            return View();
        }

        public IActionResult Privacy()
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
