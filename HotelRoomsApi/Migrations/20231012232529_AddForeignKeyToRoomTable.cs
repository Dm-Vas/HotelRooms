using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRoomsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "RoomNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 16, 25, 29, 149, DateTimeKind.Local).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 16, 25, 29, 149, DateTimeKind.Local).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 16, 25, 29, 149, DateTimeKind.Local).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 16, 25, 29, 149, DateTimeKind.Local).AddTicks(1427));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 16, 25, 29, 149, DateTimeKind.Local).AddTicks(1430));

            migrationBuilder.CreateIndex(
                name: "IX_RoomNumbers_RoomID",
                table: "RoomNumbers",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomNumbers_Rooms_RoomID",
                table: "RoomNumbers",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomNumbers_Rooms_RoomID",
                table: "RoomNumbers");

            migrationBuilder.DropIndex(
                name: "IX_RoomNumbers_RoomID",
                table: "RoomNumbers");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "RoomNumbers");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 15, 57, 24, 8, DateTimeKind.Local).AddTicks(8482));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 15, 57, 24, 8, DateTimeKind.Local).AddTicks(8521));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 15, 57, 24, 8, DateTimeKind.Local).AddTicks(8524));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 15, 57, 24, 8, DateTimeKind.Local).AddTicks(8526));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 12, 15, 57, 24, 8, DateTimeKind.Local).AddTicks(8528));
        }
    }
}
