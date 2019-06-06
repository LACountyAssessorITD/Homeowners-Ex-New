using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeOwners_Exemption.Models
{
    public class ProcessClaim
    {
        [Remote("IsClaimIDExist", "Home", ErrorMessage = "Claim ID Already Exist. Please enter another Claim ID.")]
        public int ClaimID { get; set; }
        public string ClaimStatus { get; set; }
        public string Assignee { get; set; }
        public string ClaimDate { get; set; }
    }
}
