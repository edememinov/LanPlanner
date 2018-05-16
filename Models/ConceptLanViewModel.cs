using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class ConceptLanViewModel
    {
        public List<LanPartyDate> LanPartyDates { get; set; }
        public IEnumerable<LanPartyDate> ConceptDates { get; set; }

        public LanPartyConcept LanPartyConcept { get; set; }
        public IEnumerable<LanPartyConcept> ConceptLanParties { get; set; }
        public List<ApplicationUser> Attendees { get; set; }
        public AttendeesDate AttendeesDate { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<AttendeesDate> AttendeesDates { get; set; }

        public ApplicationUser CurrentUser { get; set; }

    }
}
