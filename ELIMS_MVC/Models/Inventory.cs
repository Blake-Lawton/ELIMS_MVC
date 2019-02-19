using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.Models
{
    public class Inventory
    {
        // Preliminary information of chemical for inventory
        public int Id { get; set; }
        public string Name { get; set; }
        public string CASNum { get; set; }

        // Who made the chemical / where was it obtained?
        public string Manufacturer { get; set; }

        // Does the chemical have a data sheet that can be attached to it?
        public bool HasMSDS { get; set; }

        // Safety diamond information
        public int HealthHazard { get; set; }
        public int FlammableHazard { get; set; }
        public int ReactiveHazard { get; set; }
        public int OtherHazard { get; set; }

        // How much is there right now?
        public int CurrentQty { get; set; }
        public string Unit { get; set; }

        // Include place for type, so we could potentially also list materials, appliances, etc. as well as substances and mixtures
        public Date DateReceived { get; set; }
        public bool IsCheckedOut { get; set; }
        public Date DateCheckedOut { get; set; }
        public Date DateReturned { get; set; }

        // Where is the chemical currently being stored?
        public string Location { get; set; }

        // Disposal stats
        public bool Consumed { get; set; }
        public bool HazWaste { get; set; }
        public bool Disposed { get; set; }

    }
}
