using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARRMM.Migrations
{
    public partial class AddApplicationTablesForFormAtoFormK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ScaleType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SendBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SendTo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GrantedOrRefusedBy = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_GrantedOrRefusedBy",
                        column: x => x.GrantedOrRefusedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_SendBy",
                        column: x => x.SendBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_SendTo",
                        column: x => x.SendTo,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationMinerals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    MineralId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMinerals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationMinerals_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationMinerals_Minerals_MineralId",
                        column: x => x.MineralId,
                        principalTable: "Minerals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Explorations",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessNature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    HasShareCapital = table.Column<bool>(type: "bit", nullable: false),
                    LandAreaSize = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    NumberOfYears = table.Column<int>(type: "int", nullable: false),
                    PreviousApplicationStatusId = table.Column<int>(type: "int", nullable: false),
                    PreviousOperationStatusId = table.Column<int>(type: "int", nullable: false),
                    Offences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explorations", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Explorations_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Explorations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Explorations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Explorations_Statuses_PreviousApplicationStatusId",
                        column: x => x.PreviousApplicationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Explorations_Statuses_PreviousOperationStatusId",
                        column: x => x.PreviousOperationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LandSurrendersAndTransfers",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredMineralTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssigneeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurrenderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LandAreaSize = table.Column<int>(type: "int", nullable: true),
                    ParticularDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousApplicationStatusId = table.Column<int>(type: "int", nullable: false),
                    PreviousOperationStatusId = table.Column<int>(type: "int", nullable: false),
                    Offences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandSurrendersAndTransfers", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_LandSurrendersAndTransfers_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandSurrendersAndTransfers_Statuses_PreviousApplicationStatusId",
                        column: x => x.PreviousApplicationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LandSurrendersAndTransfers_Statuses_PreviousOperationStatusId",
                        column: x => x.PreviousOperationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LargeMiningLeases",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessNature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    HasShareCapital = table.Column<bool>(type: "bit", nullable: false),
                    LandAreaSize = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    NumberOfYears = table.Column<int>(type: "int", nullable: false),
                    PreviousApplicationStatusId = table.Column<int>(type: "int", nullable: false),
                    PreviousOperationStatusId = table.Column<int>(type: "int", nullable: false),
                    Offences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LargeMiningLeases", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_LargeMiningLeases_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LargeMiningLeases_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LargeMiningLeases_Statuses_PreviousApplicationStatusId",
                        column: x => x.PreviousApplicationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LargeMiningLeases_Statuses_PreviousOperationStatusId",
                        column: x => x.PreviousOperationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MineralDepositRetentions",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessNature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    HasShareCapital = table.Column<bool>(type: "bit", nullable: false),
                    LandAreaSize = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    NumberOfYears = table.Column<int>(type: "int", nullable: false),
                    PreviousApplicationStatusId = table.Column<int>(type: "int", nullable: false),
                    PreviousOperationStatusId = table.Column<int>(type: "int", nullable: false),
                    ClaimReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedMiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatingPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatingCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangesDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MineralDepositRetentions", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_MineralDepositRetentions_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MineralDepositRetentions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MineralDepositRetentions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MineralDepositRetentions_Statuses_PreviousApplicationStatusId",
                        column: x => x.PreviousApplicationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MineralDepositRetentions_Statuses_PreviousOperationStatusId",
                        column: x => x.PreviousOperationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NicIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NicIssuePlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLocalDomicile = table.Column<bool>(type: "bit", nullable: false),
                    NameOfTribe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharesPercentage = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prospectings",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandAreaSize = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantType = table.Column<int>(type: "int", nullable: false),
                    Authorized = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedAndSubscribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfCapital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousMiningExperienceDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastSubmittedApplicationDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfTechnicalExpert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationOfTechnicalExpert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiningConcessionsDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GovernmentDuesDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AmountInWords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospectings", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Prospectings_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prospectings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reconnaissances",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessNature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    HasShareCapital = table.Column<bool>(type: "bit", nullable: false),
                    LandAreaSize = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    NumberOfYears = table.Column<int>(type: "int", nullable: false),
                    PreviousApplicationStatusId = table.Column<int>(type: "int", nullable: false),
                    PreviousOperationStatusId = table.Column<int>(type: "int", nullable: false),
                    Offences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reconnaissances", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Reconnaissances_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reconnaissances_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reconnaissances_Statuses_PreviousApplicationStatusId",
                        column: x => x.PreviousApplicationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reconnaissances_Statuses_PreviousOperationStatusId",
                        column: x => x.PreviousOperationStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SmallMiningLeases",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandAreaSize = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ApplicantType = table.Column<int>(type: "int", nullable: false),
                    Authorized = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedAndSubscribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfCapital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousMiningExperienceDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastSubmittedApplicationDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfTechnicalExpert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationOfTechnicalExpert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiningConcessionsDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GovernmentDuesDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AmountInWords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallMiningLeases", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_SmallMiningLeases_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmallMiningLeases_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMinerals_ApplicationId",
                table: "ApplicationMinerals",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMinerals_MineralId",
                table: "ApplicationMinerals",
                column: "MineralId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GrantedOrRefusedBy",
                table: "Applications",
                column: "GrantedOrRefusedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_SendBy",
                table: "Applications",
                column: "SendBy");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_SendTo",
                table: "Applications",
                column: "SendTo");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StatusId",
                table: "Applications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Explorations_CountryId",
                table: "Explorations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Explorations_LocationId",
                table: "Explorations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Explorations_PreviousApplicationStatusId",
                table: "Explorations",
                column: "PreviousApplicationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Explorations_PreviousOperationStatusId",
                table: "Explorations",
                column: "PreviousOperationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LandSurrendersAndTransfers_PreviousApplicationStatusId",
                table: "LandSurrendersAndTransfers",
                column: "PreviousApplicationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LandSurrendersAndTransfers_PreviousOperationStatusId",
                table: "LandSurrendersAndTransfers",
                column: "PreviousOperationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LargeMiningLeases_LocationId",
                table: "LargeMiningLeases",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LargeMiningLeases_PreviousApplicationStatusId",
                table: "LargeMiningLeases",
                column: "PreviousApplicationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LargeMiningLeases_PreviousOperationStatusId",
                table: "LargeMiningLeases",
                column: "PreviousOperationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MineralDepositRetentions_CountryId",
                table: "MineralDepositRetentions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MineralDepositRetentions_LocationId",
                table: "MineralDepositRetentions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MineralDepositRetentions_PreviousApplicationStatusId",
                table: "MineralDepositRetentions",
                column: "PreviousApplicationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MineralDepositRetentions_PreviousOperationStatusId",
                table: "MineralDepositRetentions",
                column: "PreviousOperationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ApplicationId",
                table: "Persons",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CompanyId",
                table: "Persons",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryId",
                table: "Persons",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospectings_LocationId",
                table: "Prospectings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reconnaissances_LocationId",
                table: "Reconnaissances",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reconnaissances_PreviousApplicationStatusId",
                table: "Reconnaissances",
                column: "PreviousApplicationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reconnaissances_PreviousOperationStatusId",
                table: "Reconnaissances",
                column: "PreviousOperationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallMiningLeases_LocationId",
                table: "SmallMiningLeases",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationMinerals");

            migrationBuilder.DropTable(
                name: "Explorations");

            migrationBuilder.DropTable(
                name: "LandSurrendersAndTransfers");

            migrationBuilder.DropTable(
                name: "LargeMiningLeases");

            migrationBuilder.DropTable(
                name: "MineralDepositRetentions");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Prospectings");

            migrationBuilder.DropTable(
                name: "Reconnaissances");

            migrationBuilder.DropTable(
                name: "SmallMiningLeases");

            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
