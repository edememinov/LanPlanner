using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    public class TournamentGames
    {
        [Key]
        public int TournamentGamesID { get; set; }
        public int TournamentID { get; set; }
        public string GameName { get; set; }
        [NotMapped]
        public bool CheckBoxAnswer { get; set; }
    }

    public class TournamentParticipants
    {
        [Key]
        public int TournamentParticipantsID { get; set; }
        public int TournamentID { get; set; }
        public string ParticipantID { get; set; }
    }

    public class ParticipantsGames
    {
        [Key]
        public int ParticipantsGamesID { get; set; }
        public string ParticipantID { get; set; }
        public string GameName { get; set; }
        public int TournamentID { get; set; }

    }
}
