using System;
using System.Collections.Generic;

namespace HomeOwners_Exemption.Models
{
    public partial class ClaimTable
    {
        public long ClaimId { get; set; }
        public string Claimant { get; set; }
        public string ClaimantSsn { get; set; }
        public string Spouse { get; set; }
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
        public string ClaimAction { get; set; }
        public string FindingReason { get; set; }
        public DateTime? ClaimReceived { get; set; }
        public string ClaimReceivedAssignee { get; set; }
        public string ClaimReceivedAssignor { get; set; }
        public DateTime? SupervisorWorkload { get; set; }
        public string SupervisorWorkloadAssignee { get; set; }
        public string SupervisorWorkloadAssignor { get; set; }
        public DateTime? StaffReview { get; set; }
        public string StaffReviewAssignee { get; set; }
        public string StaffReviewAssignor { get; set; }
        public DateTime? StaffReviewDate { get; set; }
        public string StaffReviewDateAssignee { get; set; }
        public string StaffReviewDateAssignor { get; set; }
        public DateTime? SupervisorReview { get; set; }
        public string SupervisorReviewAssignee { get; set; }
        public string SupervisorReviewAssignor { get; set; }
        public DateTime? CaseClosed { get; set; }
        public string CaseClosedAssignee { get; set; }
        public string CaseClosedAssignor { get; set; }
        public string CurrStatus { get; set; }
        public DateTime? PreprintSent { get; set; }
        public string PreprintSentAssignee { get; set; }
        public string PreprintSentAssignor { get; set; }
        public DateTime? Hold { get; set; }
        public string HoldAssignee { get; set; }
        public string HoldAssignor { get; set; }
        public string Active { get; set; }
    }
}
