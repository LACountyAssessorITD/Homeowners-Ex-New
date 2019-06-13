using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeOwners_Exemption.Models
{
    public partial class ClaimActionRefDropdown
    {
        [Key]
        public int ClaimActionRefID { get; set; }
        public string ClaimActionRef { get; set; }
        public int OrderList { get; set; }
    }
}
