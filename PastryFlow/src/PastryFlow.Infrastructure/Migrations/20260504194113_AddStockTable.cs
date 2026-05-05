using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(@"
                INSERT INTO ""Stocks"" (""Id"", ""BranchId"", ""ProductId"", ""CurrentQuantity"", ""CreatedAt"", ""UpdatedAt"", ""IsDeleted"")
                SELECT 
                    gen_random_uuid(),
                    sub.""BranchId"",
                    sub.""ProductId"",
                    sub.""CarryOverQuantity"",
                    NOW(),
                    NOW(),
                    false
                FROM (
                    SELECT 
                        dc.""BranchId"",
                        dd.""ProductId"", 
                        dd.""CarryOverQuantity"",
                        ROW_NUMBER() OVER (
                            PARTITION BY dc.""BranchId"", dd.""ProductId"" 
                            ORDER BY dc.""ClosedAt"" DESC
                        ) as rn
                    FROM ""DayClosingDetails"" dd
                    JOIN ""DayClosings"" dc ON dd.""DayClosingId"" = dc.""Id""
                    WHERE dc.""IsClosed"" = true
                      AND dd.""IsDeleted"" = false
                ) sub
                WHERE sub.rn = 1
                ON CONFLICT DO NOTHING;
            ");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 907, DateTimeKind.Utc).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 908, DateTimeKind.Utc).AddTicks(1455));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 908, DateTimeKind.Utc).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 908, DateTimeKind.Utc).AddTicks(1471));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 908, DateTimeKind.Utc).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 908, DateTimeKind.Utc).AddTicks(1516));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(8773));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(517));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(527));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(531));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(535));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(541));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(549));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(564));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(568));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(584));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(594));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(598));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(601));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(605));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 914, DateTimeKind.Utc).AddTicks(609));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 911, DateTimeKind.Utc).AddTicks(8407));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4921));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4941));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4950));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4953));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 912, DateTimeKind.Utc).AddTicks(9713));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3763));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3789));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3824));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3836));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3846));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3869));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3879));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3883));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3887));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3892));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3896));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3958));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3985));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3993));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(3998));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4002));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4007));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4016));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4021));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4032));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4037));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4044));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4053));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4058));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4063));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4074));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4085));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4094));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4112));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4119));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4137));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4167));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4173));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4179));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4185));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4220));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4228));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4233));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4238));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4244));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4275));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4283));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4289));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4294));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4298));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4303));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4308));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4312));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4316));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4347));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4368));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4372));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4382));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4387));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 913, DateTimeKind.Utc).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(5764));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9213));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9873));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9884));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9892));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 4, 19, 41, 10, 909, DateTimeKind.Utc).AddTicks(9897));

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BranchId_ProductId",
                table: "Stocks",
                columns: new[] { "BranchId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 191, DateTimeKind.Utc).AddTicks(7284));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 191, DateTimeKind.Utc).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 191, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 191, DateTimeKind.Utc).AddTicks(9268));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 191, DateTimeKind.Utc).AddTicks(9279));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 191, DateTimeKind.Utc).AddTicks(9281));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(91));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(94));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(97));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(109));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(113));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(117));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(120));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(124));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(127));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(183));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(186));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(193));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(196));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(199));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(202));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(207));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(213));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 196, DateTimeKind.Utc).AddTicks(221));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(3493));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8269));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8278));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8281));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8316));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8324));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8326));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(1697));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4684));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4702));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4705));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4723));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4734));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4744));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4752));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4756));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4760));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4763));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4766));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4772));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4779));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4786));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4803));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4806));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4828));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4859));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4868));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4878));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4884));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4887));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4892));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4895));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4902));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4905));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4911));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4941));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4945));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4950));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4957));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4964));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4969));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4973));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4983));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4987));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(4991));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5073));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5084));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5088));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5092));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5108));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5114));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5117));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5121));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5128));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5132));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5138));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5142));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5145));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5151));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5157));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5164));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5176));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5232));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5237));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5243));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5253));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5258));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5267));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 192, DateTimeKind.Utc).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 192, DateTimeKind.Utc).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 193, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 193, DateTimeKind.Utc).AddTicks(297));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 193, DateTimeKind.Utc).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 193, DateTimeKind.Utc).AddTicks(302));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 193, DateTimeKind.Utc).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 1, 5, 41, 39, 193, DateTimeKind.Utc).AddTicks(314));
        }
    }
}
