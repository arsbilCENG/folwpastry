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
                name: "AdminWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletType = table.Column<int>(type: "integer", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminWallets", x => x.Id);
                });

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
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletType = table.Column<int>(type: "integer", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchWallets_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    TrackingType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    OpeningCashBalance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    ExpectedCashAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CashAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    PosAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    TotalCounted = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CashDifference = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    DifferenceNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TotalSalesRevenue = table.Column<decimal>(type: "numeric", nullable: false),
                    ReceiptPhotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CounterPhotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
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
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetRole = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SourceEntity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SourceEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceBranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceBranchName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceiptPhotoUrl = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransferNumber = table.Column<string>(type: "text", nullable: false),
                    SenderBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ShippedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CancellationReason = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceivedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CancelledByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Branches_ReceiverBranchId",
                        column: x => x.ReceiverBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Branches_SenderBranchId",
                        column: x => x.SenderBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Users_CancelledByUserId",
                        column: x => x.CancelledByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Users_ReceivedByUserId",
                        column: x => x.ReceivedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    WalletType = table.Column<int>(type: "integer", nullable: false),
                    SourceType = table.Column<int>(type: "integer", nullable: false),
                    SourceBranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceBranchWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceAdminWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetType = table.Column<int>(type: "integer", nullable: false),
                    TargetBranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetBranchWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetAdminWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchWalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_AdminWallets_AdminWalletId",
                        column: x => x.AdminWalletId,
                        principalTable: "AdminWallets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WalletTransactions_AdminWallets_SourceAdminWalletId",
                        column: x => x.SourceAdminWalletId,
                        principalTable: "AdminWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_AdminWallets_TargetAdminWalletId",
                        column: x => x.TargetAdminWalletId,
                        principalTable: "AdminWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_BranchWallets_BranchWalletId",
                        column: x => x.BranchWalletId,
                        principalTable: "BranchWallets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WalletTransactions_BranchWallets_SourceBranchWalletId",
                        column: x => x.SourceBranchWalletId,
                        principalTable: "BranchWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_BranchWallets_TargetBranchWalletId",
                        column: x => x.TargetBranchWalletId,
                        principalTable: "BranchWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Branches_SourceBranchId",
                        column: x => x.SourceBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Branches_TargetBranchId",
                        column: x => x.TargetBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    CounterSoldQuantity = table.Column<decimal>(type: "numeric", nullable: true),
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
                    SentQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AcceptedQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    RejectedQuantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    DeliveryRejectionReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RejectionPhotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AcceptedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AffectsStock = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransferId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferItems_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DemandId = table.Column<Guid>(type: "uuid", nullable: false),
                    DemandItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToBranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PhotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryReturns_Branches_FromBranchId",
                        column: x => x.FromBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryReturns_Branches_ToBranchId",
                        column: x => x.ToBranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryReturns_DemandItems_DemandItemId",
                        column: x => x.DemandItemId,
                        principalTable: "DemandItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryReturns_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryReturns_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AdminWallets",
                columns: new[] { "Id", "CreatedAt", "CurrentBalance", "DeletedAt", "IsDeleted", "UpdatedAt", "WalletType" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("77777777-7777-7777-7777-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "BranchType", "City", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111101"), 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Kırklareli Mutfak", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-1111-1111-1111-111111111102"), 1, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Edirne Mutfak", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-1111-1111-1111-111111111103"), 1, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Lüleburgaz Mutfak", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-1111-1111-1111-111111111104"), 2, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Kırklareli Tezgah", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-1111-1111-1111-111111111105"), 2, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Edirne Tezgah", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-1111-1111-1111-111111111106"), 2, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Lüleburgaz Tezgah", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "CakeOptions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "OptionType", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Kakaolu", 0, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Vanilyalı", 0, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Meyveli", 0, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Havuçlu", 0, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Muzlu", 0, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000006"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Limonlu", 0, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000007"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Çikolatalı", 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Muzlu", 1, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Frambuazlı", 1, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000010"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Vanilyalı", 1, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000011"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Karamelli", 1, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Fıstıklı", 1, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000013"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Beyaz Çikolatalı", 1, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000014"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Beyaz", 2, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000015"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Çikolata", 2, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Toz Pembe", 2, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Mavi", 2, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Kırmızı", 2, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Mor", 2, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Yeşil", 2, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000021"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Sarı", 2, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-000000000022"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Turuncu", 2, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEK", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "MAYALILAR", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTALAR", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "İÇECEK", 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "FIRIN", 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KAHVALTI", 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "HAMMADDE", 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CreatedAt", "DeletedAt", "Email", "FullName", "IsActive", "IsDeleted", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333301"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@pastryflow.com", "Admin Kullanıcı", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333308"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "sofor@pastryflow.com", "Şoför", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "BranchWallets",
                columns: new[] { "Id", "BranchId", "CreatedAt", "CurrentBalance", "DeletedAt", "IsDeleted", "UpdatedAt", "WalletType" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-000000000001"), new Guid("11111111-1111-1111-1111-111111111101"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("66666666-6666-6666-6666-000000000002"), new Guid("11111111-1111-1111-1111-111111111101"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("66666666-6666-6666-6666-000000000003"), new Guid("11111111-1111-1111-1111-111111111102"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("66666666-6666-6666-6666-000000000004"), new Guid("11111111-1111-1111-1111-111111111102"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("66666666-6666-6666-6666-000000000005"), new Guid("11111111-1111-1111-1111-111111111103"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("66666666-6666-6666-6666-000000000006"), new Guid("11111111-1111-1111-1111-111111111103"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("66666666-6666-6666-6666-000000000007"), new Guid("11111111-1111-1111-1111-111111111104"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("66666666-6666-6666-6666-000000000008"), new Guid("11111111-1111-1111-1111-111111111104"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("66666666-6666-6666-6666-000000000009"), new Guid("11111111-1111-1111-1111-111111111105"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("66666666-6666-6666-6666-000000000010"), new Guid("11111111-1111-1111-1111-111111111105"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("66666666-6666-6666-6666-000000000011"), new Guid("11111111-1111-1111-1111-111111111106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("66666666-6666-6666-6666-000000000012"), new Guid("11111111-1111-1111-1111-111111111106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0m, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "ProductType", "ProductionBranchId", "SortOrder", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000001"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEK DİLİM", 2, new Guid("11111111-1111-1111-1111-111111111101"), 2, 1, 45m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000002"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEK KALIP KÜÇÜK", 2, new Guid("11111111-1111-1111-1111-111111111101"), 3, 1, 180m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000003"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEK KALIP ORTA", 2, new Guid("11111111-1111-1111-1111-111111111101"), 4, 1, 250m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000004"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEK KALIP BÜYÜK", 2, new Guid("11111111-1111-1111-1111-111111111101"), 5, 1, 320m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000005"), new Guid("22222222-2222-2222-2222-222222222201"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEK MUFİN", 2, new Guid("11111111-1111-1111-1111-111111111101"), 6, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000006"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "AÇMA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 7, 1, 30m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000007"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "BÖREK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 8, 1, 55m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000008"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KAFKAS BÖREĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 9, 1, 65m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000009"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "İÇLİ SİMİT", 2, new Guid("11111111-1111-1111-1111-111111111103"), 10, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000010"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "İÇLİ SANDVİÇ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 11, 1, 60m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000011"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PİZZA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 12, 1, 80m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000012"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "POĞAÇA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 13, 1, 30m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000013"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "POĞAÇA İRAN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 14, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000014"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "POĞAÇA SAKALLI", 2, new Guid("11111111-1111-1111-1111-111111111103"), 15, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000015"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SİMİT", 2, new Guid("11111111-1111-1111-1111-111111111103"), 16, 1, 20m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000016"), new Guid("22222222-2222-2222-2222-222222222202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SU BÖREĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 17, 1, 70m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000017"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ACIBADEM", 2, new Guid("11111111-1111-1111-1111-111111111103"), 18, 2, 320m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000018"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "BEZE BÜYÜK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 19, 1, 45m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000019"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "BEZE KUTU", 2, new Guid("11111111-1111-1111-1111-111111111103"), 20, 1, 120m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000020"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "GÜLEN YÜZ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 21, 2, 280m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000021"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KANDİL SİMİDİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 22, 1, 25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000022"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE EKSTRA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 23, 2, 300m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000023"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE MUZ / BONCUK", 2, new Guid("11111111-1111-1111-1111-111111111103"), 24, 2, 280m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000024"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE SADE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 25, 2, 260m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000025"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE ŞAM", 2, new Guid("11111111-1111-1111-1111-111111111103"), 26, 2, 290m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000026"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE TAHİNLİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 27, 2, 270m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000027"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE TART", 2, new Guid("11111111-1111-1111-1111-111111111103"), 28, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000028"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURABİYE UN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 29, 2, 250m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000029"), new Guid("22222222-2222-2222-2222-222222222203"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KURU PASTA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 30, 2, 300m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000030"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ALMAN PASTASI", 2, new Guid("11111111-1111-1111-1111-111111111103"), 31, 1, 380m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000031"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "FİGÜR-BUDAPEŞTE-SEBASTİAN", 2, new Guid("11111111-1111-1111-1111-111111111103"), 32, 1, 450m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000032"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTA ADET", 2, new Guid("11111111-1111-1111-1111-111111111103"), 33, 1, 350m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000033"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTA B1", 2, new Guid("11111111-1111-1111-1111-111111111103"), 34, 1, 420m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000034"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTA CHEESECAKE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 35, 1, 400m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000035"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTA KG", 2, new Guid("11111111-1111-1111-1111-111111111103"), 36, 2, 280m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000036"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTA NO 0", 2, new Guid("11111111-1111-1111-1111-111111111103"), 37, 1, 280m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000037"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PASTA NO 1", 2, new Guid("11111111-1111-1111-1111-111111111103"), 38, 1, 350m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000038"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "RULO SARMA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 39, 1, 320m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000039"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SİPARİŞ PASTA", 2, new Guid("11111111-1111-1111-1111-111111111103"), 40, 1, 500m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000040"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SÜTLÜ TATLILAR", 2, new Guid("11111111-1111-1111-1111-111111111103"), 41, 1, 85m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000041"), new Guid("22222222-2222-2222-2222-222222222204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TRİLEÇE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 42, 1, 90m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "ProductType", "ProductionBranchId", "SortOrder", "TrackingType", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000042"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ÇAY SATIŞ", 2, null, 43, 2, 1, 20m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000043"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TÜRK KAHVESİ", 2, null, 44, 2, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000044"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ESPRESSO", 2, null, 45, 2, 1, 40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000045"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "BİTKİ ÇAYI", 2, null, 46, 2, 1, 25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000046"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "AYRAN", 2, null, 47, 1, 1, 25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000047"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "GAZLI İÇECEK", 2, null, 48, 1, 1, 30m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000048"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "LİMONATA", 2, null, 49, 1, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000049"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "MEYVE SUYU", 2, null, 50, 1, 1, 30m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000050"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SODA MEYVELİ", 2, null, 51, 1, 1, 25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000051"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SODA SADE", 2, null, 52, 1, 1, 20m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000052"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SU 0.5 L", 2, null, 53, 1, 1, 15m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "ProductType", "ProductionBranchId", "SortOrder", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[] { new Guid("44444444-4444-4444-4444-000000000053"), new Guid("22222222-2222-2222-2222-222222222205"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PİKNİK ÜRÜNLER", 2, new Guid("11111111-1111-1111-1111-111111111103"), 54, 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "ProductType", "ProductionBranchId", "SortOrder", "TrackingType", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000054"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Kahvaltı Tabağı", 2, null, 55, 2, 1, 250m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000055"), new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Serpme Kahvaltı", 2, null, 56, 2, 1, 500m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "ProductType", "ProductionBranchId", "SortOrder", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000056"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ÇAVDAR EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 57, 1, 45m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000057"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "EKŞİ MAYA 2'Lİ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 58, 1, 60m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000058"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "EKŞİ MAYA 3'LÜ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 59, 1, 85m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000059"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KEPEK EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 60, 1, 40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000060"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "MISIR EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 61, 1, 40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000061"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PİDE", 2, new Guid("11111111-1111-1111-1111-111111111102"), 62, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000062"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SANDVİÇ EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 63, 1, 30m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000063"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SİYEZ EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 64, 1, 50m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000064"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TAHİLLI EKMEK", 2, new Guid("11111111-1111-1111-1111-111111111102"), 65, 1, 45m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000065"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TAM BUĞDAY", 2, new Guid("11111111-1111-1111-1111-111111111102"), 66, 1, 40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000066"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TEKLİ BEYAZ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 67, 1, 25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000067"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ÇİFTLİ BEYAZ", 2, new Guid("11111111-1111-1111-1111-111111111102"), 68, 1, 40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000068"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "EKMEK BEYAZ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 69, 1, 25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000069"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "EKMEK ÇEŞİT", 2, new Guid("11111111-1111-1111-1111-111111111103"), 70, 1, 40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000070"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "GALETE", 2, new Guid("11111111-1111-1111-1111-111111111103"), 71, 1, 30m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000071"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "PİDE ÇİFTLİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 72, 1, 50m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000072"), new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TANDIR EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 73, 1, 35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000073"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "AYÇİÇEK YAĞI", 1, null, 74, 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000074"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KAKAO", 1, null, 75, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000075"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KABARTMA TOZU", 1, null, 76, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000076"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "KREMA", 1, null, 77, 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000077"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "MARGARİN", 1, null, 78, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000078"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "MAYA", 1, null, 79, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000079"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "NİŞASTA", 1, null, 80, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000080"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "ŞEKER", 1, null, 81, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000081"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "SÜT", 1, null, 82, 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000082"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TEREYAĞI", 1, null, 83, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000083"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "TUZ", 1, null, 84, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000084"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "UN", 1, null, 85, 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-000000000085"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "YUMURTA", 1, null, 86, 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CreatedAt", "DeletedAt", "Email", "FullName", "IsActive", "IsDeleted", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333302"), new Guid("11111111-1111-1111-1111-111111111101"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "kirklareli.mutfak@pastryflow.com", "Kırklareli Üretim", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333303"), new Guid("11111111-1111-1111-1111-111111111102"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "edirne.mutfak@pastryflow.com", "Edirne Üretim", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333304"), new Guid("11111111-1111-1111-1111-111111111103"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "luleburgaz.mutfak@pastryflow.com", "Lüleburgaz Üretim", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333305"), new Guid("11111111-1111-1111-1111-111111111104"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "kirklareli.tezgah@pastryflow.com", "Kırklareli Satış", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333306"), new Guid("11111111-1111-1111-1111-111111111105"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "edirne.tezgah@pastryflow.com", "Edirne Satış", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333307"), new Guid("11111111-1111-1111-1111-111111111106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "luleburgaz.tezgah@pastryflow.com", "Lüleburgaz Satış", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminWallets_WalletType",
                table: "AdminWallets",
                column: "WalletType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchWallets_BranchId_WalletType",
                table: "BranchWallets",
                columns: new[] { "BranchId", "WalletType" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReturns_CreatedAt",
                table: "DeliveryReturns",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReturns_DemandId",
                table: "DeliveryReturns",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReturns_DemandItemId",
                table: "DeliveryReturns",
                column: "DemandItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReturns_FromBranchId",
                table: "DeliveryReturns",
                column: "FromBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReturns_ProductId",
                table: "DeliveryReturns",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReturns_ToBranchId",
                table: "DeliveryReturns",
                column: "ToBranchId");

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
                name: "IX_Notifications_BranchId",
                table: "Notifications",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BranchId_IsRead",
                table: "Notifications",
                columns: new[] { "BranchId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedAt",
                table: "Notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsRead",
                table: "Notifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductionBranchId",
                table: "Products",
                column: "ProductionBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductId",
                table: "PurchaseItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseId",
                table: "PurchaseItems",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BranchId_PurchaseDate",
                table: "Purchases",
                columns: new[] { "BranchId", "PurchaseDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CreatedByUserId",
                table: "Purchases",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseNumber",
                table: "Purchases",
                column: "PurchaseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BranchId_ProductId",
                table: "Stocks",
                columns: new[] { "BranchId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_ProductId",
                table: "TransferItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_TransferId",
                table: "TransferItems",
                column: "TransferId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ReceiverBranchId",
                table: "Transfers",
                column: "ReceiverBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SenderBranchId",
                table: "Transfers",
                column: "SenderBranchId");

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
                name: "IX_WalletTransactions_AdminWalletId",
                table: "WalletTransactions",
                column: "AdminWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_BranchWalletId",
                table: "WalletTransactions",
                column: "BranchWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_CreatedByUserId",
                table: "WalletTransactions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_SourceAdminWalletId",
                table: "WalletTransactions",
                column: "SourceAdminWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_SourceBranchId",
                table: "WalletTransactions",
                column: "SourceBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_SourceBranchWalletId",
                table: "WalletTransactions",
                column: "SourceBranchWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_TargetAdminWalletId",
                table: "WalletTransactions",
                column: "TargetAdminWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_TargetBranchId",
                table: "WalletTransactions",
                column: "TargetBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_TargetBranchWalletId",
                table: "WalletTransactions",
                column: "TargetBranchWalletId");

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
                name: "CustomCakeOrders");

            migrationBuilder.DropTable(
                name: "DayClosingDetails");

            migrationBuilder.DropTable(
                name: "DeliveryReturns");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "TransferItems");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Wastes");

            migrationBuilder.DropTable(
                name: "CakeOptions");

            migrationBuilder.DropTable(
                name: "DayClosings");

            migrationBuilder.DropTable(
                name: "DemandItems");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "AdminWallets");

            migrationBuilder.DropTable(
                name: "BranchWallets");

            migrationBuilder.DropTable(
                name: "Demands");

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
