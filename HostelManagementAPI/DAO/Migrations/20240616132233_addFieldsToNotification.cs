using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class addFieldsToNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Account_AccountID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Account_AccountNoticeAccountID",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "AccountNoticeAccountID",
                table: "Notifications",
                newName: "ReceiveAccountId");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Notifications",
                newName: "AccountNoticeId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_AccountNoticeAccountID",
                table: "Notifications",
                newName: "IX_Notifications_ReceiveAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_AccountID",
                table: "Notifications",
                newName: "IX_Notifications_AccountNoticeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Account_AccountNoticeId",
                table: "Notifications",
                column: "AccountNoticeId",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Account_ReceiveAccountId",
                table: "Notifications",
                column: "ReceiveAccountId",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Account_AccountNoticeId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Account_ReceiveAccountId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ReceiveAccountId",
                table: "Notifications",
                newName: "AccountNoticeAccountID");

            migrationBuilder.RenameColumn(
                name: "AccountNoticeId",
                table: "Notifications",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ReceiveAccountId",
                table: "Notifications",
                newName: "IX_Notifications_AccountNoticeAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_AccountNoticeId",
                table: "Notifications",
                newName: "IX_Notifications_AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Account_AccountID",
                table: "Notifications",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Account_AccountNoticeAccountID",
                table: "Notifications",
                column: "AccountNoticeAccountID",
                principalTable: "Account",
                principalColumn: "AccountID");
        }
    }
}
