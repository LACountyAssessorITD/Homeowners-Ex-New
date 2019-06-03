using System;
using System.Collections.Generic;

namespace HomeOwners_Exemption.Models
{
    public partial class ClaimantTable
    {
        public string Claimant { get; set; }
        public decimal ClaimantSsn { get; set; }
        public string Spouse { get; set; }
        public decimal? SpouseSsn { get; set; }
        public string MailingStName { get; set; }
        public string MailingApt { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public int? MailingZip { get; set; }
    }
}
