using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCForAssessment.Data;
using MVCForAssessment.Models;

namespace MVCForAssessment.Controllers
{
    public class DeptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Depts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depts.ToListAsync());
        }

        // GET: Depts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depts = await _context.Depts
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (depts == null)
            {
                return NotFound();
            }

            return View(depts);
        }

        // GET: Depts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptId,DeptName")] Depts depts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depts);
        }

        // GET: Depts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depts = await _context.Depts.FindAsync(id);
            if (depts == null)
            {
                return NotFound();
            }
            return View(depts);
        }

        // POST: Depts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeptId,DeptName")] Depts depts)
        {
            if (id != depts.DeptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeptsExists(depts.DeptId))
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
            return View(depts);
        }

        // GET: Depts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depts = await _context.Depts
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (depts == null)
            {
                return NotFound();
            }

            return View(depts);
        }

        // POST: Depts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depts = await _context.Depts.FindAsync(id);
            _context.Depts.Remove(depts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeptsExists(int id)
        {
            return _context.Depts.Any(e => e.DeptId == id);
        }
    }
}
