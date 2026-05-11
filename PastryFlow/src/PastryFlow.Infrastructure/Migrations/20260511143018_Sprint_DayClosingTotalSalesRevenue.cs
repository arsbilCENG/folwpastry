using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint_DayClosingTotalSalesRevenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalesRevenue",
                table: "DayClosings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 865, DateTimeKind.Utc).AddTicks(613));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 865, DateTimeKind.Utc).AddTicks(5083));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 865, DateTimeKind.Utc).AddTicks(5092));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 865, DateTimeKind.Utc).AddTicks(5095));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 865, DateTimeKind.Utc).AddTicks(5098));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 865, DateTimeKind.Utc).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(6008));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9280));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9326));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9423));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9431));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9439));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9459));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9473));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9488));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9497));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9510));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9544));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9562));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9581));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 874, DateTimeKind.Utc).AddTicks(9605));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 870, DateTimeKind.Utc).AddTicks(8479));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9843));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9848));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9854));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 871, DateTimeKind.Utc).AddTicks(9859));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 872, DateTimeKind.Utc).AddTicks(8477));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5272));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5284));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5293));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5323));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5344));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5399));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5414));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5423));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5465));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5478));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5499));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5530));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5538));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5546));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5642));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5665));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5699));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5716));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5729));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5741));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5818));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5827));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5838));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5846));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5861));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5893));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5909));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5960));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5974));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5982));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(5990));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6002));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6044));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6054));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6061));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6069));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6077));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6087));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6103));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6128));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6175));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6186));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6204));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6213));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6226));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6249));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6273));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6288));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6297));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6305));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6317));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6326));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6334));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6362));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6371));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6379));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6396));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6404));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6423));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6432));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6455));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6462));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6471));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6492));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 873, DateTimeKind.Utc).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(3264));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(8237));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(9292));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(9307));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 14, 30, 14, 867, DateTimeKind.Utc).AddTicks(9318));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSalesRevenue",
                table: "DayClosings");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 949, DateTimeKind.Utc).AddTicks(9052));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 950, DateTimeKind.Utc).AddTicks(2563));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 950, DateTimeKind.Utc).AddTicks(2606));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 950, DateTimeKind.Utc).AddTicks(2610));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 950, DateTimeKind.Utc).AddTicks(2613));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 950, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(5281));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9088));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9114));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9124));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9132));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9151));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9167));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9191));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9201));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9213));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9234));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9255));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9264));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9279));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9288));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9299));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9317));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9363));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 959, DateTimeKind.Utc).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 954, DateTimeKind.Utc).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1569));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1573));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1578));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 956, DateTimeKind.Utc).AddTicks(1598));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 957, DateTimeKind.Utc).AddTicks(1916));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(1898));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(1987));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2017));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2044));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2096));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2255));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2298));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2332));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2357));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2462));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2478));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2509));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2644));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2666));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2676));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2685));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2694));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2703));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2749));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2759));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2778));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2790));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2900));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2931));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2948));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2967));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2978));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2988));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3017));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3027));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3041));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3051));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3061));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3070));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3080));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3113));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3195));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3208));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3217));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3229));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3259));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3272));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3284));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3300));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3317));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3329));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3348));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3359));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3370));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3389));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3404));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3424));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3451));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3461));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3471));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3481));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3495));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3505));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3514));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3524));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3567));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3580));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3590));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 958, DateTimeKind.Utc).AddTicks(3606));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 951, DateTimeKind.Utc).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(1970));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(1981));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(1986));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(1996));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 18, 12, 7, 952, DateTimeKind.Utc).AddTicks(2013));
        }
    }
}
