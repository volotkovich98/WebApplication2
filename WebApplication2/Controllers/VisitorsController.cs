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
    public class VisitorsController : Controller
    {
        private readonly sportcomplexContext _context;

        public VisitorsController(sportcomplexContext context)
        {
            _context = context;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            var sportcomplexContext = _context.Visitor.Include(v => v.Abonement).Include(v => v.Group);
            return View(await sportcomplexContext.ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor
                .Include(v => v.Abonement)
                .Include(v => v.Group)
                .SingleOrDefaultAsync(m => m.VisitorId == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["AbonementId"] = new SelectList(_context.Abonement, "AbonementId", "AbonementId");
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId");
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitorId,NameV,SurnameV,GroupId,AbonementId")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbonementId"] = new SelectList(_context.Abonement, "AbonementId", "AbonementId", visitor.AbonementId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", visitor.GroupId);
            return View(visitor);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor.SingleOrDefaultAsync(m => m.VisitorId == id);
            if (visitor == null)
            {
                return NotFound();
            }
            ViewData["AbonementId"] = new SelectList(_context.Abonement, "AbonementId", "AbonementId", visitor.AbonementId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", visitor.GroupId);
            return View(visitor);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitorId,NameV,SurnameV,GroupId,AbonementId")] Visitor visitor)
        {
            if (id != visitor.VisitorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorExists(visitor.VisitorId))
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
            ViewData["AbonementId"] = new SelectList(_context.Abonement, "AbonementId", "AbonementId", visitor.AbonementId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", visitor.GroupId);
            return View(visitor);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor
                .Include(v => v.Abonement)
                .Include(v => v.Group)
                .SingleOrDefaultAsync(m => m.VisitorId == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitor = await _context.Visitor.SingleOrDefaultAsync(m => m.VisitorId == id);
            _context.Visitor.Remove(visitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorExists(int id)
        {
            return _context.Visitor.Any(e => e.VisitorId == id);
        }
    }
}
