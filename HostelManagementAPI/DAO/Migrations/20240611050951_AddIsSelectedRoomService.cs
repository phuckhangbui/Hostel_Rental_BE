using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSelectedRoomService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Contract_ContractID",
                table: "BillPayment");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                table: "BillPayment",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_ContractID",
                table: "BillPayment",
                newName: "IX_BillPayment_ContractId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "RoomService",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "BillPayment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Contract_ContractId",
                table: "BillPayment",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "ContractID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Contract_ContractId",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "RoomService");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "BillPayment",
                newName: "ContractID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_ContractId",
                table: "BillPayment",
                newName: "IX_BillPayment_ContractID");

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "BillPayment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Contract_ContractID",
                table: "BillPayment",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
