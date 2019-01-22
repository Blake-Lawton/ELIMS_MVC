using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ELIMS_MVC.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestMade { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NAUEmail { get; set; }
        public string ProjectName { get; set; }
        public string ProjectObjective { get; set; }

        public string ContactName { get; set; }
        public string ContactID { get; set; }

        public string Funding { get; set; }

        public string SponsorName { get; set; }
        public string SponsorPhone { get; set; }
        public string SponsorEmail { get; set; }

        public string Chemicals { get; set; }

        public string MeetingTimes { get; set; }

    }
}
