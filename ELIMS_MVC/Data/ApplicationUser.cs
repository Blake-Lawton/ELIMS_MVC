﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ELIMS_MVC.Data
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Status { get; set; }

        public bool LabAccess { get; set; }

        public bool TrainingCompleted { get; set; }

        public DateTime LastLogIn { get; set; }
    }


}
