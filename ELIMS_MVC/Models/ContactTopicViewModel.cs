using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Model for filtering the contact form (see: ContactForm.cs Model) responses by topic or by a string

namespace ELIMS_MVC.Models
{
    public class ContactTopicViewModel
    {
        public List<ContactForm> ContactForms;
        public SelectList Topics;
        public string EntryTopic { get; set; }
        public string SearchString { get; set; }

    }
}
