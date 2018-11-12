using Microsoft.EntityFrameworkCore.Migrations;

namespace SportFixtures.Data.Access.Migrations
{
    public partial class AddedToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Encounters_EncounterId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_EncounterId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "EncountersTeams",
                columns: table => new
                {
                    EncounterId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncountersTeams", x => new { x.EncounterId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_EncountersTeams_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncountersTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncountersTeams_TeamId",
                table: "EncountersTeams",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncountersTeams");

            migrationBuilder.AddColumn<int>(
                name: "EncounterId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_EncounterId",
                table: "Teams",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Encounters_EncounterId",
                table: "Teams",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
