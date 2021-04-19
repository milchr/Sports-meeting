using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsMeeting.Server.Data.Migrations
{
    public partial class conversation_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Meetings_MeetingId",
                table: "Conversations");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Meetings_MeetingId",
                table: "Conversations",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Meetings_MeetingId",
                table: "Conversations");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Meetings_MeetingId",
                table: "Conversations",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
