using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsMeeting.Server.Data.Migrations
{
    public partial class participants_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_AspNetUsers_UserId1",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Participants",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_UserId1",
                table: "Participants",
                newName: "IX_Participants_UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_AspNetUsers_UserEmail",
                table: "Participants",
                column: "UserEmail",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_AspNetUsers_UserEmail",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Participants",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_UserEmail",
                table: "Participants",
                newName: "IX_Participants_UserId1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_AspNetUsers_UserId1",
                table: "Participants",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
