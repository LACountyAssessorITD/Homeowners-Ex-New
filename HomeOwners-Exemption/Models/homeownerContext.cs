using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeOwners_Exemption.Models
{
    public partial class homeownerContext : DbContext
    {
        public homeownerContext()
        {
        }

        public homeownerContext(DbContextOptions<homeownerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserStaff> UserStaff { get; set; }
        public virtual DbSet<ClaimHistory> History{ get; set; }
        public virtual DbSet<ClaimActionRefDropdown> ClaimAction { get; set; }
        public virtual DbSet<FindReasonRefDropdown> FindingReason { get; set; }
        public virtual DbSet<ClaimStatusRefDropdown> ClaimStatus { get; set; }
        public virtual DbSet<ClaimStatusRefList> statusList { get; set; }
        public virtual DbSet<MyClaims> MyClaims { get; set; }
        //public virtual DbSet<User> user { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Supervisors> Supervisors { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<UserInformation> user { get; set; }
       
        public virtual DbSet<ClaimTable> ClaimTable { get; set; }
        public virtual DbSet<ClaimantTable> ClaimantTable { get; set; }
        public virtual DbSet<ClaimsList> ClaimsList { get; set; }
        public virtual DbSet<PropertyTable> PropertyTable { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<RoleStatus> RoleStatus { get; set; }
        public virtual DbSet<ClaimInfo> ClaimInfo { get; set; }


        // Unable to generate entity type for table 'dbo.harvest_table'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.temp_table'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PivotData'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LegalDescription'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.usersRole'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.hoxusers'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.FindingReasonRef'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ClaimStatusRef'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ClaimStatus'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ClaimStatusRefRole'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tmpClaimID'. Please see the warning messages.



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

           

         

            modelBuilder.Entity<ClaimTable>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .HasName("PK__claim_ta__01BDF9B33BDE967C");

                entity.ToTable("claim_table");

                entity.Property(e => e.ClaimId)
                    .HasColumnName("claimID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CaseClosed)
                    .HasColumnName("caseClosed")
                    .HasColumnType("date");

                entity.Property(e => e.CaseClosedAssignee)
                    .HasColumnName("caseClosedAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CaseClosedAssignor)
                    .HasColumnName("caseClosedAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimAction)
                    .HasColumnName("claimAction")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimReceived)
                    .HasColumnName("claimReceived")
                    .HasColumnType("date");

                entity.Property(e => e.ClaimReceivedAssignee)
                    .HasColumnName("claimReceivedAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimReceivedAssignor)
                    .HasColumnName("claimReceivedAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Claimant)
                    .HasColumnName("claimant")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimantSsn)
                    .HasColumnName("claimantSSN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrStatus)
                    .HasColumnName("currStatus")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentApn).HasColumnName("currentAPN");

                entity.Property(e => e.CurrentApt)
                    .HasColumnName("currentApt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentCity)
                    .HasColumnName("currentCity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentStName)
                    .HasColumnName("currentStName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentState)
                    .HasColumnName("currentState")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentZip).HasColumnName("currentZip");

                entity.Property(e => e.DateAcquired)
                    .HasColumnName("dateAcquired")
                    .HasColumnType("date");

                entity.Property(e => e.DateMovedOut)
                    .HasColumnName("dateMovedOut")
                    .HasColumnType("date");

                entity.Property(e => e.DateOccupied)
                    .HasColumnName("dateOccupied")
                    .HasColumnType("date");

                entity.Property(e => e.ExemptRe).HasColumnName("exemptRE");

                entity.Property(e => e.ExemptRe2).HasColumnName("exemptRE2");

                entity.Property(e => e.FindingReason)
                    .HasColumnName("findingReason")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hold)
                    .HasColumnName("hold")
                    .HasColumnType("date");

                entity.Property(e => e.HoldAssignee)
                    .HasColumnName("holdAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoldAssignor)
                    .HasColumnName("holdAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingApt)
                    .HasColumnName("mailingApt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingCity)
                    .HasColumnName("mailingCity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingStName)
                    .HasColumnName("mailingStName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingState)
                    .HasColumnName("mailingState")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingZip).HasColumnName("mailingZip");

                entity.Property(e => e.PreprintSent)
                    .HasColumnName("preprintSent")
                    .HasColumnType("date");

                entity.Property(e => e.PreprintSentAssignee)
                    .HasColumnName("preprintSentAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreprintSentAssignor)
                    .HasColumnName("preprintSentAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriorApn).HasColumnName("priorAPN");

                entity.Property(e => e.PriorApt)
                    .HasColumnName("priorApt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriorCity)
                    .HasColumnName("priorCity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriorStName)
                    .HasColumnName("priorStName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriorState)
                    .HasColumnName("priorState")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriorZip).HasColumnName("priorZip");

                entity.Property(e => e.RollTaxYear).HasColumnName("rollTaxYear");

                entity.Property(e => e.Spouse)
                    .HasColumnName("spouse")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpouseSsn)
                    .HasColumnName("spouseSSN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffReview)
                    .HasColumnName("staffReview")
                    .HasColumnType("date");

                entity.Property(e => e.StaffReviewAssignee)
                    .HasColumnName("staffReviewAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffReviewAssignor)
                    .HasColumnName("staffReviewAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffReviewDate)
                    .HasColumnName("staffReviewDate")
                    .HasColumnType("date");

                entity.Property(e => e.StaffReviewDateAssignee)
                    .HasColumnName("staffReviewDateAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffReviewDateAssignor)
                    .HasColumnName("staffReviewDateAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupervisorReview)
                    .HasColumnName("supervisorReview")
                    .HasColumnType("date");

                entity.Property(e => e.SupervisorReviewAssignee)
                    .HasColumnName("supervisorReviewAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupervisorReviewAssignor)
                    .HasColumnName("supervisorReviewAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupervisorWorkload)
                    .HasColumnName("supervisorWorkload")
                    .HasColumnType("date");

                entity.Property(e => e.SupervisorWorkloadAssignee)
                    .HasColumnName("supervisorWorkloadAssignee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupervisorWorkloadAssignor)
                    .HasColumnName("supervisorWorkloadAssignor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuppTaxYear).HasColumnName("suppTaxYear");
            });

            modelBuilder.Entity<ClaimantTable>(entity =>
            {
                entity.HasKey(e => e.ClaimantSsn)
                    .HasName("PK__claimant__97DABD326715F007");

                entity.ToTable("claimant_table");

                entity.Property(e => e.ClaimantSsn)
                    .HasColumnName("claimantSSN")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Claimant)
                    .HasColumnName("claimant")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingApt)
                    .HasColumnName("mailingApt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingCity)
                    .HasColumnName("mailingCity")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingStName)
                    .HasColumnName("mailingStName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingState)
                    .HasColumnName("mailingState")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailingZip).HasColumnName("mailingZip");

                entity.Property(e => e.Spouse)
                    .HasColumnName("spouse")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpouseSsn)
                    .HasColumnName("spouseSSN")
                    .HasColumnType("numeric(9, 0)");
            });

            modelBuilder.Entity<ClaimsList>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .HasName("PK__claims_l__01BDF9B3CF7B9B71");

                entity.ToTable("claims_list");

                entity.Property(e => e.ClaimId)
                    .HasColumnName("claimID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ain).HasColumnName("AIN");

                entity.Property(e => e.ClaimantSsn)
                    .HasColumnName("claimantSSN")
                    .HasColumnType("numeric(9, 0)");
            });

            modelBuilder.Entity<PropertyTable>(entity =>
            {
                entity.ToTable("property_table");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ain).HasColumnName("AIN");

                entity.Property(e => e.Apt)
                    .HasColumnName("apt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateAcquired)
                    .HasColumnName("dateAcquired")
                    .HasColumnType("date");

                entity.Property(e => e.DateMovedOut)
                    .HasColumnName("dateMovedOut")
                    .HasColumnType("date");

                entity.Property(e => e.DateOccupied)
                    .HasColumnName("dateOccupied")
                    .HasColumnType("date");

                entity.Property(e => e.OwnerName)
                    .HasColumnName("ownerName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerSsn)
                    .HasColumnName("ownerSSN")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasColumnName("streetName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
