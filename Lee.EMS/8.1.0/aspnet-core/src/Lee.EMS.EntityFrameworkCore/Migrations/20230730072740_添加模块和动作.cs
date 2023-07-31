using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lee.EMS.Migrations
{
    /// <inheritdoc />
    public partial class 添加模块和动作 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Action",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Sort = table.Column<long>(type: "bigint", nullable: false),
                    Visible = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    MethodCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Module",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Sort = table.Column<long>(type: "bigint", nullable: false),
                    Visible = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    PageRoute = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Describe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ModuleActionInRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    ActionId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ModuleActionInRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_ModuleActionInRole_RoleId_ModuleId_ActionId",
                table: "Sys_ModuleActionInRole",
                columns: new[] { "RoleId", "ModuleId", "ActionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Action");

            migrationBuilder.DropTable(
                name: "Sys_Module");

            migrationBuilder.DropTable(
                name: "Sys_ModuleActionInRole");
        }
    }
}
