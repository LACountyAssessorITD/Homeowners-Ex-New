using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class ClaimHistory
    {
        [Key]
        public long ClaimID { get; set; }
        public string Claimant { get; set; }
        public long AIN { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public DateTime? DateChanged { get; set; }
        public int? Days { get; set; }
        public string CreatedBy { get; set; }
    }
}
