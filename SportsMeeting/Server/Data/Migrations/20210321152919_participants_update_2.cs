using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsMeeting.Server.Data.Migrations
{
    public partial class participants_update_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Conversations_ConversationId",
                table: "Participants");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationId",
                table: "Participants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Conversations_ConversationId",
                table: "Participants",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Conversations_ConversationId",
                table: "Participants");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Conversations_ConversationId",
                table: "Participants",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
