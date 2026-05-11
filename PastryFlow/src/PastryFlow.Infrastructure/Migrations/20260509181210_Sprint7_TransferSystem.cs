using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint7_TransferSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Products_ProductId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Branches_FromBranchId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Branches_ToBranchId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "ReceivedQuantity",
                table: "TransferItems");

            migrationBuilder.RenameColumn(
                name: "ToBranchId",
                table: "Transfers",
                newName: "SenderBranchId");

            migrationBuilder.RenameColumn(
                name: "FromBranchId",
                table: "Transfers",
                newName: "ReceiverBranchId");

            migrationBuilder.RenameColumn(
                name: "DriverUserId",
                table: "Transfers",
                newName: "CancelledByUserId");

            migrationBuilder.RenameColumn(
                name: "DeliveredAt",
                table: "Transfers",
                newName: "CancelledAt");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ToBranchId",
                table: "Transfers",
                newName: "IX_Transfers_SenderBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_FromBranchId",
                table: "Transfers",
                newName: "IX_Transfers_ReceiverBranchId");

            migrationBuilder.AlterColumn<string>(
                name: "TransferNumber",
                table: "Transfers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Transfers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Transfers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedAt",
                table: "Transfers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_CancelledByUserId",
                table: "Transfers",
                column: "CancelledByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_CreatedByUserId",
                table: "Transfers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ReceivedByUserId",
                table: "Transfers",
                column: "ReceivedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Products_ProductId",
                table: "TransferItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Branches_ReceiverBranchId",
                table: "Transfers",
                column: "ReceiverBranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Branches_SenderBranchId",
                table: "Transfers",
                column: "SenderBranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_CancelledByUserId",
                table: "Transfers",
                column: "CancelledByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_CreatedByUserId",
                table: "Transfers",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_ReceivedByUserId",
                table: "Transfers",
                column: "ReceivedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Products_ProductId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Branches_ReceiverBranchId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Branches_SenderBranchId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_CancelledByUserId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_CreatedByUserId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_ReceivedByUserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_CancelledByUserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_CreatedByUserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ReceivedByUserId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ShippedAt",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "SenderBranchId",
                table: "Transfers",
                newName: "ToBranchId");

            migrationBuilder.RenameColumn(
                name: "ReceiverBranchId",
                table: "Transfers",
                newName: "FromBranchId");

            migrationBuilder.RenameColumn(
                name: "CancelledByUserId",
                table: "Transfers",
                newName: "DriverUserId");

            migrationBuilder.RenameColumn(
                name: "CancelledAt",
                table: "Transfers",
                newName: "DeliveredAt");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_SenderBranchId",
                table: "Transfers",
                newName: "IX_Transfers_ToBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ReceiverBranchId",
                table: "Transfers",
                newName: "IX_Transfers_FromBranchId");

            migrationBuilder.AlterColumn<string>(
                name: "TransferNumber",
                table: "Transfers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Transfers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Transfers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedByUserId",
                table: "Transfers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TransferItems",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivedQuantity",
                table: "TransferItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 807, DateTimeKind.Utc).AddTicks(1794));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 807, DateTimeKind.Utc).AddTicks(4777));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 807, DateTimeKind.Utc).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 807, DateTimeKind.Utc).AddTicks(4821));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 807, DateTimeKind.Utc).AddTicks(4824));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 807, DateTimeKind.Utc).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 815, DateTimeKind.Utc).AddTicks(7754));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(940));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(957));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(971));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(997));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1007));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1014));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1021));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1029));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1037));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1043));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1051));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1065));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1072));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1081));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1162));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1182));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 816, DateTimeKind.Utc).AddTicks(1197));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 812, DateTimeKind.Utc).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2374));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2393));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2400));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2408));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2415));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2435));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 813, DateTimeKind.Utc).AddTicks(2442));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(1265));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7753));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7824));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7847));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7901));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7912));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7922));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(7985));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8002));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8039));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8049));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8080));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8116));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8127));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8173));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8244));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8274));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8321));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8331));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8342));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8415));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8436));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8447));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8458));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8472));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8483));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8504));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8514));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8525));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8536));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8564));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8576));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8587));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8599));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8659));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8671));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8698));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8709));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8720));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8751));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8776));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8787));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8797));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8823));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8844));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8866));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8904));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8915));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8925));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8946));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8956));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8966));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8978));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(8994));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(9005));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 814, DateTimeKind.Utc).AddTicks(9016));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 808, DateTimeKind.Utc).AddTicks(7260));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(1647));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(1678));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(1685));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(1689));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(1692));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 9, 12, 46, 24, 809, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Products_ProductId",
                table: "TransferItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Branches_FromBranchId",
                table: "Transfers",
                column: "FromBranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Branches_ToBranchId",
                table: "Transfers",
                column: "ToBranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
