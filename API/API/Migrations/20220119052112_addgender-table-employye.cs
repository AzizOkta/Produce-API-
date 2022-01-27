
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addgendertableemployye : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "TB_M_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "TB_M_Employee");
        }
    }
}
