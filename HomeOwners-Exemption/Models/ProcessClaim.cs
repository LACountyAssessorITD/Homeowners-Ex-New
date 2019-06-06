using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeOwners_Exemption.Models
{
    public class ProcessClaim
    {
        public int ClaimID { get; set; }
        public int AIN { get; set; }
        public string ClaimDate { get; set; }
        public string Supervisor { get; set; }
        public IEnumerable<SelectListItem> Supervisors { get; set; }
    }
}
