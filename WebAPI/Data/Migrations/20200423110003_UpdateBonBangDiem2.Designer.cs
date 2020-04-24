﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200423110003_UpdateBonBangDiem2")]
    partial class UpdateBonBangDiem2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.DetailDiscipline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("DatePunish");

                    b.Property<Guid>("DisciplineId");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Punishment");

                    b.Property<string>("Reason");

                    b.Property<bool>("Status");

                    b.Property<Guid>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentId");

                    b.ToTable("DetailDisciplines");
                });

            modelBuilder.Entity("Data.Entities.DetailReward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("DateReward");

                    b.Property<string>("Gift");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Reason");

                    b.Property<Guid>("RewardId");

                    b.Property<bool>("Status");

                    b.Property<Guid>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("RewardId");

                    b.HasIndex("StudentId");

                    b.ToTable("DetailRewards");
                });

            modelBuilder.Entity("Data.Entities.DiemLopBay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AmNhac");

                    b.Property<float>("CongNghe");

                    b.Property<float>("DiaLy");

                    b.Property<float>("DiemTrungBinhCong");

                    b.Property<float>("GiaoDucCongDan");

                    b.Property<float>("LichSu");

                    b.Property<float>("MyThuat");

                    b.Property<float>("NgoaiNgu");

                    b.Property<float>("NguVan");

                    b.Property<float>("SinhHoc");

                    b.Property<Guid>("StudentId");

                    b.Property<float>("Tin");

                    b.Property<float>("Toan");

                    b.Property<float>("VatLy");

                    b.Property<int>("loai");

                    b.HasKey("Id");

                    b.ToTable("DiemLopBays");
                });

            modelBuilder.Entity("Data.Entities.DiemLopChin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AmNhac");

                    b.Property<float>("CongNghe");

                    b.Property<float>("DiaLy");

                    b.Property<float>("DiemTrungBinhCong");

                    b.Property<float>("GiaoDucCongDan");

                    b.Property<float>("LichSu");

                    b.Property<float>("MyThuat");

                    b.Property<float>("NgoaiNgu");

                    b.Property<float>("NguVan");

                    b.Property<float>("SinhHoc");

                    b.Property<Guid>("StudentId");

                    b.Property<float>("Tin");

                    b.Property<float>("Toan");

                    b.Property<float>("VatLy");

                    b.Property<int>("loai");

                    b.HasKey("Id");

                    b.ToTable("DiemLopChins");
                });

            modelBuilder.Entity("Data.Entities.DiemLopSau", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AmNhac");

                    b.Property<float>("CongNghe");

                    b.Property<float>("DiaLy");

                    b.Property<float>("DiemTrungBinhCong");

                    b.Property<float>("GiaoDucCongDan");

                    b.Property<float>("LichSu");

                    b.Property<float>("MyThuat");

                    b.Property<float>("NgoaiNgu");

                    b.Property<float>("NguVan");

                    b.Property<float>("SinhHoc");

                    b.Property<Guid>("StudentId");

                    b.Property<float>("Tin");

                    b.Property<float>("Toan");

                    b.Property<float>("VatLy");

                    b.Property<int>("loai");

                    b.HasKey("Id");

                    b.ToTable("DiemLopSaus");
                });

            modelBuilder.Entity("Data.Entities.DiemLopTam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AmNhac");

                    b.Property<float>("CongNghe");

                    b.Property<float>("DiaLy");

                    b.Property<float>("DiemTrungBinhCong");

                    b.Property<float>("GiaoDucCongDan");

                    b.Property<float>("LichSu");

                    b.Property<float>("MyThuat");

                    b.Property<float>("NgoaiNgu");

                    b.Property<float>("NguVan");

                    b.Property<float>("SinhHoc");

                    b.Property<Guid>("StudentId");

                    b.Property<float>("Tin");

                    b.Property<float>("Toan");

                    b.Property<float>("VatLy");

                    b.Property<int>("loai");

                    b.HasKey("Id");

                    b.ToTable("DiemLopTams");
                });

            modelBuilder.Entity("Data.Entities.Discipline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Number");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("Data.Entities.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.Property<int>("levelEnum");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Data.Entities.Reward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Number");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("Data.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Data.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BirthLocate");

                    b.Property<string>("Birthday");

                    b.Property<string>("Code");

                    b.Property<string>("CreatedDate");

                    b.Property<string>("DateGoShcool");

                    b.Property<Guid>("GradeId");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Sex");

                    b.Property<bool>("Status");

                    b.Property<string>("Talent");

                    b.Property<string>("address");

                    b.Property<string>("imageLink");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Data.Entities.StudentScore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AverageLevelEight");

                    b.Property<float>("AverageLevelNine");

                    b.Property<float>("AverageLevelSeven");

                    b.Property<float>("AverageLevelSix");

                    b.Property<string>("CreatedDate");

                    b.Property<string>("ModifiedDate");

                    b.Property<float>("SemesterOneLevelEight");

                    b.Property<float>("SemesterOneLevelNine");

                    b.Property<float>("SemesterOneLevelSeven");

                    b.Property<float>("SemesterOneLevelSix");

                    b.Property<float>("SemesterTwoLevelEight");

                    b.Property<float>("SemesterTwoLevelNine");

                    b.Property<float>("SemesterTwoLevelSeven");

                    b.Property<float>("SemesterTwoLevelSix");

                    b.Property<bool>("Status");

                    b.Property<Guid>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentScores");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("Avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedDate");

                    b.Property<string>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool?>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Data.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Data.Entities.DetailDiscipline", b =>
                {
                    b.HasOne("Data.Entities.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.DetailReward", b =>
                {
                    b.HasOne("Data.Entities.Reward", "Reward")
                        .WithMany()
                        .HasForeignKey("RewardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.Student", b =>
                {
                    b.HasOne("Data.Entities.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.StudentScore", b =>
                {
                    b.HasOne("Data.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.UserRole", b =>
                {
                    b.HasOne("Data.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Data.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
