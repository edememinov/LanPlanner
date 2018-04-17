using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLanParty.Data;
using PlannerLanParty.Models;


namespace PlannerLanParty.Controllers
{
    public class LanPartyConceptsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LanPartyConceptsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: LanPartyConcepts
        public async Task<IActionResult> Index()
        {
            return View(await _context.LanPartyConcept.ToListAsync());
        }

        // GET: LanPartyConcepts/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id, ConceptLanViewModel lanPartyConceptViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }
            lanPartyConceptViewModel.LanPartyDates = _context.LanPartyDates.Where(x => x.LanPartyID == id);

            lanPartyConceptViewModel.LanPartyConcept = await _context.LanPartyConcept
                .SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyConceptViewModel.LanPartyConcept == null)
            {
                return NotFound();
            }

            return View(lanPartyConceptViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Vote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ConceptLanViewModel lanPartyConceptViewModel = new ConceptLanViewModel();
            lanPartyConceptViewModel.LanPartyDates = _context.LanPartyDates.Where(x => x.LanPartyID == id);
            lanPartyConceptViewModel.AttendeesDates = _context.AttendeesDates.Where(x => x.LanPartyID == id);
            if (lanPartyConceptViewModel.AttendeesDates.Count() > 0)
            {
                foreach (var item in lanPartyConceptViewModel.AttendeesDates)
                {
                    lanPartyConceptViewModel.Attendees.Add(lanPartyConceptViewModel.Users.Where(x => x.Id == item.AttendeeID).FirstOrDefault());
                }
            }
            

            lanPartyConceptViewModel.LanPartyConcept = await _context.LanPartyConcept
                .SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyConceptViewModel.LanPartyConcept == null)
            {
                return NotFound();
            }

            return View(lanPartyConceptViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Vote(int? id, ConceptLanViewModel lanPartyConceptViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var user = await GetCurrentUserAsync();
            lanPartyConceptViewModel.LanPartyDates = _context.LanPartyDates.Where(x => x.LanPartyID == id);
            foreach(var item in lanPartyConceptViewModel.LanPartyDates)
            {
                if(item.CheckkBoxAnswer == true)
                {
                    AttendeesDate attendeesDate = new AttendeesDate();
                    attendeesDate.AttendeeID = user?.Id;
                    attendeesDate.DateID = item.DateID;
                    attendeesDate.LanPartyID = item.LanPartyID;
                    _context.Add(attendeesDate);
                }
            }
            lanPartyConceptViewModel.LanPartyConcept = await _context.LanPartyConcept
                .SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyConceptViewModel.LanPartyConcept == null)
            {
                return NotFound();
            }

            return View(lanPartyConceptViewModel);
        }

        // GET: LanPartyConcepts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LanPartyConcepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConceptLanViewModel lanPartyConceptViewModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(lanPartyConceptViewModel.LanPartyConcept);
                foreach(var item in lanPartyConceptViewModel.LanPartyDates)
                {
                    item.LanPartyID = lanPartyConceptViewModel.LanPartyConcept.LanPartyID;
                    _context.LanPartyDates.Add(item);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lanPartyConceptViewModel);
        }

        // GET: LanPartyConcepts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ConceptLanViewModel lanPartyConceptViewModel = new ConceptLanViewModel();

            lanPartyConceptViewModel.LanPartyConcept = await _context.LanPartyConcept.SingleOrDefaultAsync(m => m.LanPartyID == id);
            lanPartyConceptViewModel.LanPartyDates =  _context.LanPartyDates.Where(x => x.LanPartyID == id);
            if (lanPartyConceptViewModel.LanPartyConcept == null)
            {
                return NotFound();
            }
            return View(lanPartyConceptViewModel);
        }

        // POST: LanPartyConcepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConceptLanViewModel lanPartyConceptViewModel)
        {
            if (id != lanPartyConceptViewModel.LanPartyConcept.LanPartyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in lanPartyConceptViewModel.LanPartyDates)
                    {
                        item.LanPartyID = lanPartyConceptViewModel.LanPartyConcept.LanPartyID;
                        _context.LanPartyDates.Add(item);
                    }
                    _context.Update(lanPartyConceptViewModel.LanPartyConcept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanPartyConceptExists(lanPartyConceptViewModel.LanPartyConcept.LanPartyID))
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
            return View(lanPartyConceptViewModel.LanPartyConcept);
        }

        // GET: LanPartyConcepts/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(lanPartyConcept);
        }

        // POST: LanPartyConcepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ConceptLanViewModel lanPartyConceptViewModel)
        {
            var lanPartyConcept = await _context.LanPartyConcept.SingleOrDefaultAsync(m => m.LanPartyID == id);
            lanPartyConceptViewModel.LanPartyDates = _context.LanPartyDates.Where(x => x.LanPartyID == id);
            foreach (var item in lanPartyConceptViewModel.LanPartyDates)
            {
                item.LanPartyID = lanPartyConceptViewModel.LanPartyConcept.LanPartyID;
                _context.LanPartyDates.Remove(item);
            }
            _context.LanPartyConcept.Remove(lanPartyConcept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanPartyConceptExists(int id)
        {
            return _context.LanPartyConcept.Any(e => e.LanPartyID == id);
        }
    }
}
