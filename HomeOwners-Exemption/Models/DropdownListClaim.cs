using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class DropdownListClaim
    {
        public List<SelectListItem> Status { get; set; }
        public List<SelectListItem> Action { get; set; }
        public List<SelectListItem> Reason { get; set; }
        public List<SelectListItem> states { get; set; }
        public List<SelectListItem> Assignee { get; set; }

        public DropdownListClaim()
        {
            Status = new List<SelectListItem>();
            Action = new List<SelectListItem>();
            Reason = new List<SelectListItem>();
            states = new List<SelectListItem>();
            Assignee = new List<SelectListItem>();
        }

    }
}
