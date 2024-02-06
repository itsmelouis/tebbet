using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tebbet.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SnailParticipatingRace_Races_TableAId",
                table: "SnailParticipatingRace");

            migrationBuilder.DropForeignKey(
                name: "FK_SnailParticipatingRace_Snails_TableBId",
                table: "SnailParticipatingRace");

            migrationBuilder.DropIndex(
                name: "IX_SnailParticipatingRace_TableAId",
                table: "SnailParticipatingRace");

            migrationBuilder.DropIndex(
                name: "IX_SnailParticipatingRace_TableBId",
                table: "SnailParticipatingRace");

            migrationBuilder.DropColumn(
                name: "TableAId",
                table: "SnailParticipatingRace");

            migrationBuilder.DropColumn(
                name: "TableBId",
                table: "SnailParticipatingRace");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableAId",
                table: "SnailParticipatingRace",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TableBId",
                table: "SnailParticipatingRace",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SnailParticipatingRace_TableAId",
                table: "SnailParticipatingRace",
                column: "TableAId");

            migrationBuilder.CreateIndex(
                name: "IX_SnailParticipatingRace_TableBId",
                table: "SnailParticipatingRace",
                column: "TableBId");

            migrationBuilder.AddForeignKey(
                name: "FK_SnailParticipatingRace_Races_TableAId",
                table: "SnailParticipatingRace",
                column: "TableAId",
                principalTable: "Races",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SnailParticipatingRace_Snails_TableBId",
                table: "SnailParticipatingRace",
                column: "TableBId",
                principalTable: "Snails",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
