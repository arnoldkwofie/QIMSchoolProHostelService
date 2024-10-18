using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QIMSchoolPro.Hostel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcademicPeriod_LowerYear = table.Column<int>(type: "int", nullable: true),
                    AcademicPeriod_UpperYear = table.Column<int>(type: "int", nullable: true),
                    AcademicPeriod_Semester = table.Column<int>(type: "int", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CurrentVersion = table.Column<int>(type: "int", nullable: false),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Audit_LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_EntityStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_EntityStatusCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_EntityStatusCreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_EntityStatusLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Audit_EntityStatusLastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherProperty1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeAmenity_AmenityId",
                table: "RoomTypeAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_RoomId",
                table: "Bed",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bed_Room_RoomId",
                table: "Bed",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeAmenity_Amenity_AmenityId",
                table: "RoomTypeAmenity",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bed_Room_RoomId",
                table: "Bed");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeAmenity_Amenity_AmenityId",
                table: "RoomTypeAmenity");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypeAmenity_AmenityId",
                table: "RoomTypeAmenity");

            migrationBuilder.DropIndex(
                name: "IX_Bed_RoomId",
                table: "Bed");
        }
    }
}
