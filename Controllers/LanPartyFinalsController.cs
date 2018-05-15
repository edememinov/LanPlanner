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
        public async Task<IActionResult> Create(LanPartyFinal lanPartyFinal)
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

        [HttpGet]
        public async Task<IActionResult> MakeFinal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanPartyConcept = await _context.LanPartyConcept
                .SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyConcept == null)
            {
                return NotFound();
            }
            var lanPartyFinal = new FinalLanViewModel();
            var attendeeDates = new List<AttendeesDate>();
            attendeeDates = _context.AttendeesDates.ToList().Where(x => x.LanPartyID == id).ToList();
            lanPartyFinal.LanPartyFinal = new LanPartyFinal();
            lanPartyFinal.LanPartyFinal.LanPartyAddress = lanPartyConcept.LanPartyAddress;
            var dates = attendeeDates
            .GroupBy(n => n.DateID)
            .Select(n => new
            {
                MetricName = n.Key,
                MetricCount = n.Count()
            }
            )
            .OrderBy(n => n.MetricCount);
            var mostKey = dates.First().MetricName;
            var lanDateFinal = _context.LanPartyDates.Where(x => x.DateID == mostKey).First();
            lanPartyFinal.LanPartyFinal.LanPartyFinalStartDate = lanDateFinal.DateTimeStart;
            lanPartyFinal.LanPartyFinal.LanPartyFinalFinishDate = lanDateFinal.DateTimeFinish;
            lanPartyFinal.LanPartyFinal.LanPartyName = lanPartyConcept.LanPartyName;
            lanPartyFinal.LanPartyFinal.LanPartyPlace = lanPartyConcept.LanPartyPlace;
            lanPartyFinal.LanPartyFinal.ConceptPartyID = lanPartyConcept.LanPartyID;
            lanPartyFinal.TournamentList = _context.Tournaments.Select(x => new SelectListItem { Text = x.TournamentName, Value = x.TournamentID.ToString() }).ToList();




            return View(lanPartyFinal);
        }

        [HttpPost]
        public async Task<IActionResult> MakeFinal (FinalLanViewModel FinalLanViewModel)
        {
            FinalLanViewModel.LanPartyFinal.TournamentID = Int32.Parse(FinalLanViewModel.LanPartyFinal.TournamentIDString);
            if (ModelState.IsValid)
            {
                _context.Add(FinalLanViewModel.LanPartyFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(FinalLanViewModel);
        }
    }
}
