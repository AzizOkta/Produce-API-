using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateNameEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "TB_M_Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Person",
                table: "TB_M_Person",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Person",
                table: "TB_M_Person");

            migrationBuilder.RenameTable(
                name: "TB_M_Person",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "NIK");
        }
    }
}
