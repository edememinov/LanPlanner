using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PlannerLanParty.Migrations
{
    public partial class Addedfinallan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FinalCheck",
                table: "LanPartyDates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ConceptPartyID",
                table: "LanParties",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalCheck",
                table: "LanPartyDates");

            migrationBuilder.DropColumn(
                name: "ConceptPartyID",
                table: "LanParties");
        }
    }
}
