using Microsoft.EntityFrameworkCore.Migrations;

namespace ARRMM.Migrations
{
    public partial class MakePreviousStatusIdsNullableInLandSurrenderAndTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers",
                column: "PreviousApplicationStatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers",
                column: "PreviousOperationStatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers",
                column: "PreviousApplicationStatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LandSurrendersAndTransfers_Statuses_PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers",
                column: "PreviousOperationStatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
