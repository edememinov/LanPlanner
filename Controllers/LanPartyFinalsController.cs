using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLanParty.Data;
using PlannerLanParty.Models;

namespace PlannerLanParty.Controllers
{
    public class LanPartyFinalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanPartyFinalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LanPartyFinals
        public async Task<IActionResult> Index()
        {
            return View(await _context.LanParties.ToListAsync());
        }

        // GET: LanPartyFinals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanPartyFinal = await _context.LanParties
                .SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyFinal == null)
            {
                return NotFound();
            }

            return View(lanPartyFinal);
        }

        // GET: LanPartyFinals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LanPartyFinals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanPartyFinalDate,LanPartyID,LanPartyName,LanPartyPlace,LanPartyAddress")] LanPartyFinal lanPartyFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lanPartyFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lanPartyFinal);
        }

        // GET: LanPartyFinals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanPartyFinal = await _context.LanParties.SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyFinal == null)
            {
                return NotFound();
            }
            return View(lanPartyFinal);
        }

        // POST: LanPartyFinals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanPartyFinalDate,LanPartyID,LanPartyName,LanPartyPlace,LanPartyAddress")] LanPartyFinal lanPartyFinal)
        {
            if (id != lanPartyFinal.LanPartyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lanPartyFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanPartyFinalExists(lanPartyFinal.LanPartyID))
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
            return View(lanPartyFinal);
        }

        // GET: LanPartyFinals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanPartyFinal = await _context.LanParties
                .SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyFinal == null)
            {
                return NotFound();
            }

            return View(lanPartyFinal);
        }

        // POST: LanPartyFinals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lanPartyFinal = await _context.LanParties.SingleOrDefaultAsync(m => m.LanPartyID == id);
            _context.LanParties.Remove(lanPartyFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanPartyFinalExists(int id)
        {
            return _context.LanParties.Any(e => e.LanPartyID == id);
        }
    }
}
