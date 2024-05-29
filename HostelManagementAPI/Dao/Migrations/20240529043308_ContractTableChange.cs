using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ContractTableChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Account_AccountID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Account_OwnerAccountAccountID",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_OwnerAccountAccountID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "OwnerAccountAccountID",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Contract",
                newName: "StudentAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_AccountID",
                table: "Contract",
                newName: "IX_Contract_StudentAccountID");

            //migrationBuilder.AddColumn<double>(
            //    name: "PackageFee",
            //    table: "MembershipsRegisterTransaction",
            //    type: "float",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerAccountID",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OwnerAccountID",
                table: "Contract",
                column: "OwnerAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Account_OwnerAccountID",
                table: "Contract",
                column: "OwnerAccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Account_StudentAccountID",
                table: "Contract",
                column: "StudentAccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Account_OwnerAccountID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Account_StudentAccountID",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_OwnerAccountID",
                table: "Contract");

            //migrationBuilder.DropColumn(
            //    name: "PackageFee",
            //    table: "MembershipsRegisterTransaction");

            migrationBuilder.DropColumn(
                name: "OwnerAccountID",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "StudentAccountID",
                table: "Contract",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_StudentAccountID",
                table: "Contract",
                newName: "IX_Contract_AccountID");

            migrationBuilder.AddColumn<int>(
                name: "OwnerAccountAccountID",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OwnerAccountAccountID",
                table: "Contract",
                column: "OwnerAccountAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Account_AccountID",
                table: "Contract",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Account_OwnerAccountAccountID",
                table: "Contract",
                column: "OwnerAccountAccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
