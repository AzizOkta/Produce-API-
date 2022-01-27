using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Education_Id",
                table: "TB_M_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "University_Id",
                table: "TB_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education_Id",
                table: "TB_M_Profiling");

            migrationBuilder.DropColumn(
                name: "University_Id",
                table: "TB_M_Education");
        }
    }
}
