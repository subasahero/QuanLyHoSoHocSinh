using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateBonBangDiem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiemLopBays",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Toan = table.Column<float>(nullable: false),
                    NguVan = table.Column<float>(nullable: false),
                    NgoaiNgu = table.Column<float>(nullable: false),
                    Tin = table.Column<float>(nullable: false),
                    LichSu = table.Column<float>(nullable: false),
                    DiaLy = table.Column<float>(nullable: false),
                    GiaoDucCongDan = table.Column<float>(nullable: false),
                    CongNghe = table.Column<float>(nullable: false),
                    VatLy = table.Column<float>(nullable: false),
                    SinhHoc = table.Column<float>(nullable: false),
                    AmNhac = table.Column<float>(nullable: false),
                    MyThuat = table.Column<float>(nullable: false),
                    DiemTrungBinhCong = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemLopBays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiemLopChins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Toan = table.Column<float>(nullable: false),
                    NguVan = table.Column<float>(nullable: false),
                    NgoaiNgu = table.Column<float>(nullable: false),
                    Tin = table.Column<float>(nullable: false),
                    LichSu = table.Column<float>(nullable: false),
                    DiaLy = table.Column<float>(nullable: false),
                    GiaoDucCongDan = table.Column<float>(nullable: false),
                    CongNghe = table.Column<float>(nullable: false),
                    VatLy = table.Column<float>(nullable: false),
                    SinhHoc = table.Column<float>(nullable: false),
                    AmNhac = table.Column<float>(nullable: false),
                    MyThuat = table.Column<float>(nullable: false),
                    DiemTrungBinhCong = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemLopChins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiemLopSaus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Toan = table.Column<float>(nullable: false),
                    NguVan = table.Column<float>(nullable: false),
                    NgoaiNgu = table.Column<float>(nullable: false),
                    Tin = table.Column<float>(nullable: false),
                    LichSu = table.Column<float>(nullable: false),
                    DiaLy = table.Column<float>(nullable: false),
                    GiaoDucCongDan = table.Column<float>(nullable: false),
                    CongNghe = table.Column<float>(nullable: false),
                    VatLy = table.Column<float>(nullable: false),
                    SinhHoc = table.Column<float>(nullable: false),
                    AmNhac = table.Column<float>(nullable: false),
                    MyThuat = table.Column<float>(nullable: false),
                    DiemTrungBinhCong = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemLopSaus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiemLopTams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    Toan = table.Column<float>(nullable: false),
                    NguVan = table.Column<float>(nullable: false),
                    NgoaiNgu = table.Column<float>(nullable: false),
                    Tin = table.Column<float>(nullable: false),
                    LichSu = table.Column<float>(nullable: false),
                    DiaLy = table.Column<float>(nullable: false),
                    GiaoDucCongDan = table.Column<float>(nullable: false),
                    CongNghe = table.Column<float>(nullable: false),
                    VatLy = table.Column<float>(nullable: false),
                    SinhHoc = table.Column<float>(nullable: false),
                    AmNhac = table.Column<float>(nullable: false),
                    MyThuat = table.Column<float>(nullable: false),
                    DiemTrungBinhCong = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemLopTams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiemLopBays");

            migrationBuilder.DropTable(
                name: "DiemLopChins");

            migrationBuilder.DropTable(
                name: "DiemLopSaus");

            migrationBuilder.DropTable(
                name: "DiemLopTams");
        }
    }
}
