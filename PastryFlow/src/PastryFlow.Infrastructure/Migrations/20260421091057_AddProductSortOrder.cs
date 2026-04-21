using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSortOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 287, DateTimeKind.Utc).AddTicks(790));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 287, DateTimeKind.Utc).AddTicks(3052));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 287, DateTimeKind.Utc).AddTicks(3055));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 287, DateTimeKind.Utc).AddTicks(3058));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 287, DateTimeKind.Utc).AddTicks(3072));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 287, DateTimeKind.Utc).AddTicks(3075));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(6918));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(6928));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(6960));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(6963));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(6966));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 290, DateTimeKind.Utc).AddTicks(6976));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(896), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3865), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3877), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3881), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3885), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3896), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3900), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3910), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3914), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3921), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3924), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3928), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3931), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3935), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3938), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3944), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3962), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3970), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3974), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3978), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3981), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3985), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3988), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3994), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(3998), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4001), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4005), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4009), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4014), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4018), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4023), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4066), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4071), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4076), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4081), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4085), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4088), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4092), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4096), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4102), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4106), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4120), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4125), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4129), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4132), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4136), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4140), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4146), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4150), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4153), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4158), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4161), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4166), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4174), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4178), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4184), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4188), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4191), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4195), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4199), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4203), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4207), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4210), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4216), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4220), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4225), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4236), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4240), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4244), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4247), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4251), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4257), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4261), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4265), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4268), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4274), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4278), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4282), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4286), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4291), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4295), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4299), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4302), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4306), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4311), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4315), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4319), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 21, 9, 10, 56, 291, DateTimeKind.Utc).AddTicks(4325), 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(2119));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5039));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5717));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5723));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5728));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5731));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5741));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 9, 10, 56, 288, DateTimeKind.Utc).AddTicks(5744));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(3639));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5387));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5403));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 876, DateTimeKind.Utc).AddTicks(8030));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2519));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2537));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2539));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(5542));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7908));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7922));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7925));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7947));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7954));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7962));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7969));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7975));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7997));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8015));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8028));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8034));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8037));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8045));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8081));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8103));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8110));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8114));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8117));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8133));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8136));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8144));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8148));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8151));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8164));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8168));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8175));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8179));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8189));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8192));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8198));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8205));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8209));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8212));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8216));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8219));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8223));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8228));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8233));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8238));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8241));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8245));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8248));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8252));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8255));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8261));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8265));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8273));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8278));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8293));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8302));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8305));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8309));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8314));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8317));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8321));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8325));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(4941));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5440));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5446));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5448));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5450));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5461));
        }
    }
}
