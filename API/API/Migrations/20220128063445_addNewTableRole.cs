using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addNewTableRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_AccountRole",
                columns: table => new
                {
                    Id_Role = table.Column<int>(type: "int", nullable: false),
                    Id_Account = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_AccountRole", x => new { x.Id_Account, x.Id_Role });
                    table.ForeignKey(
                        name: "FK_TB_M_AccountRole_TB_M_Account_Id_Account",
                        column: x => x.Id_Account,
                        principalTable: "TB_M_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_AccountRole_TB_M_Role_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "TB_M_Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_AccountRole_Id_Role",
                table: "TB_M_AccountRole",
                column: "Id_Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_AccountRole");

            migrationBuilder.DropTable(
                name: "TB_M_Role");
        }
    }
}
