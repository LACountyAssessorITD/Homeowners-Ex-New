using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class UpdateClaim
    {
        public List<SqlParameter> parameterMap { get; set; }

        public UpdateClaim(Claim udpList)
        {
            parameterMap = new List<SqlParameter>();

            parameterMap.Add( new SqlParameter("@claimID", udpList.claimID));

            parameterMap.Add( new SqlParameter("@ClaimStatusRefID", udpList.ClaimStatusID));
            parameterMap.Add(new SqlParameter("@AssigneeID", udpList.AssigneeID));
            parameterMap.Add( new SqlParameter("@AssignorID", udpList.AssignorID));

            parameterMap.Add(new SqlParameter("@claimant", udpList.Claimant));
            parameterMap.Add(new SqlParameter("@claimantSSN", DBNull.Value));
            parameterMap.Add(new SqlParameter("@spouse", udpList.Spouse == null ? (object)DBNull.Value : udpList.Spouse));
            parameterMap.Add(new SqlParameter("@spouseSSN", DBNull.Value));

            parameterMap.Add(new SqlParameter("@mailingStName", udpList.MailingStName == null ? (object)DBNull.Value : udpList.MailingStName));
            parameterMap.Add(new SqlParameter("@mailingApt", udpList.MailingApt == null ? (object)DBNull.Value : udpList.MailingApt));
            parameterMap.Add(new SqlParameter("@mailingCity", udpList.MailingCity == null ? (object)DBNull.Value : udpList.MailingCity));
            parameterMap.Add(new SqlParameter("@mailingState", udpList.MailingState == null ? (object)DBNull.Value : udpList.MailingState));
            parameterMap.Add(new SqlParameter("@mailingZip", udpList.MailingZip == null ? (object)DBNull.Value : udpList.MailingZip));

            parameterMap.Add(new SqlParameter("@priorAPN", udpList.PriorApn == null ? (object)DBNull.Value : udpList.PriorApn));
            parameterMap.Add(new SqlParameter("@dateMovedOut", udpList.DateMovedOut == null ? (object)DBNull.Value : udpList.DateMovedOut));
            parameterMap.Add(new SqlParameter("@priorStName", udpList.PriorStName == null ? (object)DBNull.Value : udpList.PriorStName));
            parameterMap.Add(new SqlParameter("@priorApt", udpList.PriorApt == null ? (object)DBNull.Value : udpList.PriorApt));
            parameterMap.Add(new SqlParameter("@priorCity", udpList.PriorCity == null ? (object)DBNull.Value : udpList.PriorCity));
            parameterMap.Add(new SqlParameter("@priorState", udpList.PriorState == null ? (object)DBNull.Value : udpList.PriorState));
            parameterMap.Add(new SqlParameter("@priorZip", udpList.PriorZip == null ? (object)DBNull.Value : udpList.PriorZip));

            parameterMap.Add(new SqlParameter("@ClaimActionRefID", udpList.ClaimActionID == null ? (object)DBNull.Value : udpList.ClaimActionID));
            parameterMap.Add(new SqlParameter("@FindingReasonRefID", udpList.FindingReasonID));
            parameterMap.Add(new SqlParameter("@Comments", DBNull.Value));
            parameterMap.Add(new SqlParameter("@Late", DBNull.Value));

            parameterMap.Add(new SqlParameter("@rollTaxYear", udpList.RollTaxYear == null ? (object)DBNull.Value : udpList.RollTaxYear));
            parameterMap.Add(new SqlParameter("@suppTaxYear", udpList.SuppTaxYear == null ? (object)DBNull.Value : udpList.SuppTaxYear));
            parameterMap.Add(new SqlParameter("@exemptRE", udpList.ExemptRe == null ? (object)DBNull.Value : udpList.ExemptRe));
            parameterMap.Add(new SqlParameter("@exemptRE2", udpList.ExemptRe2 == null ? (object)DBNull.Value : udpList.ExemptRe2));
           
            //parameterMap.Add("claimID", new SqlParameter("@claimID", udpList.claimID));

            //parameterMap.Add("ClaimStatusRefID", new SqlParameter("@ClaimStatusRefID", udpList.ClaimStatusRef));
            //parameterMap.Add("AssigneeD", new SqlParameter("@AssigneeID", udpList.AssigneeID));
            //parameterMap.Add("AssignorID", new SqlParameter("@AssignorID", udpList.AssignorID));

            //parameterMap.Add("Claimant	", new SqlParameter("@claimant	", udpList.Claimant));
            //parameterMap.Add("ClaimantSSN", new SqlParameter("@claimantSSN", null));
            //parameterMap.Add("Spouse", new SqlParameter("@spouse", udpList.Spouse));
            //parameterMap.Add("spouseSSN", new SqlParameter("@spouseSSN", null));

            //parameterMap.Add("MailingStName", new SqlParameter("@mailingStName", udpList.MailingStName));
            //parameterMap.Add("MailingApt", new SqlParameter("@mailingApt", udpList.MailingApt));
            //parameterMap.Add("MailingCity", new SqlParameter("@mailingCity", udpList.MailingCity));
            //parameterMap.Add("MailingState", new SqlParameter("@mailingState", udpList.MailingState));
            //parameterMap.Add("MailingZip", new SqlParameter("@mailingZip", udpList.MailingZip));

            //parameterMap.Add("PriorApn", new SqlParameter("@priorAPN", udpList.PriorApn));
            //parameterMap.Add("DateMovedOut", new SqlParameter("@dateMovedOut", udpList.DateMovedOut));
            //parameterMap.Add("PriorStName", new SqlParameter("@priorStName", udpList.PriorStName));
            //parameterMap.Add("PriorApt", new SqlParameter("@priorApt", udpList.PriorApt));
            //parameterMap.Add("PriorCity", new SqlParameter("@priorCity", udpList.PriorCity));
            //parameterMap.Add("PriorState", new SqlParameter("@priorState", udpList.PriorState));
            //parameterMap.Add("PriorZip", new SqlParameter("@priorZip", udpList.PriorZip));

            //parameterMap.Add("ClaimActionID", new SqlParameter("@ClaimActionRefID", udpList.ClaimActionID));
            //parameterMap.Add("FindingReasonID", new SqlParameter("@FindingReasonRefID", udpList.FindingReasonID));
            //parameterMap.Add("Late", new SqlParameter("@Late", null));

        }
    }
}
