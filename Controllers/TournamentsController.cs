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
    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public TournamentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Tournaments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tournaments.ToListAsync());
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .SingleOrDefaultAsync(m => m.TournamentID == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            TournamentViewModel tournament = new TournamentViewModel();
            tournament.GameList = _context.Games.Select(x => new SelectListItem { Text = x.GameName, Value = x.GameName }).ToList();
            return View(tournament);
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TournamentViewModel TournamentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Tournaments.Add(TournamentViewModel.Tournament);
                await _context.SaveChangesAsync();
                foreach(var game in TournamentViewModel.TournamentGamesList)
                {
                    game.TournamentID = TournamentViewModel.Tournament.TournamentID;
                    _context.TournamentGames.Add(game);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(TournamentViewModel);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TournamentViewModel TournamentViewModel = new TournamentViewModel();
            TournamentViewModel.Tournament = await _context.Tournaments.SingleOrDefaultAsync(m => m.TournamentID == id);
            TournamentViewModel.TournamentGames = _context.TournamentGames.ToList().Where(x => x.TournamentID == id);
            TournamentViewModel.TournamentGamesList = TournamentViewModel.TournamentGames.ToList();
            TournamentViewModel.GameList = _context.Games.Select(x => new SelectListItem { Text = x.GameName, Value = x.GameName }).ToList();

            if (TournamentViewModel.Tournament == null)
            {
                return NotFound();
            }
            return View(TournamentViewModel);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TournamentViewModel TournamentViewModel)
        {
            if (id != TournamentViewModel.Tournament.TournamentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Tournaments.Update(TournamentViewModel.Tournament);
                    foreach(var game in TournamentViewModel.TournamentGamesList)
                    {
                        _context.Update(game);
                    }
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(TournamentViewModel.Tournament.TournamentID))
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
            return View(TournamentViewModel);
        }


        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Vote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TournamentViewModel TournamentViewModel = new TournamentViewModel();
            TournamentViewModel.Tournament = await _context.Tournaments.SingleOrDefaultAsync(m => m.TournamentID == id);
            TournamentViewModel.TournamentGames = _context.TournamentGames.ToList().Where(x => x.TournamentID == id);
            TournamentViewModel.TournamentGamesList = TournamentViewModel.TournamentGames.ToList();
            TournamentViewModel.ParticipantsGames = _context.ParticipantsGames.ToList().Where(x => x.TournamentID == id);
            TournamentViewModel.Participants = new List<ApplicationUser>();
            TournamentViewModel.GameList = _context.Games.Select(x => new SelectListItem { Text = x.GameName, Value = x.GameName }).ToList();
            TournamentViewModel.Users = _userManager.Users.ToList();
            TournamentViewModel.CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (TournamentViewModel.ParticipantsGames.Count() > 0)
            {
                foreach (var item in TournamentViewModel.ParticipantsGames)
                {
                    TournamentViewModel.Participants.Add(TournamentViewModel.Users.Where(x => x.Id == item.ParticipantID).FirstOrDefault());
                }
            }
            else
            {
                TournamentViewModel.Participants = null;
            }




            TournamentViewModel.Tournament = await _context.Tournaments
                .SingleOrDefaultAsync(m => m.TournamentID == id);

            if (TournamentViewModel.Tournament == null)
            {
                return NotFound();
            }
            return View(TournamentViewModel);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vote(int id, TournamentViewModel TournamentViewModel)
        {
            if (id != TournamentViewModel.Tournament.TournamentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TournamentViewModel.Tournament = await _context.Tournaments
                .SingleOrDefaultAsync(m => m.TournamentID == id);
                    var user = await GetCurrentUserAsync();
                    foreach (var item in TournamentViewModel.TournamentGamesList)
                    {
                        if (item.CheckBoxAnswer == true)
                        {
                            ParticipantsGames participantsGames = new ParticipantsGames();
                            participantsGames.ParticipantID = user?.Id;
                            participantsGames.GameName = item.GameName;
                            participantsGames.TournamentID = TournamentViewModel.Tournament.TournamentID;
                            _context.Add(participantsGames);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(TournamentViewModel.Tournament.TournamentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(TournamentViewModel);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .SingleOrDefaultAsync(m => m.TournamentID == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournament = await _context.Tournaments.SingleOrDefaultAsync(m => m.TournamentID == id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.TournamentID == id);
        }
    }
}
