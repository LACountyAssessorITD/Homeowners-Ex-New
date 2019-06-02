using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace HomeOwners_Exemption.Models
{
    public class Claim
    {
        [Key]
        public Int64 ClaimID { get; set; }
        public String Claimant { get; set; }
        public String ClaimantSSN { get; set; }
        public String Spouse { get; set; }
        public String SpouseSSN { get; set; }
        public  Int64 currentAPN { get; set; }
        public DateTime dateAcquired { get; set; }
        public DateTime dateOccupied { get; set; }
        public String currentStName { get; set; }
        public String currentApt { get; set; }
        public String currentCity { get; set; }
        public String currentState { get; set; }
        public int currentZip { get; set; }
        public String mailingStName { get; set; }
        public String mailingApt { get; set; }
        public String mailingCity { get; set; }
        public String mailingState { get; set; }
        public int mailingZip { get; set; }
        public Int64 priorAPN { get; set; }
        public String dateMovedOut { get; set; }
        public String priorStName { get; set; }
        public String priorApt { get; set; }
        public String priorCity { get; set; }
        public String priorState { get; set; }
        public int priorZip { get; set; }
        public int rollTaxYear { get; set; }
        public int exemptRE { get; set; }
        public int suppTaxYear { get; set; }
        public int exemptRE2 { get; set; }
        public int ClaimActionRefID { get; set; }
        public int FindingReasonRefID { get; set; }
        public int ClaimStatusRefID { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String active { get; set; }                              
        
    }
}
