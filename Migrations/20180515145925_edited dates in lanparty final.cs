using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PlannerLanParty.Migrations
{
    public partial class editeddatesinlanpartyfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanPartyFinalDate",
                table: "LanParties",
                newName: "LanPartyFinalStartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LanPartyFinalFinishDate",
                table: "LanParties",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanPartyFinalFinishDate",
                table: "LanParties");

            migrationBuilder.RenameColumn(
                name: "LanPartyFinalStartDate",
                table: "LanParties",
                newName: "LanPartyFinalDate");
        }
    }
}
