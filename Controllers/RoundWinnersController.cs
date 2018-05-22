using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLanParty.Data;
using PlannerLanParty.Models;

namespace PlannerLanParty.Controllers
{
    public class RoundWinnersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public RoundWinnersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: RoundWinners
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoundWinners.ToListAsync());
        }

        // GET: RoundWinners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roundWinners = await _context.RoundWinners
                .SingleOrDefaultAsync(m => m.RoundWinnerID == id);
            if (roundWinners == null)
            {
                return NotFound();
            }

            return View(roundWinners);
        }

        // GET: RoundWinners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoundWinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoundWinnerID,FirstPlace,SecondPlace,ThirdPlace,FourthPlace,FifthPlace")] RoundWinners roundWinners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roundWinners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roundWinners);
        }

        // GET: RoundWinners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roundWinners = await _context.RoundWinners.SingleOrDefaultAsync(m => m.RoundWinnerID == id);
            if (roundWinners == null)
            {
                return NotFound();
            }
            return View(roundWinners);
        }

        // POST: RoundWinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoundWinnerID,FirstPlace,SecondPlace,ThirdPlace,FourthPlace,FifthPlace")] RoundWinners roundWinners)
        {
            if (id != roundWinners.RoundWinnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roundWinners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoundWinnersExists(roundWinners.RoundWinnerID))
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
            return View(roundWinners);
        }

        // GET: RoundWinners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roundWinners = await _context.RoundWinners
                .SingleOrDefaultAsync(m => m.RoundWinnerID == id);
            if (roundWinners == null)
            {
                return NotFound();
            }

            return View(roundWinners);
        }

        // POST: RoundWinners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roundWinners = await _context.RoundWinners.SingleOrDefaultAsync(m => m.RoundWinnerID == id);
            _context.RoundWinners.Remove(roundWinners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> SetWinners(int id)
        {
            RoundsViewModel roundsViewModel = new RoundsViewModel();
            roundsViewModel.Round = await _context.Rounds
                .SingleOrDefaultAsync(m => m.RoundID == id);
            roundsViewModel.ParticipantsGames = _context.ParticipantsGames.ToList().Where(x => x.TournamentID == roundsViewModel.Round.TournamentID);
            roundsViewModel.Participants = new List<ApplicationUser>();
            roundsViewModel.GameList = _context.Games.Select(x => new SelectListItem { Text = x.GameName, Value = x.GameName }).ToList();
            roundsViewModel.Users = _userManager.Users.ToList();
            roundsViewModel.CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (roundsViewModel.ParticipantsGames.Count() > 0)
            {
                foreach (var item in roundsViewModel.ParticipantsGames)
                {
                    roundsViewModel.Participants.Add(roundsViewModel.Users.Where(x => x.Id == item.ParticipantID).FirstOrDefault());
                }
            }
            else
            {
                roundsViewModel.Participants = null;
            }

            if (roundsViewModel.Round == null)
            {
                return NotFound();
            }
            return View(roundsViewModel);
        }

        private bool RoundWinnersExists(int id)
        {
            return _context.RoundWinners.Any(e => e.RoundWinnerID == id);
        }
    }
}
