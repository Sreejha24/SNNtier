using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCForAssessment.Models;

namespace MVCForAssessment.Controllers
{
    public class DependencyController : Controller
    {
        private readonly IDependencyDemo _Ddemo;

        public DependencyController(IDependencyDemo Ddemo)
        {
            _Ddemo = Ddemo;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult DependencyDemo()
        {
          ViewBag.result =   _Ddemo.Sum(4, 6);
            return View();
        }
    }
}
