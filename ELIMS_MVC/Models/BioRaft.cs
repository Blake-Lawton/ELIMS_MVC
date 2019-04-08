using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ELIMS_MVC.Models
{
    public class BioRaft
    {
        public int Id { get; set; }

        [Display(Name = "Training Module")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Date Completed")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

    }
}
