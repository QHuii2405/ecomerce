using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ResetAdminPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                column: "PasswordHash",
                value: "$2b$11$kZ/0UcwxKA/eXzbvAnhS9OaGA.l5bQDSxd/lNqcNh5w3xhNeByN/a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"),
                column: "PasswordHash",
                value: "$2b$11$kZ/0UcwxKA/eXzbvAnhS9OaGA.l5bQDSxd/lNqcNh5w3xhNeByN/a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                column: "PasswordHash",
                value: "$2a$11$y2CddBh0H5CK9EmBsbo1EOj8pN26ACgU/Eme8ITQEqi3syLHxEFRG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"),
                column: "PasswordHash",
                value: "$2a$11$9Nm0pHWKVQg31ja3mv6en.MwVYy2fEp11Tn0kqATnBJIehM2iL/Ky");
        }
    }
}
