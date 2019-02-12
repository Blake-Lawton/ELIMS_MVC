using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELIMS_MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Please login to continue";

            return View();
        }

        public IActionResult Register()
        {
            ViewData["Message"] = "Welcome to the Environmental Laboratory Informatics and Management System";

            return View();
        }
        //
        //[AllowAnonymous]
        //[Route("login")]
        //public async Task Login(string returnUrl)
        //{
          //  var props = new AuthenticationProperties
            //{
              //  RedirectUri = returnUrl
            //};
            //await HttpContext.ChallengeAsync("CAS", props);
        
      //  }
    }

}