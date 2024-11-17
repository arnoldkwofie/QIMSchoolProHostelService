using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QIMSchoolPro.Hostel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServerIPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_PaymentVendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payee_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payee_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payee_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentVendorDetail_TallerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentVendorDetail_TallerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentVendorDetail_Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentVendorDetail_BankTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentVendorId = table.Column<int>(type: "int", nullable: false),
                    AcademicPeriod_LowerYear = table.Column<int>(type: "int", nullable: false),
                    AcademicPeriod_UpperYear = table.Column<int>(type: "int", nullable: false),
                    AcademicPeriod_Semester = table.Column<int>(type: "int", nullable: false),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payment_PaymentVendor_PaymentVendorId",
                        column: x => x.PaymentVendorId,
                        principalTable: "PaymentVendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentItemLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Amount_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_LocalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_ForeignValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_LocalCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount_ForeignCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount_Rate_Selling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Rate_Buying = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Token_ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token_PinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_PaymentItemLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentItemLine_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PartyId",
                table: "Payment",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentVendorId",
                table: "Payment",
                column: "PaymentVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItemLine_PaymentId",
                table: "PaymentItemLine",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentItemLine");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentVendor");
        }
    }
}
