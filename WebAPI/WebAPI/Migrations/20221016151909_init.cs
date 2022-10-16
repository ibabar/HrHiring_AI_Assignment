using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Resume = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    VacancyID = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: true),
                    ApprovalOne = table.Column<long>(type: "bigint", nullable: true),
                    ApprovalTwo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_UserLogin",
                        column: x => x.ApprovalOne,
                        principalTable: "UserLogin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidate_UserLogin1",
                        column: x => x.ApprovalTwo,
                        principalTable: "UserLogin",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenPosition = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ApprovedCandidate = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancy_Candidate",
                        column: x => x.ApprovedCandidate,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[] { 1L, "123456", "manager", "hrm" });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[] { 2L, "123456", "officer", "hro" });

            migrationBuilder.InsertData(
                table: "UserLogin",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[] { 3L, "123456", "director", "hrd" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ApprovalOne",
                table: "Candidate",
                column: "ApprovalOne");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ApprovalTwo",
                table: "Candidate",
                column: "ApprovalTwo");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_ApprovedCandidate",
                table: "Vacancy",
                column: "ApprovedCandidate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacancy");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
