using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserLoyaltyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoyaltyPoints",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MembershipTier",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSpent",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                columns: new[] { "LoyaltyPoints", "MembershipTier", "TotalSpent" },
                values: new object[] { 0, "Bronze", 0m });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"),
                columns: new[] { "LoyaltyPoints", "MembershipTier", "TotalSpent" },
                values: new object[] { 0, "Bronze", 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MembershipTier",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalSpent",
                table: "Users");
        }
    }
}
