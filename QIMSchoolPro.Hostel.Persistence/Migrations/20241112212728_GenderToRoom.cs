using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QIMSchoolPro.Hostel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GenderToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderOption",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BedId",
                table: "Booking",
                column: "BedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Bed_BedId",
                table: "Booking",
                column: "BedId",
                principalTable: "Bed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Bed_BedId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BedId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "GenderOption",
                table: "Room");
        }
    }
}
