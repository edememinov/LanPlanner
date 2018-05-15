using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PlannerLanParty.Migrations
{
    public partial class Initialmigrationnamefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TournantGames",
                table: "TournantGames");

            migrationBuilder.RenameTable(
                name: "TournantGames",
                newName: "TournamentGames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentGames",
                table: "TournamentGames",
                column: "TournamentGamesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentGames",
                table: "TournamentGames");

            migrationBuilder.RenameTable(
                name: "TournamentGames",
                newName: "TournantGames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournantGames",
                table: "TournantGames",
                column: "TournamentGamesID");
        }
    }
}
