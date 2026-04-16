using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    City = table.Column<int>(type: "integer", nullable: false),
                    BranchType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    RelatedEntityType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RelatedEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransferNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FromBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DriverUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReceivedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Branches_FromBranchId",
                        column: x => x.FromBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Branches_ToBranchId",
                        column: x => x.ToBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductionBranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductType = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Branches_ProductionBranchId",
                        column: x => x.ProductionBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DemandNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SalesBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductionBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DriverUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReceivedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demands_Branches_ProductionBranchId",
                        column: x => x.ProductionBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_Branches_SalesBranchId",
                        column: x => x.SalesBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyStockSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    OpeningStock = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedFromDemands = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IncomingTransferQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OutgoingTransferQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DayWasteQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndOfDayCount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CarryOverQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndOfDayWaste = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CalculatedSales = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TransferItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransferId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Notes = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferItems_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wastes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    WasteType = table.Column<int>(type: "integer", nullable: false),
                    PhotoPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wastes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wastes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wastes_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DemandId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestedQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ApprovedQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RejectionReason = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ReviewedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandItems_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "BranchType", "City", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111101"), 1, 1, new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(614), true, "Kırklareli Mutfak", null },
                    { new Guid("11111111-1111-1111-1111-111111111102"), 1, 2, new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3021), true, "Edirne Mutfak", null },
                    { new Guid("11111111-1111-1111-1111-111111111103"), 1, 3, new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3025), true, "Lüleburgaz Mutfak", null },
                    { new Guid("11111111-1111-1111-1111-111111111104"), 2, 1, new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3027), true, "Kırklareli Tezgah", null },
                    { new Guid("11111111-1111-1111-1111-111111111105"), 2, 2, new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3029), true, "Edirne Tezgah", null },
                    { new Guid("11111111-1111-1111-1111-111111111106"), 2, 3, new DateTime(2026, 4, 16, 8, 44, 2, 17, DateTimeKind.Utc).AddTicks(3031), true, "Lüleburgaz Tezgah", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(3917), true, "KEK", 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9664), true, "EKMEK", 2, null },
                    { new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9682), true, "MAYALILAR", 3, null },
                    { new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9685), true, "KURABİYE", 4, null },
                    { new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9722), true, "PASTALAR", 5, null },
                    { new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9725), true, "İÇECEK", 6, null },
                    { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9728), true, "FIRIN", 7, null },
                    { new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 20, DateTimeKind.Utc).AddTicks(9730), true, "HAMMADDE", 8, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333301"), null, new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(4120), "admin@pastryflow.com", "Admin Kullanıcı", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 1, null },
                    { new Guid("33333333-3333-3333-3333-333333333308"), null, new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7834), "sofor@pastryflow.com", "Şoför", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 4, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "ProductType", "ProductionBranchId", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000001"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(3729), true, "KEK DİLİM", 2, new Guid("11111111-1111-1111-1111-111111111101"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000002"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(6989), true, "KEK KALIP BÜYÜK", 2, new Guid("11111111-1111-1111-1111-111111111101"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000003"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7002), true, "KEK KALIP KÜÇÜK", 2, new Guid("11111111-1111-1111-1111-111111111101"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000004"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7007), true, "KEK KALIP ORTA", 2, new Guid("11111111-1111-1111-1111-111111111101"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000005"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7012), true, "KEK MUFİN", 2, new Guid("11111111-1111-1111-1111-111111111101"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000006"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7024), true, "EKŞİ MAYA (3'LÜ)", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000007"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7119), true, "EKŞİ MAYA (2'Lİ)", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000008"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7137), true, "ÇAVDAR", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000009"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7142), true, "TAHILLI", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000010"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7151), true, "MISIR", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000011"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7155), true, "TEKLİ BEYAZ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000012"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7159), true, "ÇİFTLİ BEYAZ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000013"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7164), true, "KEPEK", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000014"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7168), true, "TAMBUĞDAY", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000015"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7172), true, "PİDE", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000016"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7176), true, "SANDVİÇ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000017"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7180), true, "SİYEZ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000018"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7191), true, "BÖREK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000019"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7195), true, "KAFKAS BÖREĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000020"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7206), true, "AÇMA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000021"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7210), true, "İÇLİ SİMİT", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000022"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7215), true, "PİZZA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000023"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7219), true, "POĞAÇA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000024"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7223), true, "POĞAÇA İRAN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000025"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7228), true, "POĞAÇA SAKALLI", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000026"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7234), true, "SİMİT", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000027"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7243), true, "SU BÖREĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000028"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7247), true, "İÇLİ SANDVİÇ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000029"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7259), true, "ACIBADEM", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000030"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7264), true, "BEZE BÜYÜK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000031"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7269), true, "BEZE KUTU", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000032"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7307), true, "GÜLEN YÜZ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000033"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7312), true, "KURABİYE EKSTRA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000034"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7321), true, "KURABİYE MUZ / BONCUK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000035"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7381), true, "KURABİYE SADE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000036"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7387), true, "KURABİYE ŞAM", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000037"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7392), true, "KURABİYE TAHİNLİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000038"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7397), true, "KURABİYE UN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000039"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7401), true, "KURU PASTA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000040"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7406), true, "KURABİYE TART", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000041"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7411), true, "KANDİL SİMİDİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000042"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7419), true, "ALMAN PASTASI", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000043"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7424), true, "RULO SARMA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000044"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7428), true, "PASTA ADET", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000045"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7432), true, "PASTA CHEESECAKE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000046"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7436), true, "PASTA B1", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000047"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7441), true, "PASTA NO 0", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000048"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7445), true, "PASTA NO 1", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000049"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7449), true, "FİGÜR-BUDAPEŞTE-SEBASTİAN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000050"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7455), true, "SÜTLÜ TATLILAR", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000051"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7460), true, "TRİLEÇE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000052"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7464), true, "SİPARİŞ PASTA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000053"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7469), true, "PASTA KG", 2, new Guid("11111111-1111-1111-1111-111111111103"), 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000054"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7475), true, "AYRAN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000055"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7480), true, "ÇAY SATIŞ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000056"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7484), true, "ESPRESSO", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000057"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7489), true, "GAZLI İÇECEK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000058"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7495), true, "LİMONATA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000059"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7499), true, "MEYVE SUYU", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000060"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7503), true, "SODA MEYVELİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000061"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7508), true, "SODA SADE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000062"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7512), true, "SU 0.5 L", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000063"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7516), true, "TÜRK KAHVESİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000064"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7520), true, "BİTKİ ÇAYI", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000065"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7525), true, "PİKNİK ÜRÜNLER", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000066"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7580), true, "KAHVALTI TABAĞI", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000067"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7586), true, "GALETE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000068"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7591), true, "EKMEK BEYAZ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000069"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7596), true, "EKMEK ÇEŞİT", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000070"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7600), true, "EKMEK KEPEK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000071"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7605), true, "EKMEK MISIR", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000072"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7609), true, "EKMEK SANDVİÇ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000073"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7613), true, "TAHILLI EKMEK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000074"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7619), true, "PİDE ÇİFTLİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000075"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7623), true, "TANDIR EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000076"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7629), true, "UN", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000077"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7634), true, "TUZ", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000078"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7638), true, "ŞEKER", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000079"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7642), true, "MAYA", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000080"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7646), true, "TEREYAĞI", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000081"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7650), true, "MARGARİN", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000082"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7656), true, "KAKAO", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000083"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7661), true, "KABARTMA TOZU", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000084"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7665), true, "NİŞASTA", 1, null, 2, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000085"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7670), true, "SÜT", 1, null, 3, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000086"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7674), true, "KREMA", 1, null, 3, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000087"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7678), true, "AYÇİÇEK YAĞI", 1, null, 3, null, null },
                    { new Guid("44444444-4444-4444-4444-000000000088"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 16, 8, 44, 2, 21, DateTimeKind.Utc).AddTicks(7683), true, "YUMURTA", 1, null, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333302"), new Guid("11111111-1111-1111-1111-111111111101"), new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7171), "kirklareli.mutfak@pastryflow.com", "Kırklareli Üretim", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 2, null },
                    { new Guid("33333333-3333-3333-3333-333333333303"), new Guid("11111111-1111-1111-1111-111111111102"), new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7810), "edirne.mutfak@pastryflow.com", "Edirne Üretim", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 2, null },
                    { new Guid("33333333-3333-3333-3333-333333333304"), new Guid("11111111-1111-1111-1111-111111111103"), new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7822), "luleburgaz.mutfak@pastryflow.com", "Lüleburgaz Üretim", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 2, null },
                    { new Guid("33333333-3333-3333-3333-333333333305"), new Guid("11111111-1111-1111-1111-111111111104"), new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7825), "kirklareli.tezgah@pastryflow.com", "Kırklareli Satış", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 3, null },
                    { new Guid("33333333-3333-3333-3333-333333333306"), new Guid("11111111-1111-1111-1111-111111111105"), new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7828), "edirne.tezgah@pastryflow.com", "Edirne Satış", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 3, null },
                    { new Guid("33333333-3333-3333-3333-333333333307"), new Guid("11111111-1111-1111-1111-111111111106"), new DateTime(2026, 4, 16, 8, 44, 2, 18, DateTimeKind.Utc).AddTicks(7831), "luleburgaz.tezgah@pastryflow.com", "Lüleburgaz Satış", true, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyStockSummaries_BranchId_ProductId_Date",
                table: "DailyStockSummaries",
                columns: new[] { "BranchId", "ProductId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyStockSummaries_ProductId",
                table: "DailyStockSummaries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandItems_DemandId",
                table: "DemandItems",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandItems_ProductId",
                table: "DemandItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_CreatedByUserId",
                table: "Demands",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_ProductionBranchId",
                table: "Demands",
                column: "ProductionBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_SalesBranchId",
                table: "Demands",
                column: "SalesBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductionBranchId",
                table: "Products",
                column: "ProductionBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_ProductId",
                table: "TransferItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_TransferId",
                table: "TransferItems",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromBranchId",
                table: "Transfers",
                column: "FromBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToBranchId",
                table: "Transfers",
                column: "ToBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wastes_BranchId",
                table: "Wastes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Wastes_CreatedByUserId",
                table: "Wastes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wastes_ProductId",
                table: "Wastes",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStockSummaries");

            migrationBuilder.DropTable(
                name: "DemandItems");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "TransferItems");

            migrationBuilder.DropTable(
                name: "Wastes");

            migrationBuilder.DropTable(
                name: "Demands");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
