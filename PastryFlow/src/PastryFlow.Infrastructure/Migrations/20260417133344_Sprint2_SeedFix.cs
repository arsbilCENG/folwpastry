using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint2_SeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 396, DateTimeKind.Utc).AddTicks(9777));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 397, DateTimeKind.Utc).AddTicks(6699));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 397, DateTimeKind.Utc).AddTicks(6703));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 397, DateTimeKind.Utc).AddTicks(6712));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 397, DateTimeKind.Utc).AddTicks(6714));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 397, DateTimeKind.Utc).AddTicks(6716));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6892), 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6904), 3 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6987), 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6990), 5 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6998), 6 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(7001), 7 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(622));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3508));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3520));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3524));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3528));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3545) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3555) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3558) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3566) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3571) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3574) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3577) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3583) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3589) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3592) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3595) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3608));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3612));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3615));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3618));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3623));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3627));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3686));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3691));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3699));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3702));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3717));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3725));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3728));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3731));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3735));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3740));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3743));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3747));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3752));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3756));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3759));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3762));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3766));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3775));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3778));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3782));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3789));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3797));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3803));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3877));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3881));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3885));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3888));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3891));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3894));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3898));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3903));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3906));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3909));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3914));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3923));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3926));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3929));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3935));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3938));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3941));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3947));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3952));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3960));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3965));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3972));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3975));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3978));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(4033));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(4038));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(4041));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(4047));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(4051));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(4846));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8072));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222202"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(614));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3021));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3025));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3027));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3029));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(3917));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9682), 3 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9685), 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9722), 5 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9725), 6 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9728), 7 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                columns: new[] { "CreatedAt", "SortOrder" },
                values: new object[] { new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9730), 8 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "SortOrder", "UpdatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9664), true, "EKMEK", 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(3729));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7002));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7007));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7024) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7119) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7137) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7142) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7151) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7155) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7159) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7164) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7168) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7172) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7176) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                columns: new[] { "CategoryId", "CreatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7191));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7195));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7215));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7234));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7259));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7321));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7381));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7387));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7392));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7406));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7411));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7419));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7424));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7428));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7432));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7441));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7445));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7449));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7464));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7469));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7475));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7480));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7484));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7489));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7495));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7499));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7512));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7516));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7586));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7591));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7596));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7619));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7623));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7661));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7665));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7674));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7683));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7171));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7822));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7825));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7828));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7831));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7834));
        }
    }
}
