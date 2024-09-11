using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetsManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Suppliers",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Suppliers",
                newName: "email");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "WarrantyExpiryDate",
                table: "Assets",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DepreciationDate",
                table: "Assets",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62474474-f91b-483d-b0d8-2742c01146f0"),
                column: "ConcurrencyStamp",
                value: "2d82efcb-ea78-486f-8388-f404105e8c4f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("846e3679-1537-487d-969c-3a6116fc3b2d"),
                column: "ConcurrencyStamp",
                value: "b9bdafc8-763b-47df-9c57-7c6af798f551");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d9c0c478-adf7-40db-ade3-2b7810d9659f"),
                column: "ConcurrencyStamp",
                value: "7d937483-51de-4e3c-92e9-265fd047a823");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc05f613-0e97-444e-b19b-018a223a7484"),
                column: "ConcurrencyStamp",
                value: "9a90f48e-c43a-45c2-b80a-7c2dc9176917");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bdabcf06-a956-4ef7-8045-3214e68b9b4c"),
                columns: new[] { "AddedOnDate", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 9, 11, 11, 2, 40, 549, DateTimeKind.Local).AddTicks(5522), "edc157a6-69fc-4cda-8b7a-b981c914a04d", "AQAAAAIAAYagAAAAEBD6qscKWLU/+YHWk2BOmXtoDuA1/34sS5YSXtrrDwScJv5svAxe2fjrE5g+b8/nxg==", "68f32ced-5ab5-4ce9-8949-18df26fe5f23" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DepreciationDate",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Suppliers",
                newName: "ContactInfo");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Suppliers",
                newName: "Name");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "WarrantyExpiryDate",
                table: "Assets",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62474474-f91b-483d-b0d8-2742c01146f0"),
                column: "ConcurrencyStamp",
                value: "a8228163-f18b-4703-8d9f-5d0fc4f866af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("846e3679-1537-487d-969c-3a6116fc3b2d"),
                column: "ConcurrencyStamp",
                value: "c47def99-1654-4b01-8223-9e9b1769e2f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d9c0c478-adf7-40db-ade3-2b7810d9659f"),
                column: "ConcurrencyStamp",
                value: "7ac3e0a8-69aa-4352-b782-8f11382a0f1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc05f613-0e97-444e-b19b-018a223a7484"),
                column: "ConcurrencyStamp",
                value: "1e104f2c-a1c0-4dfc-a9f5-0ca63211fd3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bdabcf06-a956-4ef7-8045-3214e68b9b4c"),
                columns: new[] { "AddedOnDate", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 9, 10, 15, 36, 7, 340, DateTimeKind.Local).AddTicks(5096), "ee40935a-0a49-4061-a386-d509eba5d056", "AQAAAAIAAYagAAAAEOEMPUXTzA+apAZhPnM6RCzf7st7upl5lx9WXa/rqfz95VH65TtYB/jpNahr9ta3XA==", "75050396-8313-455a-8967-4ef9ae2f25c4" });
        }
    }
}
