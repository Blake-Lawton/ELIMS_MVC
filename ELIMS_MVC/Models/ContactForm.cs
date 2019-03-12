using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

// Model for contact form, with elements for:
//     User's first and last name
//     NAU UserID (ex: 1234567)
//     NAU email (ex: abc123@nau.edu)
//     The topic selected from a dropdown menu
//     The message
//     A timestamp for the message

namespace ELIMS_MVC.Models
{
    public class ContactForm
    {
        public int Id { get; set; }

        // User ID from AspNetUser table
        public string OwnerID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }

        [Display(Name = "NAU UserID")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "NAU Email")]
        [Required]
        public string NAUEmail { get; set; }

        [Required]
        public string Topic { get; set; }
        
        [Required]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date Created")]
        public string ContactDate { get; set; }
    }
}
