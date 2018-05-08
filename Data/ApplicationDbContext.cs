using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlannerLanParty.Models;

namespace PlannerLanParty.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LanPartyFinal> LanParties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<PlannerLanParty.Models.LanPartyConcept> LanPartyConcept { get; set; }
        public DbSet<LanPartyDate> LanPartyDates { get; set; }
        public DbSet<AttendeesDate> AttendeesDates { get; set; }
        public DbSet<TournamentParticipants> TournamentParticipants { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<TournantGames> TournantGames { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }





    }
}
