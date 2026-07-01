using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@ecommerce.com", "$2a$11$y2CddBh0H5CK9EmBsbo1EOj8pN26ACgU/Eme8ITQEqi3syLHxEFRG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@iluminaty.com", "$2a$11$Le3WZPxhWsgyNrsgQ5oEEOQ6uCYXNMpEWZ14rRqQAkZejS75R/zJK" });
        }
    }
}
