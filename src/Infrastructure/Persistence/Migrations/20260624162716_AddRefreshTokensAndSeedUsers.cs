using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokensAndSeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "FullName", "IsDeleted", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), null, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "admin@ecommerce.com", "System Administrator", false, "$2a$11$Le3WZPxhWsgyNrsgQ5oEEOQ6uCYXNMpEWZ14rRqQAkZejS75R/zJK", null, null, null, "Admin", null },
                    { new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"), null, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "staff@ecommerce.com", "Store Operator", false, "$2a$11$9Nm0pHWKVQg31ja3mv6en.MwVYy2fEp11Tn0kqATnBJIehM2iL/Ky", null, null, null, "Staff", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");
        }
    }
}
