using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addNewColumToTableStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageLink",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "imageLink",
                table: "Students");
        }
    }
}
