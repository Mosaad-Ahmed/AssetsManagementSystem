using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetsManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddManfacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Manufacturers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62474474-f91b-483d-b0d8-2742c01146f0"),
                column: "ConcurrencyStamp",
                value: "87ae7d73-10d7-48f2-8e53-4be33ce74f53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("846e3679-1537-487d-969c-3a6116fc3b2d"),
                column: "ConcurrencyStamp",
                value: "ea3413b5-fa77-4648-807c-79f36fd6850c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d9c0c478-adf7-40db-ade3-2b7810d9659f"),
                column: "ConcurrencyStamp",
                value: "54a5a7ef-5f31-4307-8859-e98b4bbe011f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc05f613-0e97-444e-b19b-018a223a7484"),
                column: "ConcurrencyStamp",
                value: "4335cba7-14f4-4b33-8bd4-1149a318ab23");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bdabcf06-a956-4ef7-8045-3214e68b9b4c"),
                columns: new[] { "AddedOnDate", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(2024, 9, 11, 14, 25, 20, 930, DateTimeKind.Local).AddTicks(1945), "3f8b4c2b-5fd9-4e9e-bc66-6a7265bd6b8a", "AQAAAAIAAYagAAAAEPVqfWm+hnHVbUIsUgyBH6b6UqelkGbQG4QgVRiKLYIRtw2WmrjJ2E8DAq/dzy6ghA==", "f4bac057-c293-441b-90ef-41b8d31d2298" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Manufacturers");

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
    }
}
