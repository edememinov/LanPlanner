using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class LanParty
    {
        [Key]
        public int LanPartyID { get; set; }
        public string LanPartyName { get; set; }
        public string LanPartyPlace { get; set; }
        public string LanPartyAddress { get; set; }
        public virtual List<ApplicationUser> Attendees { get; set; }

    }

    public class LanPartyConcept : LanParty
    {
        public bool FinalCheck { get; set; }
        [NotMapped]
        public int AttendeeCount { get; set; }
    }

    public class LanPartyDate
    {
        [Key]
        public int DateID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTimeFinish { get; set; }

        public int LanPartyID { get; set; }
        [NotMapped]
        public bool CheckBoxAnswer { get; set; }
        

    }


    public class AttendeesDate
    {
        [Key]
        public int AtteendeesDateID { get; set; }
        public string AttendeeID { get; set; }
        public int DateID { get; set; }
        public int LanPartyID { get; set; }

    }

    public class LanPartyFinal : LanParty
    {
        public DateTime LanPartyFinalStartDate { get; set; }
        public DateTime LanPartyFinalFinishDate { get; set; }

        public int ConceptPartyID { get; set; }

        public int TournamentID { get; set; }
        [NotMapped]
        public string TournamentIDString { get; set; }
    }
}
