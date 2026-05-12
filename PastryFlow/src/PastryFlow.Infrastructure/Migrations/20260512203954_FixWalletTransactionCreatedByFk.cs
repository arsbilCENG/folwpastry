using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixWalletTransactionCreatedByFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Users_CreatedById",
                table: "WalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WalletTransactions_CreatedById",
                table: "WalletTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "WalletTransactions");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 541, DateTimeKind.Utc).AddTicks(2195));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 541, DateTimeKind.Utc).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 541, DateTimeKind.Utc).AddTicks(4554));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 541, DateTimeKind.Utc).AddTicks(4556));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 541, DateTimeKind.Utc).AddTicks(4559));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 541, DateTimeKind.Utc).AddTicks(4561));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(9773));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1345));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1353));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1356));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1360));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1366));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1371));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1381));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1384));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1388));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1392));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1402));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1419));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1426));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1429));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 547, DateTimeKind.Utc).AddTicks(1432));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7891));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 545, DateTimeKind.Utc).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5404));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5469));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5504));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5510));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5514));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5523));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5532));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5555));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5566));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5578));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5582));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5586));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5590));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5598));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5605));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5609));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5633));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5638));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5649));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5664));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5669));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5673));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5677));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5682));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5687));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5707));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5711));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5716));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5720));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5724));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5728));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5733));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5739));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5743));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5747));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5767));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5793));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5804));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5809));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5814));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5819));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5824));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5828));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5833));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5855));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5859));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5865));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5874));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5879));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5883));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5890));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5894));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5908));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5912));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5916));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5920));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5927));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5935));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5939));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5943));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5948));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5953));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5957));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 546, DateTimeKind.Utc).AddTicks(5964));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(4141));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(7781));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(8613));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(8624));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(8628));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(8631));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(8635));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 20, 39, 52, 542, DateTimeKind.Utc).AddTicks(8684));

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_CreatedByUserId",
                table: "WalletTransactions",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Users_CreatedByUserId",
                table: "WalletTransactions",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Users_CreatedByUserId",
                table: "WalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WalletTransactions_CreatedByUserId",
                table: "WalletTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "WalletTransactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_CreatedById",
                table: "WalletTransactions",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Users_CreatedById",
                table: "WalletTransactions",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
