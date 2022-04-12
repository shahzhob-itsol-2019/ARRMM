using Microsoft.EntityFrameworkCore.Migrations;

namespace ARRMM.Migrations
{
    public partial class RenameOffencesWithReasonInLandSurrenderAndTransferTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Offences",
                table: "LandSurrendersAndTransfers",
                newName: "Reason");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "LandSurrendersAndTransfers",
                newName: "Offences");
        }
    }
}
