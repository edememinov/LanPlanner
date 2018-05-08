using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class Tournament
    {
        [Key]
        public int TournamentID { get; set; }
        public string TournamentName { get; set; }
    }

    public class TournantGames
    {
        [Key]
        public int TournamentGamesID { get; set; }
        public int TournamentID { get; set; }
        public int GameID { get; set; }
    }

    public class TournamentParticipants
    {
        [Key]
        public int TournamentParticipantsID { get; set; }
        public int TournamentID { get; set; }
        public string ParticipantID { get; set; }
    }
}
