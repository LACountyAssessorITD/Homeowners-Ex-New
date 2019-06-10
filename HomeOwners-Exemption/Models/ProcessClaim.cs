using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Resources;

namespace HomeOwners_Exemption.Models
{
    public class ProcessClaim
    {
        public string ClaimStatus { get; set; }
        //public int ClaimID { get; set; }
        //public int AIN { get; set; }
        public string ClaimReceivedDate { get; set; }
        public string Supervisor { get; set; }
        public string Staff { get; set; }
        //public IEnumerable<ClaimInfoClass> ClaimInfo { get; set; }
        public IEnumerable<SelectListItem> Supervisors { get; set; }
        public IEnumerable<SelectListItem> Staffs { get; set; }

        //public class ClaimInfoClass
        //{
        //    public string ClaimStatus1 { get; set; }
        //    public int ClaimID1 { get; set; }
        //    public int AIN1 { get; set; }
        //    public string Assignor1 { get; set; }
        //    public string Assignee1 { get; set; }
        //    public string ClaimReceivedDate1 { get; set; }
        //}
    }
}
