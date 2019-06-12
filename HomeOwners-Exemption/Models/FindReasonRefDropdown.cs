using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class FindReasonRefDropdown
    {
        [Key]
        public int FindingReasonRefID { get; set; }
        public string FindingReasonRef { get; set; }
        public int OrderList { get; set; }
    }
}
