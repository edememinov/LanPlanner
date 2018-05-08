using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class TournamentViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public List<Game> GamesList { get; set; }

        public IEnumerable<TournamentParticipants> Participants { get; set; }
        public IEnumerable<TournantGames> TournantGames { get; set; }
        public List<TournantGames> TournantGamesList { get; set; }

        public Tournament Tournament { get; set; }
    }
}
