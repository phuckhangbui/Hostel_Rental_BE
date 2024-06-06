using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class deleteContractDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractMember_Account_AccountID",
                table: "ContractMember");

            migrationBuilder.DropTable(
                name: "ContractDetail");

            migrationBuilder.DropIndex(
                name: "IX_ContractMember_AccountID",
                table: "ContractMember");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "ContractMember");

            migrationBuilder.RenameColumn(
                name: "ContractMemberD",
                table: "ContractMember",
                newName: "ContractMemberID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractMemberID",
                table: "ContractMember",
                newName: "ContractMemberD");

            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "ContractMember",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ContractMember_AccountID",
                table: "ContractMember",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetail_ContractID",
                table: "ContractDetail",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetail_RoomServiceId",
                table: "ContractDetail",
                column: "RoomServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractMember_Account_AccountID",
                table: "ContractMember",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "AccountID");
        }
    }
}
