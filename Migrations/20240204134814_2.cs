using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tebbet.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Circuits_id",
                table: "Races");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Races",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CircuitId",
                table: "Races",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Races_CircuitId",
                table: "Races",
                column: "CircuitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Circuits_CircuitId",
                table: "Races",
                column: "CircuitId",
                principalTable: "Circuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Circuits_CircuitId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_CircuitId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "CircuitId",
                table: "Races");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Races",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Circuits_id",
                table: "Races",
                column: "id",
                principalTable: "Circuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
