using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsMeeting.Server.Data.Migrations
{
    public partial class category_update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Meetings_MeetingId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_MeetingId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Meetings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CategoryId",
                table: "Meetings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Category_CategoryId",
                table: "Meetings",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Category_CategoryId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CategoryId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Meetings");

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_MeetingId",
                table: "Category",
                column: "MeetingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Meetings_MeetingId",
                table: "Category",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
