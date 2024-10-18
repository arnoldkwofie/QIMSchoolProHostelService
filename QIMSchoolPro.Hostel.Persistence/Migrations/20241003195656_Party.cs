using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QIMSchoolPro.Hostel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Party : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDisability = table.Column<bool>(type: "bit", nullable: true),
                    DisabilityReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyType = table.Column<int>(type: "int", nullable: false),
                    Name_Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_OtherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryEmailAddress_Email_EmailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherEmailAddress_Email_EmailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryPhoneNumber_PhoneNumberType = table.Column<int>(type: "int", nullable: false),
                    PrimaryPhoneNumber_Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryPhoneNumber_Phone_CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherPhoneNumber_PhoneNumberType = table.Column<int>(type: "int", nullable: false),
                    OtherPhoneNumber_Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherPhoneNumber_Phone_CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_AddressType = table.Column<int>(type: "int", nullable: false),
                    AddressLine_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_Line3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_Line4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_RegionId = table.Column<int>(type: "int", nullable: true),
                    AddressLine_CountryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_DistrictId = table.Column<int>(type: "int", nullable: true),
                    AddressLine_CityText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_HomeTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine_CityId = table.Column<int>(type: "int", nullable: true),
                    AddressLine_GhanaPostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Audit_LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_EntityStatusCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_EntityStatusCreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_EntityStatusLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_EntityStatusLastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherProperty1 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Party");
        }
    }
}
