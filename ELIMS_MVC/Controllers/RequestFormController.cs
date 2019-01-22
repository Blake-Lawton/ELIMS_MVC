using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ELIMS_MVC.Controllers
{
    public class RequestFormController : Controller
    {
        
        public IActionResult Index()
        {
            ViewData["Message"] = "Make a new request to use the ENE Lab space for your project";

            return View();
        }

        //public string Index()
        //{
            //return "This will be the request form...";
        //}

        public string Submission(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Thank you for your submission, {name}, NAU ID: {id}.");
        }
    }
}