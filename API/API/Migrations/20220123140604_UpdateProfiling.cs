using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateProfiling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education_id",
                table: "TB_M_Profiling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Education_id",
                table: "TB_M_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
