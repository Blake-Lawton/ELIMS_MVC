using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.Models
{
    public class RequestTopicViewModel
    {

        public List<Request> Requests;
        public SelectList Status;
        public string EntryStatus { get; set; }
        public string SearchString { get; set; }
    }
}
