using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportFixtures.Data.Access.Migrations
{
    public partial class Testmigrationwithchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_Teams_Team1Id",
                table: "Encounters");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_Teams_Team2Id",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_Team1Id",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_Team2Id",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "Team1Id",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "Team2Id",
                table: "Encounters");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Teams",
                newName: "Photo");

            migrationBuilder.AddColumn<int>(
                name: "EncounterId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EncounterMode",
                table: "Sports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PositionInEncounter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    EncounterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionInEncounter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionInEncounter_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_EncounterId",
                table: "Teams",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionInEncounter_EncounterId",
                table: "PositionInEncounter",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Encounters_EncounterId",
                table: "Teams",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Encounters_EncounterId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "PositionInEncounter");

            migrationBuilder.DropIndex(
                name: "IX_Teams_EncounterId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EncounterMode",
                table: "Sports");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Teams",
                newName: "PhotoPath");

            migrationBuilder.AddColumn<int>(
                name: "Team1Id",
                table: "Encounters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team2Id",
                table: "Encounters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_Team1Id",
                table: "Encounters",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_Team2Id",
                table: "Encounters",
                column: "Team2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_Teams_Team1Id",
                table: "Encounters",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_Teams_Team2Id",
                table: "Encounters",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
