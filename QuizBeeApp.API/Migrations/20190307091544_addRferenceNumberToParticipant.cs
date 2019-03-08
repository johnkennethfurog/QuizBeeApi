using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizBeeApp.API.Migrations
{
    public partial class addRferenceNumberToParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Participants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Participants");
        }
    }
}
