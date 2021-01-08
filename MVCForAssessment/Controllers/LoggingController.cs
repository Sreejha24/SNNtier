using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVCForAssessment.Controllers
{
    public class LoggingController : Controller
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }
        public IActionResult Logging()
        {
            _logger.LogDebug(1,"index in Debugging Mode");
            _logger.LogInformation("information Logged into File");

            return View();
        }
    }
}
