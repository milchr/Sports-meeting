using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsMeeting.Server.Data.Migrations
{
    public partial class category_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_MeetingId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Meetings");

            migrationBuilder.CreateIndex(
                name: "IX_Category_MeetingId",
                table: "Category",
                column: "MeetingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_MeetingId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_MeetingId",
                table: "Category",
                column: "MeetingId");
        }
    }
}
