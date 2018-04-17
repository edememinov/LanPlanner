using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PlannerLanParty.Migrations
{
    public partial class Addedatable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanPartyDates_LanPartyConcept_LanPartyID",
                table: "LanPartyDates");

            migrationBuilder.DropIndex(
                name: "IX_LanPartyDates_LanPartyID",
                table: "LanPartyDates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LanPartyDates_LanPartyID",
                table: "LanPartyDates",
                column: "LanPartyID");

            migrationBuilder.AddForeignKey(
                name: "FK_LanPartyDates_LanPartyConcept_LanPartyID",
                table: "LanPartyDates",
                column: "LanPartyID",
                principalTable: "LanPartyConcept",
                principalColumn: "LanPartyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
