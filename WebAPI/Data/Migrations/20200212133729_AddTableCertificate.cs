using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddTableCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubjectCareer = table.Column<string>(nullable: true),
                    CertificateType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CertificateId",
                table: "Students",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Certificates_CertificateId",
                table: "Students",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Certificates_CertificateId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Students_CertificateId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "Certificate",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }
    }
}
