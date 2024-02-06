using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tebbet.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnailParticipatingRace",
                columns: table => new
                {
                    RacesId = table.Column<int>(type: "integer", nullable: false),
                    SnailsId = table.Column<int>(type: "integer", nullable: false),
                    TableAId = table.Column<int>(type: "integer", nullable: false),
                    TableBId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnailParticipatingRace", x => new { x.RacesId, x.SnailsId });
                    table.ForeignKey(
                        name: "FK_SnailParticipatingRace_Races_TableAId",
                        column: x => x.TableAId,
                        principalTable: "Races",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnailParticipatingRace_Snails_TableBId",
                        column: x => x.TableBId,
                        principalTable: "Snails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnailParticipatingRace_TableAId",
                table: "SnailParticipatingRace",
                column: "TableAId");

            migrationBuilder.CreateIndex(
                name: "IX_SnailParticipatingRace_TableBId",
                table: "SnailParticipatingRace",
                column: "TableBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnailParticipatingRace");
        }
    }
}
