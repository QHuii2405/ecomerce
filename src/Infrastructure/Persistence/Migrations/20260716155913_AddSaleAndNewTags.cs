using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleAndNewTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "OldPrice",
                value: 24720000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                column: "OldPrice",
                value: 3240000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { true, 25080000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000012"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000013"),
                column: "OldPrice",
                value: 18960000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000015"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000017"),
                column: "OldPrice",
                value: 1560000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000018"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000021"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { true, 8160000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000024"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000025"),
                column: "OldPrice",
                value: 20400000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000027"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000029"),
                column: "OldPrice",
                value: 20880000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000030"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000033"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { true, 11400000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000036"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000037"),
                column: "OldPrice",
                value: 12360000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000039"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000041"),
                column: "OldPrice",
                value: 9120000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000042"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000045"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { true, 12840000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000048"),
                column: "IsNew",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000049"),
                column: "OldPrice",
                value: 11400000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000012"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000013"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000015"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000017"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000018"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000021"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000024"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000025"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000027"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000029"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000030"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000033"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000036"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000037"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000039"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000041"),
                column: "OldPrice",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000042"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000045"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000048"),
                column: "IsNew",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000049"),
                column: "OldPrice",
                value: null);
        }
    }
}
