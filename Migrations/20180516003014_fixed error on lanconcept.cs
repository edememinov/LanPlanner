using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PlannerLanParty.Migrations
{
    public partial class fixederroronlanconcept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalCheck",
                table: "LanPartyDates");

            migrationBuilder.AddColumn<bool>(
                name: "FinalCheck",
                table: "LanPartyConcept",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalCheck",
                table: "LanPartyConcept");

            migrationBuilder.AddColumn<bool>(
                name: "FinalCheck",
                table: "LanPartyDates",
                nullable: false,
                defaultValue: false);
        }
    }
}
