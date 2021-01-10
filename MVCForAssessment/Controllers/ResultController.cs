using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCForAssessment.Data;

namespace MVCForAssessment.Controllers
{
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext context;

        public IWebHostEnvironment WebHost { get; }

        public ResultController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            WebHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ContentResult content()
        {
            return Content("Madhu kadupu arusthundhi");
        }
        public JsonResult json()
        {
           // var emp = context.Employee;
            return Json("Jai Balayya");
        }

        public FileResult file()
        {
            string path = WebHost.ContentRootPath;
            string fullPath = Path.Combine(path, "appsettings.json");
            var content = System.IO.File.ReadAllBytes(fullPath);
            return File(content, "application/json");
        }
}
}
