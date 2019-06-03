using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class ProcessClaim
    {
        public int ClaimID { get; set; }
        public string ClaimStatus { get; set; }
        public string Assignee { get; set; }

        public string ClaimDate { get; set; }
    }
}
