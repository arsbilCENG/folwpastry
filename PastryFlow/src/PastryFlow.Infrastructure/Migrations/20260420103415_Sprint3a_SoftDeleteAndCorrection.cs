using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint3a_SoftDeleteAndCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStockSummaries");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Wastes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Wastes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Transfers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Transfers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TransferItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TransferItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Demands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Demands",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DemandItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DemandItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Branches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Branches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DayClosings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsOpened = table.Column<bool>(type: "boolean", nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OpenedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClosedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayClosings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayClosings_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayClosings_Users_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayClosings_Users_OpenedByUserId",
                        column: x => x.OpenedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayClosingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayClosingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    OpeningStock = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedFromDemands = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IncomingTransferQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OutgoingTransferQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DayWasteQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndOfDayCount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CarryOverQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndOfDayWaste = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CalculatedSales = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OriginalEndOfDayCount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    OriginalCarryOverQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CorrectedEndOfDayCount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CorrectedCarryOverQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CorrectionReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CorrectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CorrectedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayClosingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayClosingDetails_DayClosings_DayClosingId",
                        column: x => x.DayClosingId,
                        principalTable: "DayClosings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayClosingDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayClosingDetails_Users_CorrectedByUserId",
                        column: x => x.CorrectedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(3639), null, false });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5385), null, false });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5387), null, false });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5389), null, false });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5391), null, false });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 874, DateTimeKind.Utc).AddTicks(5403), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222201"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 876, DateTimeKind.Utc).AddTicks(8030), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222203"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2519), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2530), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2532), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2535), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2537), null, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(2539), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(5542), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7888), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7899), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7904), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7908), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7918), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7922), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7925), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7942), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7947), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7951), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7954), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7958), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7962), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7966), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7969), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7975), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7981), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7990), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7994), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(7997), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8011), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8015), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8018), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8024), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8028), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8034), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8037), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8045), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8077), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8081), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8086), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8091), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8095), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8099), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8103), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8106), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8110), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8114), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8117), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8123), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8129), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8133), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8136), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8141), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8144), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8148), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8151), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000049"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8164), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000050"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8168), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000051"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8172), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000052"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8175), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000053"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8179), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000054"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8185), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8189), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8192), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8198), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8201), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8205), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8209), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8212), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8216), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8219), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8223), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000065"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8228), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000066"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8233), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8238), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8241), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8245), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8248), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8252), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8255), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8261), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8265), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8273), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8278), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8282), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8286), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8290), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8293), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8298), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8302), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8305), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8309), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8314), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8317), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8321), null, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 877, DateTimeKind.Utc).AddTicks(8325), null, false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333301"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(2734), null, false, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(4941), null, false, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5440), null, false, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5446), null, false, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5448), null, false, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5450), null, false, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5452), null, false, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "Role" },
                values: new object[] { new DateTime(2026, 4, 20, 10, 34, 13, 875, DateTimeKind.Utc).AddTicks(5461), null, false, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_DayClosingDetails_CorrectedByUserId",
                table: "DayClosingDetails",
                column: "CorrectedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DayClosingDetails_DayClosingId_ProductId",
                table: "DayClosingDetails",
                columns: new[] { "DayClosingId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayClosingDetails_ProductId",
                table: "DayClosingDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DayClosings_BranchId_Date",
                table: "DayClosings",
                columns: new[] { "BranchId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayClosings_ClosedByUserId",
                table: "DayClosings",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DayClosings_OpenedByUserId",
                table: "DayClosings",
                column: "OpenedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayClosingDetails");

            migrationBuilder.DropTable(
                name: "DayClosings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Wastes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Wastes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DemandItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DemandItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Branches");

            migrationBuilder.CreateTable(
                name: "DailyStockSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CalculatedSales = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CarryOverQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClosedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DayWasteQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndOfDayCount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndOfDayWaste = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IncomingTransferQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    OpeningStock = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OutgoingTransferQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedFromDemands = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStockSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyStockSummaries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyStockSummaries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6892));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222204"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6904));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222205"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6987));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222206"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6990));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222207"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222208"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 400, DateTimeKind.Utc).AddTicks(7001));

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
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3545));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3555));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3558));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3566));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3571));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3574));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3580));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3589));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3592));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 13, 33, 43, 401, DateTimeKind.Utc).AddTicks(3595));

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
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(4846), 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333302"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(7525), 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333303"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8064), 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333304"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8072), 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333305"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8075), 3 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333306"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8087), 3 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333307"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8090), 3 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333308"),
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 4, 17, 13, 33, 43, 398, DateTimeKind.Utc).AddTicks(8092), 4 });

            migrationBuilder.CreateIndex(
                name: "IX_DailyStockSummaries_BranchId_ProductId_Date",
                table: "DailyStockSummaries",
                columns: new[] { "BranchId", "ProductId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyStockSummaries_ProductId",
                table: "DailyStockSummaries",
                column: "ProductId");
        }
    }
}
