using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeOwners_Exemption.Models
{
    public partial class State
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        
    }
}
