using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class ConceptLanViewModel
    {
        public IEnumerable<LanPartyDate> LanPartyDates { get; set; }
        public LanPartyConcept LanPartyConcept { get; set; }
        public List<ApplicationUser> Attendees { get; set; }
        public AttendeesDate AttendeesDate { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<AttendeesDate> AttendeesDates { get; set; }

    }
}
