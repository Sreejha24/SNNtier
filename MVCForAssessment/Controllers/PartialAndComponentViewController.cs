using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCForAssessment.Data;

namespace MVCForAssessment.Controllers
{
    public class PartialAndComponentViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartialAndComponentViewController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var emp = _context.Employee.ToList();
            var dept = _context.Depts.ToList();
            ViewBag.Employee = emp;
            ViewData["Depts"] = dept;

            TempData["sreejha"] = "From Tenali";
            TempData.Keep("sreejha");
         
            return View();
        }

        public IActionResult Tempdata()
        {
            string address = string.Empty;
            if (TempData.ContainsKey("sreejha"))
            {
                address = TempData["sreejha"].ToString();
                
            }
            return View();

        }
    }
}
