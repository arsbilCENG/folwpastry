using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint6a_ProductTrackingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackingType",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222209"), new DateTime(2026, 5, 1, 5, 41, 39, 194, DateTimeKind.Utc).AddTicks(8326), null, true, false, "KAHVALTI", 8, null });

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
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5073), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000055"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5080), 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000056"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5084), 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000057"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5088), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000058"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5092), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000059"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5096), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000060"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5100), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000061"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5104), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000062"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5108), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000063"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5114), 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000064"),
                columns: new[] { "CreatedAt", "TrackingType" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5117), 2 });

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
                columns: new[] { "CategoryId", "CreatedAt", "Name", "ProductionBranchId", "TrackingType" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222209"), new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5128), "Kahvaltı Tabağı", null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                columns: new[] { "CategoryId", "CreatedAt", "Name", "ProductionBranchId", "TrackingType" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222209"), new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5132), "Serpme Kahvaltı", null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5138), "GALETE" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5142), "EKMEK BEYAZ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5145), "EKMEK ÇEŞİT" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5151), "EKMEK KEPEK" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5154), "EKMEK MISIR" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5157), "EKMEK SANDVİÇ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5161), "TAHILLI EKMEK" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5164), "PİDE ÇİFTLİ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                columns: new[] { "CategoryId", "CreatedAt", "Name", "ProductType", "ProductionBranchId", "Unit" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5168), "TANDIR EKMEĞİ", 2, new Guid("11111111-1111-1111-1111-111111111103"), 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5173), "UN" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5176), "TUZ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5232), "ŞEKER" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5237), "MAYA" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5240), "TEREYAĞI" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5243), "MARGARİN" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5247), "KAKAO" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5250), "KABARTMA TOZU" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                columns: new[] { "CreatedAt", "Name", "Unit" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5253), "NİŞASTA", 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5258), "SÜT" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5263), "KREMA" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                columns: new[] { "CreatedAt", "Name", "Unit" },
                values: new object[] { new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5267), "AYÇİÇEK YAĞI", 3 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "ProductType", "ProductionBranchId", "SortOrder", "Unit", "UnitPrice", "UpdatedAt" },
                values: new object[] { new Guid("44444444-4444-4444-4444-000000000089"), new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 5, 1, 5, 41, 39, 195, DateTimeKind.Utc).AddTicks(5271), null, true, false, "YUMURTA", 1, null, 0, 1, null, null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222209"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000089"));

            migrationBuilder.DropColumn(
                name: "TrackingType",
                table: "Products");

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

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(6289));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8133));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8150));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8154));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8164));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8169));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8173));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8253));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8261));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8268));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8272));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8275));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8292));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8295));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "CakeOptions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(8302));

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
                columns: new[] { "CategoryId", "CreatedAt", "Name", "ProductionBranchId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222206"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1692), "KAHVALTI TABAĞI", new Guid("11111111-1111-1111-1111-111111111103") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000067"),
                columns: new[] { "CategoryId", "CreatedAt", "Name", "ProductionBranchId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222207"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1700), "GALETE", new Guid("11111111-1111-1111-1111-111111111103") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000068"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1704), "EKMEK BEYAZ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000069"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1709), "EKMEK ÇEŞİT" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000070"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1713), "EKMEK KEPEK" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000071"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1717), "EKMEK MISIR" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000072"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1722), "EKMEK SANDVİÇ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000073"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1726), "TAHILLI EKMEK" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000074"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1730), "PİDE ÇİFTLİ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000075"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1736), "TANDIR EKMEĞİ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000076"),
                columns: new[] { "CategoryId", "CreatedAt", "Name", "ProductType", "ProductionBranchId", "Unit" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222208"), new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1743), "UN", 1, null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000077"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1791), "TUZ" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000078"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1796), "ŞEKER" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000079"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1800), "MAYA" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000080"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1805), "TEREYAĞI" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000081"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1809), "MARGARİN" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000082"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1813), "KAKAO" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000083"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1820), "KABARTMA TOZU" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000084"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1824), "NİŞASTA" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000085"),
                columns: new[] { "CreatedAt", "Name", "Unit" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1829), "SÜT", 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000086"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1833), "KREMA" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000087"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1838), "AYÇİÇEK YAĞI" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000088"),
                columns: new[] { "CreatedAt", "Name", "Unit" },
                values: new object[] { new DateTime(2026, 4, 28, 5, 24, 30, 184, DateTimeKind.Utc).AddTicks(1843), "YUMURTA", 1 });

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
        }
    }
}
