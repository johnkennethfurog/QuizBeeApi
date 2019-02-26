using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizBeeApp.API.Migrations
{
    public partial class ChangesTypeToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizItems_QuestionTypes_TypeId",
                table: "QuizItems");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "QuizItems");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "QuizItems",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizItems_TypeId",
                table: "QuizItems",
                newName: "IX_QuizItems_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "QuizItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DefaultTimeLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizItems_QuestionCategories_CategoryId",
                table: "QuizItems",
                column: "CategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizItems_QuestionCategories_CategoryId",
                table: "QuizItems");

            migrationBuilder.DropTable(
                name: "QuestionCategories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "QuizItems");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "QuizItems",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizItems_CategoryId",
                table: "QuizItems",
                newName: "IX_QuizItems_TypeId");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "QuizItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DefaultTimeLimit = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizItems_QuestionTypes_TypeId",
                table: "QuizItems",
                column: "TypeId",
                principalTable: "QuestionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
