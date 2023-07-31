using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lee.EMS.Migrations
{
    /// <inheritdoc />
    public partial class 模块表的visible类型调整 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Visible",
                table: "Sys_Module",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Visible",
                table: "Sys_Module",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
