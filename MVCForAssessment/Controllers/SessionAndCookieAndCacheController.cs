using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using MVCForAssessment.Data;
using MVCForAssessment.Models;
using Newtonsoft.Json;

namespace MVCForAssessment.Controllers
{
    public class SessionAndCookieAndCacheController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cacheData;
        private readonly IDistributedCache _rcache;

        public SessionAndCookieAndCacheController(ApplicationDbContext context, IMemoryCache cacheData,IDistributedCache Rcache)
        {
            _context = context;
            _cacheData = cacheData;
            _rcache = Rcache;
        }
        public IActionResult Cache()
        {
            var cache = _cacheData.Get("Employeedetails");
            IList<Employee> data = new List<Employee>();
            if(cache == null)
            {
                 data = _context.Employee.ToList();
                 var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(25));
                _cacheData.Set("Employeedetails", data, cacheOptions);
            }
            else
            {
                data = (IList<Employee>)cache;
            }
            return View(data);
        }






        [ResponseCache(Duration = 30000)]
        public async Task<IActionResult> ResponseCaching()
        {
            return View("ResponseCaching", await _context.Employee.ToListAsync());
        }






        public async  Task<IActionResult> RedisCache()
        {
            var cache = _rcache.GetString("GetEmployee");
          
            IList<Employee> data = new List<Employee>();
            if(string.IsNullOrEmpty(cache))
            {
                data = _context.Employee.ToList();
                var dataString = JsonConvert.SerializeObject(data);
                var cacheOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(25));
                await _rcache.SetStringAsync("GetEmployee", dataString, cacheOptions);
                return Json(dataString);
            }
            else
            {
                data= JsonConvert.DeserializeObject<IList<Employee>>(cache);
                return Json(data);
            }
           
        }

        public IActionResult Session()
        {
            HttpContext.Response.Cookies.Append("name", "Sreejha");
           
            CookieOptions co = new CookieOptions()
            {
                Expires = DateTime.Now.AddSeconds(15)
            };
            HttpContext.Response.Cookies.Append("address", "tenali,522201",co);

            HttpContext.Session.SetString("Name", "Roshini");
            HttpContext.Session.GetString("Name");
            return View();
        }

        public IActionResult Get()
        {
            string address = string.Empty;
            HttpContext.Request.Cookies.TryGetValue("address",out address);
            return View();

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
