using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class FinalLanViewModel
    {
        public LanPartyConcept LanPartyConcept { get; set; }
        public LanPartyFinal LanPartyFinal { get; set; }

        public List<SelectListItem> TournamentList { get; set; }
    }
}
