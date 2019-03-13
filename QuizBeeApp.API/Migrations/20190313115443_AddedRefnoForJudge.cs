using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizBeeApp.API.Migrations
{
    public partial class AddedRefnoForJudge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefNo",
                table: "Judges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefNo",
                table: "Judges");
        }
    }
}
