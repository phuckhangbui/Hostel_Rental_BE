using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldInRoomAndHostel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "Room",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Hostel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostelType",
                table: "Hostel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Hostel");

            migrationBuilder.DropColumn(
                name: "HostelType",
                table: "Hostel");
        }
    }
}
