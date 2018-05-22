using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PlannerLanParty.Migrations
{
    public partial class Addedroundsandwinners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Round",
                table: "Round");

            migrationBuilder.RenameTable(
                name: "Round",
                newName: "Rounds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds",
                column: "RoundID");

            migrationBuilder.CreateTable(
                name: "RoundParticipants",
                columns: table => new
                {
                    RoundParticipantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParticipantID = table.Column<int>(nullable: false),
                    ParticipantScore = table.Column<int>(nullable: false),
                    RoundID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundParticipants", x => x.RoundParticipantID);
                });

            migrationBuilder.CreateTable(
                name: "RoundWinners",
                columns: table => new
                {
                    RoundWinnerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FifthPlace = table.Column<int>(nullable: false),
                    FirstPlace = table.Column<int>(nullable: false),
                    FourthPlace = table.Column<int>(nullable: false),
                    SecondPlace = table.Column<int>(nullable: false),
                    ThirdPlace = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundWinners", x => x.RoundWinnerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoundParticipants");

            migrationBuilder.DropTable(
                name: "RoundWinners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds");

            migrationBuilder.RenameTable(
                name: "Rounds",
                newName: "Round");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Round",
                table: "Round",
                column: "RoundID");
        }
    }
}
