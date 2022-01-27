using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateNewNameEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Person",
                table: "TB_M_Person");

            migrationBuilder.RenameTable(
                name: "TB_M_Person",
                newName: "TB_M_Employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Employee",
                table: "TB_M_Employee",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Employee",
                table: "TB_M_Employee");

            migrationBuilder.RenameTable(
                name: "TB_M_Employee",
                newName: "TB_M_Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Person",
                table: "TB_M_Person",
                column: "NIK");
        }
    }
}
