using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannerLanParty.Data;
using PlannerLanParty.Models;

namespace PlannerLanParty.Controllers
{
    [Produces("application/json")]
    [Route("api/LanPartyFinalApi")]
    public class LanPartyFinalApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanPartyFinalApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LanPartyFinalApi
        [HttpGet]
        public IEnumerable<LanPartyFinal> GetLanParties()
        {
            return _context.LanParties;
        }

        // GET: api/LanPartyFinalApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLanPartyFinal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lanPartyFinal = await _context.LanParties.SingleOrDefaultAsync(m => m.LanPartyID == id);

            if (lanPartyFinal == null)
            {
                return NotFound();
            }

            return Ok(lanPartyFinal);
        }

        // PUT: api/LanPartyFinalApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanPartyFinal([FromRoute] int id, [FromBody] LanPartyFinal lanPartyFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lanPartyFinal.LanPartyID)
            {
                return BadRequest();
            }

            _context.Entry(lanPartyFinal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanPartyFinalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LanPartyFinalApi
        [HttpPost]
        public async Task<IActionResult> PostLanPartyFinal([FromBody] LanPartyFinal lanPartyFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LanParties.Add(lanPartyFinal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanPartyFinal", new { id = lanPartyFinal.LanPartyID }, lanPartyFinal);
        }

        // DELETE: api/LanPartyFinalApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanPartyFinal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lanPartyFinal = await _context.LanParties.SingleOrDefaultAsync(m => m.LanPartyID == id);
            if (lanPartyFinal == null)
            {
                return NotFound();
            }

            _context.LanParties.Remove(lanPartyFinal);
            await _context.SaveChangesAsync();

            return Ok(lanPartyFinal);
        }

        [HttpGet("{command}")]
        public IActionResult GetNextLan(string command)
        {
            if (command.Equals("next"))
            {
                var item = _context.LanParties.OrderBy(x => x.LanPartyFinalStartDate).FirstOrDefault();
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }

            return NotFound();
           
        }

        [HttpGet("{name}/{command}")]
        public async Task<IActionResult> GetMostVotedDate(string name, string command)
        {
            if (command.Equals("mostvoted"))
            {
                var lanPartyConcept = await _context.LanPartyConcept
                .SingleOrDefaultAsync(m => m.LanPartyName.ToLower().Contains(name.ToLower()));
                var lanPartyFinal = new FinalLanViewModel();
                var attendeeDates = new List<AttendeesDate>();
                attendeeDates = _context.AttendeesDates.ToList().Where(x => x.LanPartyID == lanPartyConcept.LanPartyID).ToList();
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
                var mostKey = dates.OrderBy(x => x.MetricCount).First().MetricName;
                var lanDateFinal = _context.LanPartyDates.Where(x => x.DateID == mostKey).First();
                lanDateFinal.VotedCount = dates.OrderBy(x => x.MetricCount).First().MetricCount;
                if (lanDateFinal == null)
                {
                    return NotFound();
                }
                return Ok(lanDateFinal);
            }

            return NotFound();

        }

        private bool LanPartyFinalExists(int id)
        {
            return _context.LanParties.Any(e => e.LanPartyID == id);
        }
    }
}