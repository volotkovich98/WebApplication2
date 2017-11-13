using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class GroupsController : Controller
    {
        private readonly sportcomplexContext _context;

        public GroupsController(sportcomplexContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var sportcomplexContext = _context.Groups.Include(g => g.Instructor).Include(g => g.Schedule);
            return View(await sportcomplexContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .Include(g => g.Instructor)
                .Include(g => g.Schedule)
                .SingleOrDefaultAsync(m => m.GroupId == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorId");
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,InstructorId,Groupname,NumberOfLessons,ScheduleId")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorId", groups.InstructorId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", groups.ScheduleId);
            return View(groups);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.SingleOrDefaultAsync(m => m.GroupId == id);
            if (groups == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorId", groups.InstructorId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", groups.ScheduleId);
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,InstructorId,Groupname,NumberOfLessons,ScheduleId")] Groups groups)
        {
            if (id != groups.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsExists(groups.GroupId))
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
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "InstructorId", groups.InstructorId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "ScheduleId", "ScheduleId", groups.ScheduleId);
            return View(groups);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .Include(g => g.Instructor)
                .Include(g => g.Schedule)
                .SingleOrDefaultAsync(m => m.GroupId == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await _context.Groups.SingleOrDefaultAsync(m => m.GroupId == id);
            _context.Groups.Remove(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsExists(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }
    }
}
