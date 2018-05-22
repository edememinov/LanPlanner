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
    public class RoundsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rounds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rounds.ToListAsync());
        }

        // GET: Rounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // GET: Rounds/Create
        public IActionResult Create()
        {
            RoundsViewModel roundsViewModel = new RoundsViewModel();
            roundsViewModel.GameList = _context.Games.Select(x => new SelectListItem { Text = x.GameName, Value = x.GameID.ToString() }).ToList();
            roundsViewModel.TournamentList = _context.Tournaments.Select(x => new SelectListItem { Text = x.TournamentName, Value = x.TournamentID.ToString() }).ToList();

            return View(roundsViewModel);
        }

        // POST: Rounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoundsViewModel roundsViewModel)
        {
            if (ModelState.IsValid)
            {
                var tourid = int.Parse(roundsViewModel.TournamentRounds[0].TournamentIDString);
                foreach (var item in roundsViewModel.TournamentRounds)
                {
                    item.TournamentID = tourid;
                    item.GameID = int.Parse(item.GameIDString);
                    for (int x = 0; x < item.RoundsAmount - 1;x++)
                    {
                        var round = new Round();
                        round.TournamentID = tourid;
                        round.GameID = item.GameID;
                        round.Description = item.Description;
                        _context.Add(round);
                    }
                    _context.Add(item);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roundsViewModel);
        }

        // GET: Rounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds.SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }
            return View(round);
        }

        // POST: Rounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoundID,GameID")] Round round)
        {
            if (id != round.RoundID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(round);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoundExists(round.RoundID))
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
            return View(round);
        }

        // GET: Rounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // POST: Rounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var round = await _context.Rounds.SingleOrDefaultAsync(m => m.RoundID == id);
            _context.Rounds.Remove(round);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoundExists(int id)
        {
            return _context.Rounds.Any(e => e.RoundID == id);
        }
    }
}
