using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCForAssessment.Controllers
{
    public class ViewComponentController : Controller
    {
        public IActionResult ComponentTest()
        {
            return View();
        }
    }
}
