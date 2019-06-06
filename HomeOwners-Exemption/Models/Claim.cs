using System;
using System.Collections.Generic;

namespace HomeOwners_Exemption.Models
{
    public partial class Claim
    {
        public long ClaimId { get; set; }
        public string Claimant { get; set; }
        public string Spouse { get; set; }
        public string ClaimantSsn { get; set; }
     
        public string SpouseSsn { get; set; }
        public long? CurrentApn { get; set; }
        public DateTime? DateAcquired { get; set; }
        public DateTime? DateOccupied { get; set; }
        public string CurrentStName { get; set; }
        public string CurrentApt { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public int? CurrentZip { get; set; }
        public string MailingStName { get; set; }
        public string MailingApt { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public int? MailingZip { get; set; }
        public long? PriorApn { get; set; }
        public DateTime? DateMovedOut { get; set; }
        public string PriorStName { get; set; }
        public string PriorApt { get; set; }
        public string PriorCity { get; set; }
        public string PriorState { get; set; }
        public int? PriorZip { get; set; }
        public int? RollTaxYear { get; set; }
        public int? ExemptRe { get; set; }
        public int? SuppTaxYear { get; set; }
        public int? ExemptRe2 { get; set; }
        public int? ClaimActionRefId { get; set; }
        public int? FindingReasonRefId { get; set; }
        public int? ClaimStatusRefId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Active { get; set; }
    }
}
