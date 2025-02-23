﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QIMSchoolPro.Hostel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Amenity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Amenity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    Availabilty = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_RoomTypeAmenity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropTable(
                name: "RoomTypeAmenity");
        }
    }
}
