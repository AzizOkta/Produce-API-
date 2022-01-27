using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTableProfiling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Profiling_TB_M_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Profiling");
        }
    }
}
