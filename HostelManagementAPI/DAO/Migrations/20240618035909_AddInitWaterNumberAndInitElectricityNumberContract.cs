using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddInitWaterNumberAndInitElectricityNumberContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitElectricityNumber",
                table: "Contract",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InitWaterNumber",
                table: "Contract",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitElectricityNumber",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "InitWaterNumber",
                table: "Contract");
        }
    }
}
