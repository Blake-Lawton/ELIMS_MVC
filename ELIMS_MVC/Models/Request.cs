using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

// Model for the lab usage request form, including elements for:
//     NAU UserID
//     Project start & end dates
//     A timestamp for the request
//     User's first and last name
//     NAU UserID
//     NAU email
//     Project name
//     Project objective
//     Contact name
//     Contact NAU UserID
//     Source of funding
//     Sponsor name
//     Sponsor phone number
//     Sponsor email
//     Chemicals to be used
//     Potential meeting times
//     Group members
//     Project file

namespace ELIMS_MVC.Models
{
    public class Request
    {
        public int Id { get; set; }

        // UserID from AspNetUser table
        public string OwnerID { get; set; }

        [Display(Name = "User ID")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Project Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Project End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Request Made")]
        [DataType(DataType.Date)]
        public DateTime RequestMade { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string NAUEmail { get; set; }

        [Display(Name = "Project Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string ProjectName { get; set; }

        [Display(Name = "Project Objective")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string ProjectObjective { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string ContactName { get; set; }

        [Display(Name = "Contact User ID")]
        public int ContactID { get; set; }

        public string Funding { get; set; }

        [Display(Name = "Sponsor Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string SponsorName { get; set; }

        [Display(Name = "Sponsor Phone #")]
        [DataType(DataType.PhoneNumber)]
        public string SponsorPhone { get; set; }

        [Display(Name = "Sponsor Email")]
        [DataType(DataType.EmailAddress)]
        public string SponsorEmail { get; set; }

        public string Chemicals { get; set; }

        [Display(Name = "Meeting Times")]
        public string MeetingTimes { get; set; }

        [Display(Name = "Group Members")]
        public string GroupMembers { get; set; }

        [Display(Name = "Project File")]
        [DataType(DataType.Upload)]
        public string ProjectFile { get; set; }

        [Display(Name = "Status")]
        public RequestStatus Status { get; set; }

    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Denied
    }
}
