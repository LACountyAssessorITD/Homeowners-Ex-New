﻿using System;
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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Claim = HomeOwners_Exemption.Models.Claim;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Data;

namespace Homeowners_Ex_New.Controllers
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
            var supList = _context.Staffs.FromSql("sp_getSupervisors").ToListAsync().Result.ToList();
            staffList.AddRange(supList);

            Dictionary<string, StatusCount> statusList = new Dictionary<string, StatusCount>();
            statusList.Add("Preprint Sent", new StatusCount(0,0));
            statusList.Add("Claim Received", new StatusCount(0, 0));
            statusList.Add("Supervisor Workload", new StatusCount(0, 0));
            statusList.Add("Staff Review", new StatusCount(0, 0));
            statusList.Add("Hold", new StatusCount(0, 0));
            statusList.Add("Closed", new StatusCount(0, 0));
            List<string> statusID = new List<string>(new string[]
            {
                "Preprint Sent",
                "Claim Received",
                "Supervisor Workload",
                "Staff Review",
                "Hold",
                "Closed"
            });
            var statusDbList = _context.statusList.FromSql("sp_getCountsByStatus").ToListAsync().Result.ToList();
            foreach (var item in statusDbList)
            {
                statusList[item.ClaimStatusRef] = new StatusCount(item.OrderCount, item.Late);
             }
            var EmpID = new SqlParameter("@usersID", "471413");
            var claimList = _context.MyClaims.FromSql("sp_getListOfAssignedClaim @usersID", EmpID).ToListAsync().Result.ToList();

            var value = statusList[statusID[0]].Count;

            model.staff = staffList;
            model.claims = claimList;
            model.status = statusID;
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
            var modelUser = new Claim();
            ViewBag.ModelMessage = false;
            if (id != null)
            {
                var EmpID = new SqlParameter("@EmployeeID", id);
                 modelUser = _context.Claim.FromSql("sp_getClaim @EmployeeID", EmpID).FirstOrDefaultAsync().Result;
                if(modelUser == null)
                {
                    ViewBag.ModelMessage= true;
                }
            }
            else
            {
                modelUser = new Claim();
            }

            return View(modelUser);
        }

        public IActionResult ProcessClaim()
        {
            var model = new ProcessClaim();
            model.StatusList = GetAllClaimStatus();
            model.Supervisors = GetAllSupervisors();
            model.Staffs = GetAllStaffs();

            return View(model);
        }

        public string GetClaimInfo(IEnumerable<int> ClaimIDList, IEnumerable<int> AINList, string ClaimStatus, string ClaimReceivedDate, string AssigneeSupervisor, string AssigneeStaff)
        {
            string strAssignor = User.FindFirst("Name").Value;

            DataTable dt_tmpClaimID = new DataTable();
            dt_tmpClaimID.Columns.Add("ClaimID", typeof(string));
            dt_tmpClaimID.Columns.Add("AIN", typeof(string));
            DataRow tmpClaimID;

            if (ClaimStatus == "2")
            {
                List<int> lClaimIDList = ClaimIDList.ToList();
                List<int> lAINList = AINList.ToList();
                for (int i = 0; i < lClaimIDList.Count; i++)
                {
                    tmpClaimID = dt_tmpClaimID.NewRow();
                    tmpClaimID["ClaimID"] = Convert.ToString(lClaimIDList[i]);
                    tmpClaimID["AIN"] = Convert.ToString(lAINList[i]);
                    dt_tmpClaimID.Rows.Add(tmpClaimID);
                }
            }
            else
            {
                foreach (int claimID in ClaimIDList)
                {
                    tmpClaimID = dt_tmpClaimID.NewRow();
                    tmpClaimID["ClaimID"] = Convert.ToString(claimID);
                    dt_tmpClaimID.Rows.Add(tmpClaimID);
                }
            }

            string cnnString = Environment.GetEnvironmentVariable("ConnectionStrings__hox_connect");
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (ClaimStatus == "2") //Claim Received
            {
                cmd.CommandText = "sp_ClaimReceived";
                //cmd.Parameters.Add(new SqlParameter("@ClaimStatusRefID", ClaimStatus));
                //cmd.Parameters.Add(new SqlParameter("@ClaimDate", Convert.ToDateTime(ClaimReceivedDate)));
                //cmd.Parameters.Add(new SqlParameter("@ReceivedBy", strAssignor));
                cmd.Parameters.Add(new SqlParameter("@tvpClaimID", dt_tmpClaimID));
            }
            else if (ClaimStatus == "3") //Supervisor Workload
            {


            }
            else if (ClaimStatus == "4") //Staff Reivew
            {

            }
            else //"6" Case Closed or "7" Hold
            {

            }

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            return "1";
        }

        public string ValidateInfo(string ClaimID, string AIN, string ClaimStatus, string ClaimReceivedDate)
        {
            string strAssignor = User.FindFirst("Name").Value;

            //string cnnString = Environment.GetEnvironmentVariable("ConnectionStrings__hox_connect");
            //SqlConnection cnn = new SqlConnection(cnnString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = cnn;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "sp_ClaimReceived";
            //cmd.Parameters.Add(new SqlParameter("@ClaimStatusRefID", ClaimStatus));
            //cmd.Parameters.Add(new SqlParameter("@ClaimDate", Convert.ToDateTime(ClaimReceivedDate)));
            //cmd.Parameters.Add(new SqlParameter("@ReceivedBy", strAssignor));
            //cmd.Parameters.Add(new SqlParameter("@tvpClaimID", dt_tmpClaimID));
            //cnn.Open();
            //object o = cmd.ExecuteScalar();
            //cnn.Close();

            return "Mail Address: 100 Temple Ave, Los Angeles, CA 90000\n\n";
        }

        public string ProcessInfo(string ClaimID, string AIN, string ClaimStatus, string ClaimReceivedDate)
        {
            string strAssignor = User.FindFirst("Name").Value;

            //string cnnString = Environment.GetEnvironmentVariable("ConnectionStrings__hox_connect");
            //SqlConnection cnn = new SqlConnection(cnnString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = cnn;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "sp_ClaimReceived";
            //cmd.Parameters.Add(new SqlParameter("@ClaimStatusRefID", ClaimStatus));
            //cmd.Parameters.Add(new SqlParameter("@ClaimDate", Convert.ToDateTime(ClaimReceivedDate)));
            //cmd.Parameters.Add(new SqlParameter("@ReceivedBy", strAssignor));
            //cmd.Parameters.Add(new SqlParameter("@tvpClaimID", dt_tmpClaimID));
            //cnn.Open();
            //object o = cmd.ExecuteScalar();
            //cnn.Close();

            return "1";
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
            li.Add(new SelectListItem { Text = "Select Supervisor", Value = "" });
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
            List<Staffs> lStaffs = new List<Staffs>();
            lStaffs = _context.Staffs.FromSql("sp_getStaff").ToListAsync().Result.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select Staff", Value = "" });
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
