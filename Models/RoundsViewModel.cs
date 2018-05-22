using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class RoundsViewModel
    {
        public Round Round { get; set; }
        public RoundWinners RoundWinners { get; set; }
        public List<Round> TournamentRounds { get; set; }
        public List<SelectListItem> GameList { get; set; }
        public List<SelectListItem> TournamentList { get; set; }
        public List<ApplicationUser> Participants { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<ParticipantsGames> ParticipantsGames { get; set; }
        public ApplicationUser CurrentUser { get; set; }
    }
}
