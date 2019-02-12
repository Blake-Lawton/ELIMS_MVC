using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.Models
{
    public class UserDB
    {
        public int Id { get; set; }

        // Contact information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Username { get; set; } // will be NAU ID for students and NAU faculty, a username for new users
        // how to password

        // Lab information
        // Role = Admin, Manager, User
        public string Role { get; set; }

        // Are they a Student, Researcher, Faculty, Lab Manager?
        public string Status { get; set; }
        
        // Do they have lab access? Boolean, either Yes they have been cleared for lab access, or No they have not
        public bool LabAccess { get; set; }

        // When is the last time they completed safety trainings?
        public string TrainingCompleted { get; set; }

        // When does their training expire?
        public string TrainingExpires { get; set; }

        // Do they need to renew their training?
        public bool RenewStatus { get; set; }

        // Are they an active user?
        public bool Active { get; set; }

        // The last date they logged in
        public DateTime LastLogIn { get; set; }
    }
}
