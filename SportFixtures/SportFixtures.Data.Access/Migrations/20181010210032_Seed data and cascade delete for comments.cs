using Microsoft.EntityFrameworkCore.Migrations;

namespace SportFixtures.Data.Access.Migrations
{
    public partial class Seeddataandcascadedeleteforcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Encounters_EncounterId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_EncounterId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Role", "Token", "Username" },
                values: new object[] { 1, "admin@admin.com", "Admins LastName", "Admins Name", "admin", 1, null, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Role", "Token", "Username" },
                values: new object[] { 2, "user@user.com", "Users LastName", "Normal user", "user", 0, null, "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Encounters_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Encounters_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EncounterId",
                table: "Comments",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Encounters_EncounterId",
                table: "Comments",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
