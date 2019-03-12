using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.Models
{
    public class Inventory
    {
        // Preliminary information of chemical for inventory
        public int Id { get; set; }

        [Display(Name = "Chemical Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "CAS Num")]
        [Required]
        public string CASNum { get; set; }

        // Who made the chemical / where was it obtained?
        public string Manufacturer { get; set; }

        // Does the chemical have a data sheet that can be attached to it?
        [Display(Name = "Chemical has MSDS")]
        public bool HasMSDS { get; set; }

        // Safety diamond information
        [Display(Name = "Health Hazard Score")]
        public int HealthHazard { get; set; }

        [Display(Name = "Flammable Hazard Score")]
        public int FlammableHazard { get; set; }

        [Display(Name = "Reactive Hazard Score")]
        public int ReactiveHazard { get; set; }

        [Display(Name = "Other Hazard Score")]
        public int OtherHazard { get; set; }

        // How much is there right now?
        [Display(Name = "Current Quantity")]
        [Required]
        public int CurrentQty { get; set; }

        [Required]
        public string Unit { get; set; }

        // Include place for type, so we could potentially also list materials, appliances, etc. as well as substances and mixtures
        [Display(Name = "Date Received In Lab")]
        [DataType(DataType.Date)]
        public DateTime? DateReceived { get; set; }

        [Display(Name = "Checked Out")]
        public bool IsCheckedOut { get; set; }

        [Display(Name = "Date Checked Out")]
        [DataType(DataType.Date)]
        public DateTime? DateCheckedOut { get; set; }

        [Display(Name = "Date Returned To Lab")]
        [DataType(DataType.Date)]
        public DateTime? DateReturned { get; set; }

        // Where is the chemical currently being stored?
        public string Location { get; set; }

        // Disposal stats
        [Display(Name = "Consumed")]
        public bool Consumed { get; set; }

        [Display(Name = "Has Waste")]
        public bool HazWaste { get; set; }

        [Display(Name = "Disposed")]
        public bool Disposed { get; set; }


    }
}
