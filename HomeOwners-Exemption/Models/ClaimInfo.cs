using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeOwners_Exemption.Models
{
    public partial class ClaimInfo
    {
        [Key]
        public string info { get; set; }
    }
}

