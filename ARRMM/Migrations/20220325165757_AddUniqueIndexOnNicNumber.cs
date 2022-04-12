using Microsoft.EntityFrameworkCore.Migrations;

namespace ARRMM.Migrations
{
    public partial class AddUniqueIndexOnNicNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NicNumber",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NicNumber",
                table: "AspNetUsers",
                column: "NicNumber",
                unique: true,
                filter: "[NicNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NicNumber",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "NicNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
