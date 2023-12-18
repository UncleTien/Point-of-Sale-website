using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Point_of_Sale_website.Migrations
{
    /// <inheritdoc />
    public partial class isloked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestedDate",
                value: new DateTime(2023, 11, 27, 19, 9, 18, 316, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsLocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsLocked",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestedDate",
                value: new DateTime(2023, 11, 27, 19, 7, 53, 992, DateTimeKind.Local).AddTicks(4978));
        }
    }
}
