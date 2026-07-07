using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSaleAndNewFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OldPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000011"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000012"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000013"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000014"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000015"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000016"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000017"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000018"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000019"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000020"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000021"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000022"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000023"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000024"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000025"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000026"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000027"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000028"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000029"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000030"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000031"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000032"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000033"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000034"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000035"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000036"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000037"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000038"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000039"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000040"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000041"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000042"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000043"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000044"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000045"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000046"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000047"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000048"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000049"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000050"),
                columns: new[] { "IsNew", "OldPrice" },
                values: new object[] { false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
