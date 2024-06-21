using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class accountReceiveAndAccountPaidInBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountPayId",
                table: "BillPayment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountReceiveId",
                table: "BillPayment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_AccountPayId",
                table: "BillPayment",
                column: "AccountPayId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_AccountReceiveId",
                table: "BillPayment",
                column: "AccountReceiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Account_AccountPayId",
                table: "BillPayment",
                column: "AccountPayId",
                principalTable: "Account",
                principalColumn: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Account_AccountReceiveId",
                table: "BillPayment",
                column: "AccountReceiveId",
                principalTable: "Account",
                principalColumn: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Account_AccountPayId",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Account_AccountReceiveId",
                table: "BillPayment");

            migrationBuilder.DropIndex(
                name: "IX_BillPayment_AccountPayId",
                table: "BillPayment");

            migrationBuilder.DropIndex(
                name: "IX_BillPayment_AccountReceiveId",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "AccountPayId",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "AccountReceiveId",
                table: "BillPayment");
        }
    }
}
