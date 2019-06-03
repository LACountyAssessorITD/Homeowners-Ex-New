using System;
using System.Collections.Generic;

namespace HomeOwners_Exemption.Models
{
    public partial class ClaimsList
    {
        public int ClaimId { get; set; }
        public int Ain { get; set; }
        public decimal ClaimantSsn { get; set; }
    }
}
