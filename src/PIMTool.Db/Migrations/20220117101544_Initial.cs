using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PIMTool.Db.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pimtool");

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                schema: "pimtool",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Visa = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    RowVersion = table.Column<int>(type: "int", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GROUP",
                schema: "pimtool",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaderId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<int>(type: "int", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GROUP_EMPLOYEE_LeaderId",
                        column: x => x.LeaderId,
                        principalSchema: "pimtool",
                        principalTable: "EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PROJECT",
                schema: "pimtool",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectNumber = table.Column<short>(type: "smallint", fixedLength: true, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "char(3)", fixedLength: true, maxLength: 3, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    GroupdId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<int>(type: "int", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROJECT_GROUP_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "pimtool",
                        principalTable: "GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PROJECT_EMPLOYEE",
                schema: "pimtool",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT_EMPLOYEE", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_PROJECT_EMPLOYEE_EMPLOYEE_EmployeesId",
                        column: x => x.EmployeesId,
                        principalSchema: "pimtool",
                        principalTable: "EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROJECT_EMPLOYEE_PROJECT_ProjectsId",
                        column: x => x.ProjectsId,
                        principalSchema: "pimtool",
                        principalTable: "PROJECT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GROUP_LeaderId",
                schema: "pimtool",
                table: "GROUP",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_GroupId",
                schema: "pimtool",
                table: "PROJECT",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_EMPLOYEE_ProjectsId",
                schema: "pimtool",
                table: "PROJECT_EMPLOYEE",
                column: "ProjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROJECT_EMPLOYEE",
                schema: "pimtool");

            migrationBuilder.DropTable(
                name: "PROJECT",
                schema: "pimtool");

            migrationBuilder.DropTable(
                name: "GROUP",
                schema: "pimtool");

            migrationBuilder.DropTable(
                name: "EMPLOYEE",
                schema: "pimtool");
        }
    }
}
