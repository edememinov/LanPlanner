﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PlannerLanParty.Data;
using PlannerLanParty.Models;
using System;

namespace PlannerLanParty.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180516003014_fixed error on lanconcept")]
    partial class fixederroronlanconcept
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PlannerLanParty.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("LanPartyConceptLanPartyID");

                    b.Property<int?>("LanPartyFinalLanPartyID");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LanPartyConceptLanPartyID");

                    b.HasIndex("LanPartyFinalLanPartyID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PlannerLanParty.Models.AttendeesDate", b =>
                {
                    b.Property<int>("AtteendeesDateID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttendeeID");

                    b.Property<int>("DateID");

                    b.Property<int>("LanPartyID");

                    b.HasKey("AtteendeesDateID");

                    b.ToTable("AttendeesDates");
                });

            modelBuilder.Entity("PlannerLanParty.Models.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameName");

                    b.Property<int>("GameType");

                    b.HasKey("GameID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PlannerLanParty.Models.LanPartyConcept", b =>
                {
                    b.Property<int>("LanPartyID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("FinalCheck");

                    b.Property<string>("LanPartyAddress");

                    b.Property<string>("LanPartyName");

                    b.Property<string>("LanPartyPlace");

                    b.HasKey("LanPartyID");

                    b.ToTable("LanPartyConcept");
                });

            modelBuilder.Entity("PlannerLanParty.Models.LanPartyDate", b =>
                {
                    b.Property<int>("DateID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTimeFinish");

                    b.Property<DateTime>("DateTimeStart");

                    b.Property<int>("LanPartyID");

                    b.HasKey("DateID");

                    b.ToTable("LanPartyDates");
                });

            modelBuilder.Entity("PlannerLanParty.Models.LanPartyFinal", b =>
                {
                    b.Property<int>("LanPartyID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConceptPartyID");

                    b.Property<string>("LanPartyAddress");

                    b.Property<DateTime>("LanPartyFinalFinishDate");

                    b.Property<DateTime>("LanPartyFinalStartDate");

                    b.Property<string>("LanPartyName");

                    b.Property<string>("LanPartyPlace");

                    b.Property<int>("TournamentID");

                    b.HasKey("LanPartyID");

                    b.ToTable("LanParties");
                });

            modelBuilder.Entity("PlannerLanParty.Models.ParticipantsGames", b =>
                {
                    b.Property<int>("ParticipantsGamesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameName");

                    b.Property<string>("ParticipantID");

                    b.Property<int>("TournamentID");

                    b.HasKey("ParticipantsGamesID");

                    b.ToTable("ParticipantsGames");
                });

            modelBuilder.Entity("PlannerLanParty.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TournamentName");

                    b.HasKey("TournamentID");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("PlannerLanParty.Models.TournamentGames", b =>
                {
                    b.Property<int>("TournamentGamesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameName");

                    b.Property<int>("TournamentID");

                    b.HasKey("TournamentGamesID");

                    b.ToTable("TournamentGames");
                });

            modelBuilder.Entity("PlannerLanParty.Models.TournamentParticipants", b =>
                {
                    b.Property<int>("TournamentParticipantsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ParticipantID");

                    b.Property<int>("TournamentID");

                    b.HasKey("TournamentParticipantsID");

                    b.ToTable("TournamentParticipants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PlannerLanParty.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PlannerLanParty.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlannerLanParty.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PlannerLanParty.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlannerLanParty.Models.ApplicationUser", b =>
                {
                    b.HasOne("PlannerLanParty.Models.LanPartyConcept")
                        .WithMany("Attendees")
                        .HasForeignKey("LanPartyConceptLanPartyID");

                    b.HasOne("PlannerLanParty.Models.LanPartyFinal")
                        .WithMany("Attendees")
                        .HasForeignKey("LanPartyFinalLanPartyID");
                });
#pragma warning restore 612, 618
        }
    }
}
