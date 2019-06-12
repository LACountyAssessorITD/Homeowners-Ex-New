using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class ClaimStatusRefDropdown
    {
        [Key]
        public int ClaimStatusRefID { get; set; }
        public string ClaimStatusRef { get; set; }
        public int? OrderList { get; set; }
    }
}
