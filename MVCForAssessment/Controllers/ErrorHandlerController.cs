using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVCForAssessment.Controllers
{
    public class ErrorHandlerController : Controller
    {
        private readonly ILogger<ErrorHandlerController> _logger;

        public ErrorHandlerController(ILogger<ErrorHandlerController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var z = 0;
                return Content((1 / z).ToString());
            }
            catch (Exception ce)
            {
                _logger.LogError("Error occured.While Processing The request");
                ViewBag.error = "Error occured";
                
                throw;
            }
            return View();
        }
    }
}
