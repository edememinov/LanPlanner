using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class Tournament
    {
        public int TournamentID { get; set; }
        public string TournamentName { get; set; }
    }

    public class TournantGames
    {
        public int TournamentGamesID { get; set; }
        public int TournamentID { get; set; }
        public int GameID { get; set; }
    }

    public class TournamentParticipants
    {
        public int TournamentParticipantsID { get; set; }
        public int TournamentID { get; set; }
        public string ParticipantID { get; set; }
    }
}
