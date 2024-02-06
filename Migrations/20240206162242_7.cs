using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tebbet.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    SnailId = table.Column<int>(type: "integer", nullable: false),
                    Odds = table.Column<double>(type: "double precision", nullable: false),
                    Gains = table.Column<double>(type: "double precision", nullable: false),
                    Has_Won = table.Column<bool>(type: "boolean", nullable: false),
                    Date_Bet = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => new { x.UserId, x.RaceId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.CreateTable(
                name: "UserBets",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    Date_Bet = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gains = table.Column<double>(type: "double precision", nullable: false),
                    Has_Won = table.Column<bool>(type: "boolean", nullable: false),
                    Odds = table.Column<double>(type: "double precision", nullable: false),
                    SnailId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBets", x => new { x.UserId, x.RaceId });
                });
        }
    }
}
