using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInBillPaymentAndContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DepositFee",
                table: "Contract",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RoomFee",
                table: "Contract",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillType",
                table: "BillPayment",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositFee",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "RoomFee",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BillType",
                table: "BillPayment");
        }
    }
}
