using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homeowners_Ex_New.Models;

namespace Homeowners_Ex_New.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Claim(Int64?id)
        {
            id = 1234567;
            dynamic FaqFinalModel = new System.Dynamic.ExpandoObject();
            SqlParameter param1 = new SqlParameter("@ClaimID", id);
            //var abc = db.tablename.SqlQuery("SP_Name @Value1", param1).ToList();
            var ClaimResult = _context.claim.FromSql("sp_getClaim1 @ClaimID",param1);
            FaqFinalModel.claim = ClaimResult;
                return View(FaqFinalModel);
        public IActionResult ProcessClaim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessClaim(string sClaimStatus)
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
