using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class Round
    {
        [Key]
        public int RoundID { get; set; }
        public int GameID { get; set; }
        [NotMapped]
        public int  RoundsAmount { get; set; }
        public int TournamentID { get; set; }
        [NotMapped]
        public string TournamentIDString { get; set; }
        [NotMapped]
        public string GameIDString { get; set; }
        [NotMapped]
        public bool Recurring { get; set; }
        public string Description { get; set; }
    }

    public class RoundParticipant
    {
        [Key]
        public int RoundParticipantID { get; set; }
        public int RoundID { get; set; }
        public int ParticipantID { get; set; }
        public int ParticipantScore { get; set; }
        [NotMapped]
        public string ParticipantEmail { get; set; }
        [NotMapped]
        public string ParticipantPoints { get; set; }
    }

    public class RoundWinners
    {
        [Key]
        public int RoundWinnerID { get; set; }
        public int FirstPlace { get; set; }
        public int SecondPlace { get; set; }
        public int ThirdPlace { get; set; }
        public int FourthPlace { get; set; }
        public int FifthPlace { get; set; }
        [NotMapped]
        public string FirstPlaceString { get; set; }
        [NotMapped]
        public string SecondPlaceString { get; set; }
        [NotMapped]
        public string ThirdPlaceString { get; set; }
        [NotMapped]
        public string FourthPlaceString { get; set; }
        [NotMapped]
        public string FifthPlaceString { get; set; }





    }
}
