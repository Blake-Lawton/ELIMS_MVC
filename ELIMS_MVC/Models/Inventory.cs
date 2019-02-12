using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CASNum { get; set; }
        public string EINECSNum { get; set; }

        public string Classification { get; set; }
        public string HazardPhrases { get; set; }

        public string Tonnage { get; set; }

        // Include place for type, so we could potentially also list materials, appliances, etc. as well as substances and mixtures
        public string EntryType { get; set; }
        
    }
}
