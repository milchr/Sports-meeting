using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsMeeting.Server.Data.Migrations
{
    public partial class meeting_update_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meetings_UserName",
                table: "Meetings");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_UserName",
                table: "Meetings",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meetings_UserName",
                table: "Meetings");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_UserName",
                table: "Meetings",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }
    }
}
