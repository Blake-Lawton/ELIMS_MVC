using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.ViewModels
{
    public class DashboardViewModel
    {
        //public IEnumerable<Request> RequestVM { get; set; }
        //public IEnumerable<ContactForm> ContactFormVM { get; set; }

        // Request info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NAUEmail { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }

        // Contact form info
        public string CFirstName { get; set; }
        public string CLastName { get; set; }
        public string CEmail { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
    }
}
