
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class TournamentViewModel
    {
        public List<Game> Games { get; set; }
        public List<SelectListItem> GameList { get; set; }
        public Game Game { get; set; }
        public List<ApplicationUser> Participants { get; set; }
        public IEnumerable<TournamentGames> TournamentGames { get; set; }
        public List<TournamentGames> TournamentGamesList { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public Tournament Tournament { get; set; }
        public IEnumerable<ParticipantsGames> ParticipantsGames { get; set; }
        public ApplicationUser CurrentUser { get; set; }
    }
}
