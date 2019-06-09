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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homeowners_Ex_New.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly homeownerContext _context;

        public HomeController (homeownerContext context)
        {
           
            _context = context;
            
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Claim(Int64? id)
        //{
        //    id = 1234567;
        //    dynamic FaqFinalModel = new System.Dynamic.ExpandoObject();
        //    SqlParameter param1 = new SqlParameter("@ClaimID", id);
        //    //var abc = db.tablename.SqlQuery("SP_Name @Value1", param1).ToList();
        //    var ClaimResult = _context.claim.FromSql("sp_getClaim1 @ClaimID", param1);
        //    FaqFinalModel.claim = ClaimResult;
        //    return View(FaqFinalModel);
        //}
        public IActionResult Claim(int? id)
        {
            var modelUser = new Claim();
            if (id != null)
            {
                var EmpID = new SqlParameter("@EmployeeID", id);
                 modelUser = _context.Claim.FromSql("sp_getClaim @EmployeeID", EmpID).FirstOrDefaultAsync().Result;
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
            model.Supervisors = GetAllSupervisors();
            model.Staffs = GetAllStaffs();

            return View(model);
        }

        //[HttpPost]
        //public IActionResult ProcessClaim(ProcessClaim model)
        //{
        //    model.Supervisors = GetAllSupervisors();
        //    model.Staffs = GetAllStaffs();

        //    if (ModelState.IsValid)
        //    {



        //        return View();
        //    }
        //    else
        //        return View(model);
        //}

        public string GetClaimInfo(IEnumerable<int> ClaimIDList, IEnumerable<int> AINList, string ClaimStatus, string ClaimReceivedDate, string AssigneeSupervisor, string AssigneeStaff)
        {
            if (ClaimStatus == "2") //Claim Received
            {

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


            return "1";
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
            lStaffs = _context.Staffs.FromSql("sp_users").ToListAsync().Result.ToList();
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
