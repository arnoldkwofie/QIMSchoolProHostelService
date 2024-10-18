using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QIMSchoolPro.Hostel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class beds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    BedNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Bed", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_FloorId",
                table: "Room",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Floor_FloorId",
                table: "Room",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Floor_FloorId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropIndex(
                name: "IX_Room_FloorId",
                table: "Room");
        }
    }
}
