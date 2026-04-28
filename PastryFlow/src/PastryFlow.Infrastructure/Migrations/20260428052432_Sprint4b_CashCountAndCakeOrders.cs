using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint4b_CashCountAndCakeOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CashAmount",
                table: "DayClosings",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CashDifference",
                table: "DayClosings",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CounterPhotoUrl",
                table: "DayClosings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DifferenceNote",
                table: "DayClosings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExpectedCashAmount",
                table: "DayClosings",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PosAmount",
                table: "DayClosings",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptPhotoUrl",
                table: "DayClosings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCounted",
                table: "DayClosings",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CakeOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OptionType = table.Column<int>(type: "integer", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CakeOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomCakeOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductionBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CustomerPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServingSize = table.Column<int>(type: "integer", nullable: false),
                    CakeTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    InnerCreamId = table.Column<Guid>(type: "uuid", nullable: false),
                    OuterCreamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    ReferencePhotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StatusNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    StatusChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StatusChangedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCakeOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_Branches_ProductionBranchId",
                        column: x => x.ProductionBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_CakeOptions_CakeTypeId",
                        column: x => x.CakeTypeId,
                        principalTable: "CakeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_CakeOptions_InnerCreamId",
                        column: x => x.InnerCreamId,
                        principalTable: "CakeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_CakeOptions_OuterCreamId",
                        column: x => x.OuterCreamId,
                        principalTable: "CakeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomCakeOrders_Users_StatusChangedByUserId",
                        column: x => x.StatusChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 179, DateTimeKind.Utc).AddTicks(1372));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 179, DateTimeKind.Utc).AddTicks(4639));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 179, DateTimeKind.Utc).AddTicks(4650));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 179, DateTimeKind.Utc).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 179, DateTimeKind.Utc).AddTicks(4661));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 179, DateTimeKind.Utc).AddTicks(4667));

            migrationBuilder.InsertData(
                table: "CakeOptions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "OptionType", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-000000000001"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(6289), null, true, false, "Kakaolu", 0, 1, null },
                    { new Guid("55555555-5555-5555-5555-000000000002"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8133), null, true, false, "Vanilyalı", 0, 2, null },
                    { new Guid("55555555-5555-5555-5555-000000000003"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8150), null, true, false, "Meyveli", 0, 3, null },
                    { new Guid("55555555-5555-5555-5555-000000000004"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8154), null, true, false, "Havuçlu", 0, 4, null },
                    { new Guid("55555555-5555-5555-5555-000000000005"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8158), null, true, false, "Muzlu", 0, 5, null },
                    { new Guid("55555555-5555-5555-5555-000000000006"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8164), null, true, false, "Limonlu", 0, 6, null },
                    { new Guid("55555555-5555-5555-5555-000000000007"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8169), null, true, false, "Çikolatalı", 1, 1, null },
                    { new Guid("55555555-5555-5555-5555-000000000008"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8173), null, true, false, "Muzlu", 1, 2, null },
                    { new Guid("55555555-5555-5555-5555-000000000009"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8176), null, true, false, "Frambuazlı", 1, 3, null },
                    { new Guid("55555555-5555-5555-5555-000000000010"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8253), null, true, false, "Vanilyalı", 1, 4, null },
                    { new Guid("55555555-5555-5555-5555-000000000011"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8261), null, true, false, "Karamelli", 1, 5, null },
                    { new Guid("55555555-5555-5555-5555-000000000012"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8264), null, true, false, "Fıstıklı", 1, 6, null },
                    { new Guid("55555555-5555-5555-5555-000000000013"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8268), null, true, false, "Beyaz Çikolatalı", 1, 7, null },
                    { new Guid("55555555-5555-5555-5555-000000000014"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8272), null, true, false, "Toz Pembe", 2, 1, null },
                    { new Guid("55555555-5555-5555-5555-000000000015"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8275), null, true, false, "Beyaz", 2, 2, null },
                    { new Guid("55555555-5555-5555-5555-000000000016"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8279), null, true, false, "Çikolata", 2, 3, null },
                    { new Guid("55555555-5555-5555-5555-000000000017"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8282), null, true, false, "Mavi", 2, 4, null },
                    { new Guid("55555555-5555-5555-5555-000000000018"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8286), null, true, false, "Kırmızı", 2, 5, null },
                    { new Guid("55555555-5555-5555-5555-000000000019"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8292), null, true, false, "Mor", 2, 6, null },
                    { new Guid("55555555-5555-5555-5555-000000000020"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8295), null, true, false, "Yeşil", 2, 7, null },
                    { new Guid("55555555-5555-5555-5555-000000000021"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8298), null, true, false, "Sarı", 2, 8, null },
                    { new Guid("55555555-5555-5555-5555-000000000022"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8302), null, true, false, "Turuncu", 2, 9, null }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 182, DateTimeKind.Utc).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(2551));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(2570));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(2575));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(2580));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(2583));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(2586));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 183, DateTimeKind.Utc).AddTicks(7718));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1164));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1171));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1176));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1203));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1227));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1233));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1248));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1252));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1256));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1311));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1324));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1333));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1337));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1342));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1346));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1439));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1444));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1460));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1469));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1485));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1497));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1505));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1514));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1519));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1538));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1542));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1547));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1551));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1556));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1569));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1576));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1624));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1641));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1645));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1649));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1654));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1665));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1669));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1673));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1682));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1692));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1704));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1713));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1722));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1743));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1791));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1796));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1809));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1813));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1820));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1824));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1829));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1833));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1838));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1843));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(4829));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(7925));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(8572));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(8582));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(8585));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(8590));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(8594));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 180, DateTimeKind.Utc).AddTicks(8597));

            migrationBuilder.CreateIndex(
                name: "IX_CakeOptions_OptionType_Name",
                table: "CakeOptions",
                columns: new[] { "OptionType", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_BranchId",
                table: "CustomCakeOrders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_CakeTypeId",
                table: "CustomCakeOrders",
                column: "CakeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_CreatedByUserId",
                table: "CustomCakeOrders",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_DeliveryDate",
                table: "CustomCakeOrders",
                column: "DeliveryDate");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_InnerCreamId",
                table: "CustomCakeOrders",
                column: "InnerCreamId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_OrderNumber",
                table: "CustomCakeOrders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_OuterCreamId",
                table: "CustomCakeOrders",
                column: "OuterCreamId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_ProductionBranchId",
                table: "CustomCakeOrders",
                column: "ProductionBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_Status",
                table: "CustomCakeOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_StatusChangedByUserId",
                table: "CustomCakeOrders",
                column: "StatusChangedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomCakeOrders");

            migrationBuilder.DropTable(
                name: "CakeOptions");

            migrationBuilder.DropColumn(
                name: "CashAmount",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "CashDifference",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "CounterPhotoUrl",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "DifferenceNote",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "ExpectedCashAmount",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "PosAmount",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "ReceiptPhotoUrl",
                table: "DayClosings");

            migrationBuilder.DropColumn(
                name: "TotalCounted",
                table: "DayClosings");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 827, DateTimeKind.Utc).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 827, DateTimeKind.Utc).AddTicks(8753));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 827, DateTimeKind.Utc).AddTicks(8765));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 827, DateTimeKind.Utc).AddTicks(8768));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 827, DateTimeKind.Utc).AddTicks(8770));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 827, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(2950));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(8836));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(8851));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(8872));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(8882));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(8885));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 831, DateTimeKind.Utc).AddTicks(8888));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7155));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7169));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7174));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7179));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7216));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7238));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7251));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7258));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7267));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7271));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7321));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7337));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7348));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7352));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7372));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7381));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7389));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7394));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7405));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7417));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7423));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7428));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7438));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7442));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7447));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7454));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7458));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7463));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7468));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7475));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7479));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7484));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7495));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7499));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7504));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7513));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7536));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7545));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7550));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7563));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7568));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7572));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7577));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7588));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7592));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7618));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7624));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7659));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7663));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7684));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7689));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7698));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7703));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7715));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 832, DateTimeKind.Utc).AddTicks(7719));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(534));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4086));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4790));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4800));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4811));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4817));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 27, 15, 0, 18, 829, DateTimeKind.Utc).AddTicks(4858));
        }
    }
}
