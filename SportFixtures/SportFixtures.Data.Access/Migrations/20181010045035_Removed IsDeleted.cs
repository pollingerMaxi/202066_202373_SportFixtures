using Microsoft.EntityFrameworkCore.Migrations;

namespace SportFixtures.Data.Access.Migrations
{
    public partial class RemovedIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sports",
                nullable: false,
                defaultValue: false);
        }
    }
}
