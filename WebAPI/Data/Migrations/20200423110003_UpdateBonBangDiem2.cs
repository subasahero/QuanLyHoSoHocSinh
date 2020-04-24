using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateBonBangDiem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loai",
                table: "DiemLopTams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "loai",
                table: "DiemLopSaus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "loai",
                table: "DiemLopChins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "loai",
                table: "DiemLopBays",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loai",
                table: "DiemLopTams");

            migrationBuilder.DropColumn(
                name: "loai",
                table: "DiemLopSaus");

            migrationBuilder.DropColumn(
                name: "loai",
                table: "DiemLopChins");

            migrationBuilder.DropColumn(
                name: "loai",
                table: "DiemLopBays");
        }
    }
}
