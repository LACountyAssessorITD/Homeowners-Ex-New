using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class ClaimStatusRefList
    {
        [Key]
        public string ClaimStatusRef { get; set; }
        public int? Counts { get; set; }
        
        public int? Late { get; set; }
        

    }
}
