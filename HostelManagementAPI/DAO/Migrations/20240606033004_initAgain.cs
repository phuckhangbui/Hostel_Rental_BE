using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class initAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PackageStatus = table.Column<int>(type: "int", nullable: true),
                    IsLoginWithGmail = table.Column<bool>(type: "bit", nullable: true),
                    OtpToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    MemberShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberShipName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacityHostel = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    MemberShipFee = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.MemberShipID);
                });

            migrationBuilder.CreateTable(
                name: "TypeService",
                columns: table => new
                {
                    TypeServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeService", x => x.TypeServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Hostel",
                columns: table => new
                {
                    HostelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostelAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostel", x => x.HostelID);
                    table.ForeignKey(
                        name: "FK_Hostel_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    NoticeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoticeAccountAccountID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    NoticeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateNotice = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.NoticeID);
                    table.ForeignKey(
                        name: "FK_Notice_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notice_Account_NoticeAccountAccountID",
                        column: x => x.NoticeAccountAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipsRegisterTransaction",
                columns: table => new
                {
                    MemberShipTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberShipID = table.Column<int>(type: "int", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExpire = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PackageFee = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipsRegisterTransaction", x => x.MemberShipTransactionID);
                    table.ForeignKey(
                        name: "FK_MembershipsRegisterTransaction_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembershipsRegisterTransaction_Membership_MemberShipID",
                        column: x => x.MemberShipID,
                        principalTable: "Membership",
                        principalColumn: "MemberShipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HostelImages",
                columns: table => new
                {
                    HostelImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelImages", x => x.HostelImageID);
                    table.ForeignKey(
                        name: "FK_HostelImages_Hostel_HostelID",
                        column: x => x.HostelID,
                        principalTable: "Hostel",
                        principalColumn: "HostelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    Lenght = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomFee = table.Column<double>(type: "float", nullable: true),
                    HostelID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Room_Hostel_HostelID",
                        column: x => x.HostelID,
                        principalTable: "Hostel",
                        principalColumn: "HostelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complain",
                columns: table => new
                {
                    ComplainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    RoomID = table.Column<int>(type: "int", nullable: true),
                    ComplainText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateComplain = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complain", x => x.ComplainID);
                    table.ForeignKey(
                        name: "FK_Complain_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complain_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerAccountID = table.Column<int>(type: "int", nullable: true),
                    StudentAccountID = table.Column<int>(type: "int", nullable: true),
                    RoomID = table.Column<int>(type: "int", nullable: true),
                    ContractTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSign = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoomFee = table.Column<double>(type: "float", nullable: true),
                    DepositFee = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contract_Account_OwnerAccountID",
                        column: x => x.OwnerAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Account_StudentAccountID",
                        column: x => x.StudentAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomService",
                columns: table => new
                {
                    RoomServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TypeServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomService", x => x.RoomServiceId);
                    table.ForeignKey(
                        name: "FK_RoomService_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomService_TypeService_TypeServiceId",
                        column: x => x.TypeServiceId,
                        principalTable: "TypeService",
                        principalColumn: "TypeServiceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomsImage",
                columns: table => new
                {
                    RoomImgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsImage", x => x.RoomImgID);
                    table.ForeignKey(
                        name: "FK_RoomsImage_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPayment",
                columns: table => new
                {
                    BillPaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: false),
                    BillAmount = table.Column<double>(type: "float", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: true),
                    BillPaymentStatus = table.Column<int>(type: "int", nullable: true),
                    BillType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayment", x => x.BillPaymentID);
                    table.ForeignKey(
                        name: "FK_BillPayment_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractMember",
                columns: table => new
                {
                    ContractMemberD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractMember", x => x.ContractMemberD);
                    table.ForeignKey(
                        name: "FK_ContractMember_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_ContractMember_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractDetail",
                columns: table => new
                {
                    ContractDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: true),
                    RoomServiceId = table.Column<int>(type: "int", nullable: true),
                    ServiceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDetail", x => x.ContractDetailID);
                    table.ForeignKey(
                        name: "FK_ContractDetail_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetail_RoomService_RoomServiceId",
                        column: x => x.RoomServiceId,
                        principalTable: "RoomService",
                        principalColumn: "RoomServiceId");
                });

            migrationBuilder.CreateTable(
                name: "BillPaymentDetail",
                columns: table => new
                {
                    BillPaymentDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillPaymentID = table.Column<int>(type: "int", nullable: true),
                    RoomServiceID = table.Column<int>(type: "int", nullable: true),
                    OldNumberService = table.Column<double>(type: "float", nullable: true),
                    NewNumberService = table.Column<double>(type: "float", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ServiceTotalAmount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPaymentDetail", x => x.BillPaymentDetailID);
                    table.ForeignKey(
                        name: "FK_BillPaymentDetail_BillPayment_BillPaymentID",
                        column: x => x.BillPaymentID,
                        principalTable: "BillPayment",
                        principalColumn: "BillPaymentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillPaymentDetail_RoomService_RoomServiceID",
                        column: x => x.RoomServiceID,
                        principalTable: "RoomService",
                        principalColumn: "RoomServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_ContractID",
                table: "BillPayment",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentDetail_BillPaymentID",
                table: "BillPaymentDetail",
                column: "BillPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentDetail_RoomServiceID",
                table: "BillPaymentDetail",
                column: "RoomServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Complain_AccountID",
                table: "Complain",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Complain_RoomID",
                table: "Complain",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OwnerAccountID",
                table: "Contract",
                column: "OwnerAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_RoomID",
                table: "Contract",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_StudentAccountID",
                table: "Contract",
                column: "StudentAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetail_ContractID",
                table: "ContractDetail",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetail_RoomServiceId",
                table: "ContractDetail",
                column: "RoomServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMember_AccountID",
                table: "ContractMember",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMember_ContractID",
                table: "ContractMember",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Hostel_AccountID",
                table: "Hostel",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_HostelImages_HostelID",
                table: "HostelImages",
                column: "HostelID");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipsRegisterTransaction_AccountID",
                table: "MembershipsRegisterTransaction",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipsRegisterTransaction_MemberShipID",
                table: "MembershipsRegisterTransaction",
                column: "MemberShipID");

            migrationBuilder.CreateIndex(
                name: "IX_Notice_AccountID",
                table: "Notice",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Notice_NoticeAccountAccountID",
                table: "Notice",
                column: "NoticeAccountAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HostelID",
                table: "Room",
                column: "HostelID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomService_RoomId",
                table: "RoomService",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomService_TypeServiceId",
                table: "RoomService",
                column: "TypeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsImage_RoomID",
                table: "RoomsImage",
                column: "RoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPaymentDetail");

            migrationBuilder.DropTable(
                name: "Complain");

            migrationBuilder.DropTable(
                name: "ContractDetail");

            migrationBuilder.DropTable(
                name: "ContractMember");

            migrationBuilder.DropTable(
                name: "HostelImages");

            migrationBuilder.DropTable(
                name: "MembershipsRegisterTransaction");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "RoomsImage");

            migrationBuilder.DropTable(
                name: "BillPayment");

            migrationBuilder.DropTable(
                name: "RoomService");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "TypeService");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Hostel");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
