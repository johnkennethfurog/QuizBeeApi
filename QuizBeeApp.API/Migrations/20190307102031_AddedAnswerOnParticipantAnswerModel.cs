using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizBeeApp.API.Migrations
{
    public partial class AddedAnswerOnParticipantAnswerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "ParticipantAnswers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "ParticipantAnswers");
        }
    }
}
