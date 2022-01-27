using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addNewForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_Universityid",
                table: "TB_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Educationid",
                table: "TB_M_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Profiling_Educationid",
                table: "TB_M_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_Universityid",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "Educationid",
                table: "TB_M_Profiling");

            migrationBuilder.DropColumn(
                name: "Universityid",
                table: "TB_M_Education");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Profiling_Education_Id",
                table: "TB_M_Profiling",
                column: "Education_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_University_Id",
                table: "TB_M_Education",
                column: "University_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_M_University_University_Id",
                table: "TB_M_Education",
                column: "University_Id",
                principalTable: "TB_M_University",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Education_Id",
                table: "TB_M_Profiling",
                column: "Education_Id",
                principalTable: "TB_M_Education",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_University_Id",
                table: "TB_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Education_Id",
                table: "TB_M_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Profiling_Education_Id",
                table: "TB_M_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_University_Id",
                table: "TB_M_Education");

            migrationBuilder.AddColumn<int>(
                name: "Educationid",
                table: "TB_M_Profiling",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Universityid",
                table: "TB_M_Education",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Profiling_Educationid",
                table: "TB_M_Profiling",
                column: "Educationid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_Universityid",
                table: "TB_M_Education",
                column: "Universityid");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_M_University_Universityid",
                table: "TB_M_Education",
                column: "Universityid",
                principalTable: "TB_M_University",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Educationid",
                table: "TB_M_Profiling",
                column: "Educationid",
                principalTable: "TB_M_Education",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
