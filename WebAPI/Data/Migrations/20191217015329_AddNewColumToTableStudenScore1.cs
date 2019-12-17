using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddNewColumToTableStudenScore1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AverageLevelEight",
                table: "StudentScores",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AverageLevelNine",
                table: "StudentScores",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AverageLevelSeven",
                table: "StudentScores",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AverageLevelSix",
                table: "StudentScores",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageLevelEight",
                table: "StudentScores");

            migrationBuilder.DropColumn(
                name: "AverageLevelNine",
                table: "StudentScores");

            migrationBuilder.DropColumn(
                name: "AverageLevelSeven",
                table: "StudentScores");

            migrationBuilder.DropColumn(
                name: "AverageLevelSix",
                table: "StudentScores");
        }
    }
}
