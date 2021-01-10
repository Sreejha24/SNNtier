using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCForAssessment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCForAssessment.Views.ViewComponents
{
    public class DemoViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public DemoViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _context.Employee.ToListAsync();
            return View(data);
        }
    }
}
