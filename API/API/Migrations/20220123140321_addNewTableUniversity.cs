using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addNewTableUniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Educationid",
                table: "TB_M_Profiling",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_University",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_University", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Education",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Universityid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Education", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_M_Education_TB_M_University_Universityid",
                        column: x => x.Universityid,
                        principalTable: "TB_M_University",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Profiling_Educationid",
                table: "TB_M_Profiling",
                column: "Educationid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_Universityid",
                table: "TB_M_Education",
                column: "Universityid");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Educationid",
                table: "TB_M_Profiling",
                column: "Educationid",
                principalTable: "TB_M_Education",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Educationid",
                table: "TB_M_Profiling");

            migrationBuilder.DropTable(
                name: "TB_M_Education");

            migrationBuilder.DropTable(
                name: "TB_M_University");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Profiling_Educationid",
                table: "TB_M_Profiling");

            migrationBuilder.DropColumn(
                name: "Educationid",
                table: "TB_M_Profiling");
        }
    }
}
