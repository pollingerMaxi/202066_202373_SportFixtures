using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportFixtures.Data.Access.Migrations
{
    public partial class Updatedmodeltosatisfynewrequirements : Migration
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
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(nullable: true),
                    points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Score",
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
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Encounters_EncounterId",
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
                name: "IX_Positions_TeamId",
                table: "Positions",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_EncounterId",
                table: "Score",
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
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Teams_EncounterId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "EncounterMode",
                table: "Sports");

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
