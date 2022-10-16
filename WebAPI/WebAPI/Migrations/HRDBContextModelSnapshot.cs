﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Model;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(HRDBContext))]
    partial class HRDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPI.Model.Candidate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int?>("ApplicationStatus")
                        .HasColumnType("int");

                    b.Property<long?>("ApprovalOne")
                        .HasColumnType("bigint");

                    b.Property<long?>("ApprovalTwo")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MobileNumber")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Resume")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<long>("VacancyId")
                        .HasColumnType("bigint")
                        .HasColumnName("VacancyID");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalOne");

                    b.HasIndex("ApprovalTwo");

                    b.ToTable("Candidate", (string)null);
                });

            modelBuilder.Entity("WebAPI.Model.UserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("UserLogin", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Password = "123456",
                            Role = "manager",
                            UserName = "hrm"
                        },
                        new
                        {
                            Id = 2L,
                            Password = "123456",
                            Role = "officer",
                            UserName = "hro"
                        },
                        new
                        {
                            Id = 3L,
                            Password = "123456",
                            Role = "director",
                            UserName = "hrd"
                        });
                });

            modelBuilder.Entity("WebAPI.Model.Vacancy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("ApprovedCandidate")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("OpenPosition")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ApprovedCandidate" }, "IX_Vacancy_ApprovedCandidate");

                    b.ToTable("Vacancy", (string)null);
                });

            modelBuilder.Entity("WebAPI.Model.Candidate", b =>
                {
                    b.HasOne("WebAPI.Model.UserLogin", "ApprovalOneNavigation")
                        .WithMany("CandidateApprovalOneNavigations")
                        .HasForeignKey("ApprovalOne")
                        .HasConstraintName("FK_Candidate_UserLogin");

                    b.HasOne("WebAPI.Model.UserLogin", "ApprovalTwoNavigation")
                        .WithMany("CandidateApprovalTwoNavigations")
                        .HasForeignKey("ApprovalTwo")
                        .HasConstraintName("FK_Candidate_UserLogin1");

                    b.Navigation("ApprovalOneNavigation");

                    b.Navigation("ApprovalTwoNavigation");
                });

            modelBuilder.Entity("WebAPI.Model.Vacancy", b =>
                {
                    b.HasOne("WebAPI.Model.Candidate", "ApprovedCandidateNavigation")
                        .WithMany("Vacancies")
                        .HasForeignKey("ApprovedCandidate")
                        .HasConstraintName("FK_Vacancy_Candidate");

                    b.Navigation("ApprovedCandidateNavigation");
                });

            modelBuilder.Entity("WebAPI.Model.Candidate", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("WebAPI.Model.UserLogin", b =>
                {
                    b.Navigation("CandidateApprovalOneNavigations");

                    b.Navigation("CandidateApprovalTwoNavigations");
                });
#pragma warning restore 612, 618
        }
    }
}