using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Point_of_Sale_website.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestedDate",
                value: new DateTime(2023, 12, 10, 0, 22, 48, 770, DateTimeKind.Local).AddTicks(9049));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "RequestApproved", "RequestedBy", "RequestedDate" },
                values: new object[,]
                {
                    { 2, "Tâm lý xã hội", true, "StoreOwner", new DateTime(2023, 12, 10, 0, 22, 48, 770, DateTimeKind.Local).AddTicks(9062) },
                    { 3, "kiến thức gia đình", true, "StoreOwner", new DateTime(2023, 12, 10, 0, 22, 48, 770, DateTimeKind.Local).AddTicks(9064) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 4, "Customers", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestedDate",
                value: new DateTime(2023, 11, 27, 19, 9, 18, 316, DateTimeKind.Local).AddTicks(6686));
        }
    }
}
