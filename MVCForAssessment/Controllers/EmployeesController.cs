using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCForAssessment.Data;
using MVCForAssessment.Models;
using MVCForAssessment.Models.View;
using X.PagedList;

namespace MVCForAssessment.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employee.Include(e => e.Depts).OrderBy(s => s.Depts);
            return View(await applicationDbContext.ToListAsync());
        }

        public async  Task<IActionResult> Sorting(string data)
        {
            ViewData["EmpId"] = string.IsNullOrEmpty(data) ? "EmpId" : " ";
            ViewData["EmpName"] = string.IsNullOrEmpty(data) ? "EmpName" : " ";
            ViewData["Salary"] = string.IsNullOrEmpty(data) ? "Salary" : " ";
            ViewData["City"] = string.IsNullOrEmpty(data) ? "City" : " ";
            ViewData["DeptId"] = string.IsNullOrEmpty(data) ? "DeptId" : " ";

            var data1 = from e in _context.Employee
                       select e;
            switch (data)
            {
                case "EmpId":
                    data1 = data1.OrderBy(s => s.EmpId);
                    break;
                case "EmpName":
                    data1 = data1.OrderBy(s => s.EmpName);
                    break;
                case "Salary":
                    data1 = data1.OrderBy(s => s.Salary);
                    break;
                case "City":
                    data1 = data1.OrderBy(s => s.City);
                    break;
                case "DeptId":
                    data1 = data1.OrderBy(s => s.DeptId);
                    break;
                default:
                    data1 = data1.OrderBy(s => s.EmpId);
                    break;
            }
            return View(data1);



        }

        public async Task<IActionResult> Search()
        {
            var applicationDbContext = _context.Employee.Include(e => e.Depts);
            return View(await applicationDbContext.ToListAsync());
        }

        public JsonResult SearchVal(string searchBy, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            if (searchBy == "EmpId")
            {
                int id = int.Parse(searchValue);
                data = _context.Employee.Where(d => d.EmpId == id || searchValue == null).ToList();
                return Json(data);
            }
            else if (searchBy == "EmpName")
            {
                data = _context.Employee.Where(d => d.EmpName.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(data);
            }
            else if (searchBy == "City")
            {
                data = _context.Employee.Where(d => d.City.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(data);
            }
            else
            {
                return Json("Index");
            }
        }

        public async Task<IActionResult> Paging(int? page, int? pagesize)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            if (!pagesize.HasValue)
            {
                pagesize = 5;
            }

            var data = await _context.Employee.ToPagedListAsync(page.Value, pagesize.Value);
            return View(data);
        }
        // GET: Employees/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Depts)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_context.Set<Depts>(), "DeptId", "DeptId");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,City,Salary,DeptId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Set<Depts>(), "DeptId", "DeptId", employee.DeptId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(_context.Set<Depts>(), "DeptId", "DeptId", employee.DeptId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,EmpName,City,Salary,DeptId")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Set<Depts>(), "DeptId", "DeptId", employee.DeptId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Depts)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmpId == id);
        }


        public async Task<IActionResult> RawQuery(int id)
        {
            var sql = "Select * from Employee";
            var data = _context.Employee.FromSqlRaw(sql, id);
            return View(data);
        }


        public IActionResult RawQueryComplex()
        {
            IList<EDepts> eDepts = new List<EDepts>();
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    var sql = "SELECT E.EmpId, E.EmpName, E.City, E.Salary, E.DeptId, D.DeptName FROM Employee E, Depts D " +
                        " WHERE E.DeptId = D.DeptId";
                    command.CommandText = sql;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var record = new EDepts()
                                {
                                    EmpId = reader.GetInt32(0),
                                    EmpName = reader.GetString(1),
                                    City = reader.GetString(2),
                                    Salary = reader.GetDecimal(3),
                                    DeptId = reader.GetInt32(4),
                                    DeptName = reader.GetString(5),
                                };
                                eDepts.Add(record);
                            }
                        }
                    } // reader
                } // command
                conn.Close();
            } // connection

            return View(eDepts);
        }
    }
}