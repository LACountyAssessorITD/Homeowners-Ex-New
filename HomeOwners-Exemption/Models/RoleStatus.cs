using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class RoleStatus
    {
        [Key]
        public int RoleID { get; set; }
        public int ClaimStatusRefID { get; set; }
        public string ClaimStatusRef { get; set; }
        public int OrderList { get; set; }
    }
}
