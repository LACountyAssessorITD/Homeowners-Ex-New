using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class MyClaims
    {
        [Key]
        public long ClaimId { get; set; }
        public string Claimant { get; set; }
        public long? currentAPN { get; set; }
        public DateTime? DateAcquired { get; set; }
        public string CurrentStName { get; set; }
        public string CurrentApt { get; set; }
        public string CurrentCity { get; set; }
        public int? ClaimActionId { get; set; }
        public int? FindingReasonID { get; set; }
        public int? ClaimStatusID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ClaimStatusRef { get; set; }
        public int? Days { get; set; }

    }
}
