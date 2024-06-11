using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class addFieldInPackageRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MembershipsRegisterTransaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TnxRef",
                table: "MembershipsRegisterTransaction",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MembershipsRegisterTransaction");

            migrationBuilder.DropColumn(
                name: "TnxRef",
                table: "MembershipsRegisterTransaction");
        }
    }
}
