 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeOwners_Exemption.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;

namespace HomeOwners_Exemption.Controllers
{
    public class HomeController : Controller
    {
        private readonly HOXContext _context;

        public HomeController( HOXContext context)
        {
           
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Claim()
        {   
             var test = _context.claim.FromSql("select * from claim");
            return View(test);
        }

        public IActionResult Privacy()
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
