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
    public class AbonementsController : Controller
    {
        private readonly sportcomplexContext _context;

        public AbonementsController(sportcomplexContext context)
        {
            _context = context;
        }

        // GET: Abonements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abonement.ToListAsync());
        }

        // GET: Abonements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonement = await _context.Abonement
                .SingleOrDefaultAsync(m => m.AbonementId == id);
            if (abonement == null)
            {
                return NotFound();
            }

            return View(abonement);
        }

        // GET: Abonements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abonements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AbonementId,NumberOfVisits,Price")] Abonement abonement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abonement);
        }

        // GET: Abonements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonement = await _context.Abonement.SingleOrDefaultAsync(m => m.AbonementId == id);
            if (abonement == null)
            {
                return NotFound();
            }
            return View(abonement);
        }

        // POST: Abonements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AbonementId,NumberOfVisits,Price")] Abonement abonement)
        {
            if (id != abonement.AbonementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonementExists(abonement.AbonementId))
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
            return View(abonement);
        }

        // GET: Abonements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonement = await _context.Abonement
                .SingleOrDefaultAsync(m => m.AbonementId == id);
            if (abonement == null)
            {
                return NotFound();
            }

            return View(abonement);
        }

        // POST: Abonements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonement = await _context.Abonement.SingleOrDefaultAsync(m => m.AbonementId == id);
            _context.Abonement.Remove(abonement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonementExists(int id)
        {
            return _context.Abonement.Any(e => e.AbonementId == id);
        }
    }
}
