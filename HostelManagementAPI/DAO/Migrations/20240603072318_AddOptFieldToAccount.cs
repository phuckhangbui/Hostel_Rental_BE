using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddOptFieldToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Status",
            //    table: "MembershipsRegisterTransaction");

            //migrationBuilder.AddColumn<int>(
            //    name: "Gender",
            //    table: "Account",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "IsPackage",
            //    table: "Account",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtpToken",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Gender",
            //    table: "Account");

            //migrationBuilder.DropColumn(
            //    name: "IsPackage",
            //    table: "Account");

            migrationBuilder.DropColumn(
                name: "OtpToken",
                table: "Account");

            //migrationBuilder.AddColumn<int>(
            //    name: "Status",
            //    table: "MembershipsRegisterTransaction",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
