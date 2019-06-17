using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homeowners_Ex_New.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HomeOwners_Exemption.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Claim = HomeOwners_Exemption.Models.Claim;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Data;

namespace HomeOwners_Exemption.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly homeownerContext _context;


        public HomeController(homeownerContext context)
        {

            _context = context;

        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);

            dynamic model = new ExpandoObject();
            
            var staffList = _context.Staffs.FromSql("sp_getStaff").ToListAsync().Result.ToList();
            //var supList = _context.Staffs.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
            //staffList.AddRange(supList);

            Dictionary<string, StatusCount> statusList = new Dictionary<string, StatusCount>();
            statusList.Add("Preprint Sent", new StatusCount(0,0));
            statusList.Add("Claim Received", new StatusCount(0, 0));
            statusList.Add("Supervisor Workload", new StatusCount(0, 0));
            statusList.Add("Staff Review", new StatusCount(0, 0));
            statusList.Add("Supervisor Review", new StatusCount(0, 0));
            statusList.Add("Hold", new StatusCount(0, 0));
            statusList.Add("Closed", new StatusCount(0, 0));

          

            var statusDbList = _context.statusList.FromSql("sp_getCountsByStatus").ToListAsync().Result.ToList();
            string strAssignor = User.FindFirst("Name").Value;
            foreach (var item in statusDbList)
            {
                statusList[item.ClaimStatusRef] = new StatusCount(item.OrderCount ?? default(int), item.Late ?? default(int));
             }
            var EmpID = new SqlParameter("@usersID", User.Identity.Name);
            var claimList = _context.MyClaims.FromSql("sp_getListOfAssignedClaim @usersID", EmpID).ToListAsync().Result.ToList();

            
            

            model.staff = staffList;
            model.claims = claimList;
            
            model.statusId = statusList;
            return View( model);
        }

        public class StatusCount
        {
            public int Count { get; set; }
            public int Late { get; set; }
            public int Current { get; set; }
            public StatusCount( int count, int late)
            {
                Count = count;
                Late = late;
                Current = count - late;
            }
        }

        [HttpPost]
        public RedirectToActionResult Index(int? id)
        {
            return RedirectToAction("Claim", "Home", new { id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Claim(int? id)
        {

            ViewBag.history = new ClaimHistory();
            DropdownListClaim drop = GetDropdown(id);
            var modelUser = new Claim();
            ViewBag.ModelMessage = false;
            if (id != null && id > 1)
            {
                var EmpID = new SqlParameter("@ClaimID", id);

                modelUser = _context.Claim.FromSql("sp_getClaim @ClaimID", EmpID).FirstOrDefaultAsync().Result;
                if (modelUser == null)
                {
                    ViewBag.ModelMessage = true;
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.history = _context.History.FromSql("sp_getClaimHistory @ClaimID", EmpID).ToListAsync().Result.ToList();

               
                
            }
            else
            {
                modelUser = new Claim();
                
            }
            ViewBag.Staffs = GetAllStaffs();
            ViewBag.dropdownInfo = drop;
            
            return View(modelUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Claim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                UpdateClaim updClaim = new UpdateClaim(claim);
                List<SqlParameter> parameter = updClaim.parameterMap;

                var result = _context.Database.ExecuteSqlCommand("sp_updClaim @claimID, @currentAPN, @ClaimStatusRefID, @AssigneeID, @AssignorID, @claimant, @claimantSSN, @spouse, @spouseSSN, @mailingStName, @mailingApt, @mailingCity, @mailingState, @mailingZip, @priorAPN, @dateMovedOut, @priorStName, @priorApt, @priorCity, @priorState, @priorZip, @ClaimActionRefID, @FindingReasonRefID, @Late, @Comments, @rollTaxYear, @suppTaxYear, @exemptRE, @exemptRE2"
                                                                , parameter[0], parameter[1], parameter[2],  parameter[3], new SqlParameter("@AssignorID", User.Identity.Name)
                                                                , parameter[5], parameter[6], parameter[7], parameter[8], parameter[9]
                                                                , parameter[10], parameter[11], parameter[12], parameter[13], parameter[14]
                                                                , parameter[15], parameter[16], parameter[17], parameter[18], parameter[19]
                                                                , parameter[20], parameter[21], parameter[22], parameter[23], parameter[24]
                                                                , parameter[25], parameter[26] ,parameter[27], parameter[28]);
                return RedirectToAction("Claim", "Home", new { claim.claimID });
            }
            return View("Claim", "Home");
        }


        public DropdownListClaim GetDropdown(int? ClaimID)
        {
            DropdownListClaim drop = new DropdownListClaim();

            var temp = _context.State.FromSql("sp_States").ToListAsync().Result.ToList();
            //drop.states = new List<SelectListItem>();
            foreach (var item in temp)
            {
                drop.states.Add(new SelectListItem() { Text = item.Id, Value = item.Id });
            }

            //var tempStatus = _context.ClaimStatus.FromSql("sp_getStatus").ToListAsync().Result.ToList();
            var ID = new SqlParameter("@ClaimID", ClaimID);
            var tempStatus = _context.ClaimStatus.FromSql("sp_getStatus @ClaimID", ID).ToListAsync().Result.ToList();
            var index = 0;
            foreach (var item in tempStatus)
            {
                if (index == 0)
                {
                    drop.Status.Add(new SelectListItem() { Text = item.ClaimStatusRef, Value = item.ClaimStatusRefID.ToString(), Selected = true });
                    drop.Status[0].Selected = true;
                }
                else
                    drop.Status.Add(new SelectListItem() { Text = item.ClaimStatusRef, Value = item.ClaimStatusRefID.ToString(), Selected = false });
                index = index + 1;
            }
            var tempFinding = _context.FindingReason.FromSql("sp_getReasonRef").ToListAsync().Result.ToList();
            
            foreach (var item in tempFinding)
            {
                drop.Reason.Add(new SelectListItem() { Text = item.FindingReasonRef, Value = item.FindingReasonRefID.ToString() });
            }
            var tempAction = _context.ClaimAction.FromSql("sp_getAction").ToListAsync().Result.ToList();
            drop.Action.Add(new SelectListItem() { Text = "Select", Value = "0" });
            foreach (var item in tempAction)
            {
                drop.Action.Add(new SelectListItem() { Text = item.ClaimActionRef, Value = item.ClaimActionRefID.ToString() });
            }

            var tempAssignee = _context.Claim.FromSql("sp_getClaim @ClaimID", ID).ToListAsync().Result.ToList();
            foreach (var item in tempAssignee)
            {
                if (item.ClaimStatusRefID == 2)
                {
                    drop.Assignee.Add(new SelectListItem() { Text = item.Assignee, Value = item.AssigneeID, Selected = true });
                    drop.Assignee[0].Selected = true;
                }
                else if (item.ClaimStatusRefID == 3)
                {
                    List<Supervisors> lSupervisors = new List<Supervisors>();
                    lSupervisors = _context.Supervisors.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
                    foreach (var oneSupervisor in lSupervisors)
                    {
                        if (Convert.ToInt32(oneSupervisor.EmployeeID) == Convert.ToInt32(item.AssigneeID))
                            drop.Assignee.Add(new SelectListItem { Text = oneSupervisor.Users, Value = oneSupervisor.EmployeeID, Selected = true });
                        else
                            drop.Assignee.Add(new SelectListItem { Text = oneSupervisor.Users, Value = oneSupervisor.EmployeeID, Selected = false });
                    }
                }
                else if (item.ClaimStatusRefID == 4)
                {
                    List<UserStaff> lStaffs = new List<UserStaff>();
                    lStaffs = _context.UserStaff.FromSql("sp_getStaffDropdown").ToListAsync().Result.ToList();
                    foreach (var oneStaff in lStaffs)
                    {
                        if (Convert.ToInt32(oneStaff.EmployeeID) == Convert.ToInt32(item.AssigneeID))
                            drop.Assignee.Add(new SelectListItem { Text = oneStaff.Users, Value = oneStaff.EmployeeID, Selected = true });
                        else
                            drop.Assignee.Add(new SelectListItem { Text = oneStaff.Users, Value = oneStaff.EmployeeID, Selected = false });
                    }
                }
                else if (item.ClaimStatusRefID == 5)
                {
                    drop.Assignee.Add(new SelectListItem() { Text = item.Assignee, Value = item.AssigneeID, Selected = true });
                    drop.Assignee[0].Selected = true;
                }
                else if (item.ClaimStatusRefID == 6)
                {
                    drop.Assignee.Add(new SelectListItem() { Text = item.Assignee, Value = item.AssigneeID, Selected = true });
                    drop.Assignee[0].Selected = true;
                }
                else if (item.ClaimStatusRefID == 7)
                {
                    drop.Assignee.Add(new SelectListItem() { Text = item.Assignee, Value = item.AssigneeID, Selected = true });
                    drop.Assignee[0].Selected = true;
                }
            }

            return drop;
            
        }

        public string GetDropdownAssignee(int ClaimID, int CurrentClaimStatusRefID, int SelectedClaimStatusRefID, string AssigneeID, string Assignee)
        {
            // Claim Received - Assignee
            // Supervisor Workload - Supervior List
            // Staff Review - Staff List
            // Supervisor Review - Assignee
            // Hold - Assignee
            // Closed - Assignee

            string DropdownText = "";
            if (CurrentClaimStatusRefID == 2 && SelectedClaimStatusRefID == 2) //Claim Received - Claim Received
                DropdownText = "<option value=" + AssigneeID + ">" + Assignee + "</option>";
            else if (CurrentClaimStatusRefID == 2 && SelectedClaimStatusRefID == 3) //Claim Received - Supervisor Workload
            {
                List<Supervisors> lSupervisors = new List<Supervisors>();
                lSupervisors = _context.Supervisors.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
                foreach (var oneSupervisor in lSupervisors)
                {
                    DropdownText = DropdownText + "<option value=" + oneSupervisor.EmployeeID + ">" + oneSupervisor.Users + "</option>";
                }
            }
            else if (CurrentClaimStatusRefID == 3 && SelectedClaimStatusRefID == 3) //Supervisor Workload - Supervisor Workload
            {
                List<Supervisors> lSupervisors = new List<Supervisors>();
                lSupervisors = _context.Supervisors.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
                foreach (var oneSupervisor in lSupervisors)
                {
                    if (oneSupervisor.EmployeeID == AssigneeID)
                        DropdownText = DropdownText + "<option value=" + oneSupervisor.EmployeeID + "selecte=true>" + oneSupervisor.Users + "</option>";
                    else
                        DropdownText = DropdownText + "<option value=" + oneSupervisor.EmployeeID + "selected=false>" + oneSupervisor.Users + "</option>";
                }
            }
            else if (CurrentClaimStatusRefID == 3 && SelectedClaimStatusRefID == 4) //Supervisor Workload - Staff Review
            {
                List<UserStaff> lStaffs = new List<UserStaff>();
                lStaffs = _context.UserStaff.FromSql("sp_getStaffDropdown").ToListAsync().Result.ToList();
                foreach (var oneStaff in lStaffs)
                {
                    DropdownText = DropdownText + "<option value=" + oneStaff.EmployeeID + ">" + oneStaff.Users + "</option>";
                }
            }
            else if (CurrentClaimStatusRefID == 4 && SelectedClaimStatusRefID == 4) //Staff Review - Staff Review
            {
                List<UserStaff> lStaffs = new List<UserStaff>();
                lStaffs = _context.UserStaff.FromSql("sp_getStaffDropdown").ToListAsync().Result.ToList();
                foreach (var oneStaff in lStaffs)
                {
                    if (oneStaff.EmployeeID == AssigneeID)
                        DropdownText = DropdownText + "<option value=" + oneStaff.EmployeeID + "selecte=true>" + oneStaff.Users + "</option>";
                    else
                        DropdownText = DropdownText + "<option value=" + oneStaff.EmployeeID + "selected=false>" + oneStaff.Users + "</option>";
                }
            }
            else if (CurrentClaimStatusRefID == 4 && SelectedClaimStatusRefID == 5) //Staff Review - Supervisor Review
            {
                List<Supervisors> lSupervisors = new List<Supervisors>();
                lSupervisors = _context.Supervisors.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
                foreach (var oneSupervisor in lSupervisors)
                {
                    DropdownText = DropdownText + "<option value=" + oneSupervisor.EmployeeID + ">" + oneSupervisor.Users + "</option>";
                }
            }
            else if (CurrentClaimStatusRefID == 5 && SelectedClaimStatusRefID == 5) //Supervisor Review - Supervisor Review
                DropdownText = "<option value=" + AssigneeID + ">" + Assignee + "</option>";
            else if (CurrentClaimStatusRefID == 5 && SelectedClaimStatusRefID == 6) //Supervisor Review - Hold
                DropdownText = "<option value=" + AssigneeID + ">" + Assignee + "</option>";
            else if (CurrentClaimStatusRefID == 5 && SelectedClaimStatusRefID == 7) //Supervisor Review - Closed
                DropdownText = "<option value=" + AssigneeID + ">" + Assignee + "</option>";
            else if (CurrentClaimStatusRefID == 6 && SelectedClaimStatusRefID == 6) //Hold - Hold
                DropdownText = "<option value=" + AssigneeID + ">" + Assignee + "</option>";
            else if (CurrentClaimStatusRefID == 6 && SelectedClaimStatusRefID == 7) //Hold - Closed
                DropdownText = "<option value=" + AssigneeID + ">" + Assignee + "</option>";

            return DropdownText;
        }

        public IActionResult ProcessClaim()
        {
            var model = new ProcessClaim();
            model.StatusList = GetAllClaimStatus();
            model.Supervisors = GetAllSupervisors();
            model.Staffs = GetAllStaffs();
            model.ClaimReceivedDate = DateTime.Now.ToString("MM/dd/yyyy");

            return View(model);
        }

        public string ProcessOtherStatus(IEnumerable<int> ClaimIDList, string ClaimStatusID, string AssigneeSupervisor, string AssigneeStaff)
        {
            string strAssignor = User.FindFirst("Name").Value;
            try
            {
                DataTable dt_tmpClaimID = new DataTable();
                dt_tmpClaimID.Columns.Add("ClaimID", typeof(string));
                dt_tmpClaimID.Columns.Add("Assignee", typeof(string));
                dt_tmpClaimID.Columns.Add("Assignor", typeof(string));
                if (ClaimStatusID == "4")
                {
                    dt_tmpClaimID.Columns.Add("ClaimStatusRefID", typeof(Int32));
                    dt_tmpClaimID.Columns.Add("ClaimDate", typeof(DateTime));
                }

                DataRow tmpClaimID;
                foreach (int claimID in ClaimIDList)
                {
                    tmpClaimID = dt_tmpClaimID.NewRow();               
                    if (ClaimStatusID == "3")
                    {
                        tmpClaimID["ClaimID"] = Convert.ToString(claimID);
                        tmpClaimID["Assignee"] = AssigneeSupervisor;
                        tmpClaimID["Assignor"] = strAssignor;
                    }
                    else if (ClaimStatusID == "4")
                    {
                        tmpClaimID["ClaimID"] = Convert.ToString(claimID);
                        tmpClaimID["Assignee"] = AssigneeStaff;
                        tmpClaimID["Assignor"] = strAssignor;
                        tmpClaimID["ClaimStatusRefID"] = Convert.ToInt32(ClaimStatusID);
                        tmpClaimID["ClaimDate"] = DateTime.Today;
                    }                    
                    dt_tmpClaimID.Rows.Add(tmpClaimID);
                }

                var result = "";
                if (ClaimStatusID == "3")
                {
                    var parameter = new SqlParameter("@tvpClaimID", SqlDbType.Structured);
                    parameter.TypeName = "tvpClaimID";
                    parameter.Value = dt_tmpClaimID;
                    result = _context.Database.ExecuteSqlCommand("exec sp_prepClaimID2 @tvpClaimID", parameter).ToString();
                }
                else if (ClaimStatusID == "4")
                {
                    var parameter = new SqlParameter("@tvpReview", SqlDbType.Structured);
                    parameter.TypeName = "tvpReviewID";
                    parameter.Value = dt_tmpClaimID;
                    result = _context.Database.ExecuteSqlCommand("exec sp_prepStaffReview @tvpReview", parameter).ToString();
                }
                    
                return result;
            }
            catch (Exception e)
            {
                return "0";
            }

            ////string cnnString = Environment.GetEnvironmentVariable("ConnectionStrings__hox_connect");
            ////SqlConnection cnn = new SqlConnection(cnnString);
            ////SqlCommand cmd = new SqlCommand();
            ////cmd.Connection = cnn;
            ////cmd.CommandType = System.Data.CommandType.StoredProcedure;
            ////    cmd.CommandText = "sp_ClaimReceived";
            ////    //cmd.Parameters.Add(new SqlParameter("@ClaimStatusRefID", ClaimStatus));
            ////    //cmd.Parameters.Add(new SqlParameter("@ClaimDate", Convert.ToDateTime(ClaimReceivedDate)));
            ////    //cmd.Parameters.Add(new SqlParameter("@ReceivedBy", strAssignor));
            ////    cmd.Parameters.Add(new SqlParameter("@tvpClaimID", dt_tmpClaimID));
            ////cnn.Open();
            ////object o = cmd.ExecuteScalar();
            ////cnn.Close();
        }

        public string ValidateInfo(string ClaimID, string AIN, string ClaimStatusID, string ClaimReceivedDate)
        {
            string strAssignor = User.FindFirst("Name").Value;
            try
            {
                var result = new ClaimInfo();
                var pClaimStatus = new SqlParameter("@ClaimStatusRefID", ClaimStatusID);
                var pClaimID = new SqlParameter("@ClaimID", ClaimID);
                var pAIN = new SqlParameter("@AIN", AIN);
                result = _context.ClaimInfo.FromSql("sp_chk_ClaimID_AIN @ClaimStatusRefID, @ClaimID, @AIN", pClaimStatus, pClaimID, pAIN).FirstOrDefaultAsync().Result;

                return result.info;
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        public string ProcessInfo(string ClaimID, string AIN, string ClaimStatusID, string ClaimReceivedDate)
        {
            string strAssignor = User.FindFirst("Name").Value;
            try
            {               
                string result = _context.Database.ExecuteSqlCommand("sp_create_ClaimID_AIN @p0,@p1,@p2,@p3", new SqlParameter("@p0", ClaimID), new SqlParameter("@p1", AIN), new SqlParameter("@p2", ClaimReceivedDate), new SqlParameter("@p3", strAssignor)).ToString(); 

                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        private IEnumerable<SelectListItem> GetAllClaimStatus()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            if (User.FindFirst("RoleTitle").Value == "3")
            {
                li.Add(new SelectListItem { Text = "Select Status", Value = "0" });
                li.Add(new SelectListItem { Text = "Claim Received", Value = "2" });
                li.Add(new SelectListItem { Text = "Supervisor Workload", Value = "3" });
            }
            else
            {
                li.Add(new SelectListItem { Text = "Select Status", Value = "0" });
                li.Add(new SelectListItem { Text = "Claim Received", Value = "2" });
                li.Add(new SelectListItem { Text = "Supervisor Workload", Value = "3" });
                li.Add(new SelectListItem { Text = "Staff Review", Value = "4" });
                li.Add(new SelectListItem { Text = "Closed", Value = "6" });
            }
            IEnumerable<SelectListItem> item = li.AsEnumerable();
            return item;
        }

        private IEnumerable<SelectListItem> GetAllSupervisors()
        {
            List<Supervisors> lSupervisors = new List<Supervisors>();
            lSupervisors = _context.Supervisors.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Supervisor", Value = "0" });
            foreach (var oneSupervisor in lSupervisors)
            {
                //if (oneSupervisor.Users != User.FindFirst("UserName").Value)
                li.Add(new SelectListItem { Text = oneSupervisor.Users, Value = oneSupervisor.EmployeeID });
            }
            IEnumerable<SelectListItem> item = li.AsEnumerable();
            return item;
        }

        private IEnumerable<SelectListItem> GetAllStaffs()
        {
            List<UserStaff> lStaffs = new List<UserStaff>();
            lStaffs = _context.UserStaff.FromSql("sp_getStaffDropdown").ToListAsync().Result.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Staff", Value = "0" });
            foreach (var oneStaff in lStaffs)
            {
                li.Add(new SelectListItem { Text = oneStaff.Users, Value = oneStaff.EmployeeID });
            }
            IEnumerable<SelectListItem> item = li.AsEnumerable();
            return item;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
