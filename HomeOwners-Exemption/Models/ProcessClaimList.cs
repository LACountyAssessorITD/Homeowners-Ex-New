using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class ProcessClaimList
    {
        public IEnumerable<ProcessClaim>
            ClaimList
        { get; set; }
    }
}
