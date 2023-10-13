using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelRoomsApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 11, 11, 23, 56, 140, DateTimeKind.Local).AddTicks(9321), "Room Description 1", "Room Name 1", 100.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2023, 10, 11, 11, 23, 56, 140, DateTimeKind.Local).AddTicks(9359), "Room Description 2", "Room Name 2", 100.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(2023, 10, 11, 11, 23, 56, 140, DateTimeKind.Local).AddTicks(9362), "Room Description 3", "Room Name 3", 70.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(2023, 10, 11, 11, 23, 56, 140, DateTimeKind.Local).AddTicks(9364), "Room Description 4", "Room Name 4", 70.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, new DateTime(2023, 10, 11, 11, 23, 56, 140, DateTimeKind.Local).AddTicks(9367), "Room Description 5", "Room Name 5", 50.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
