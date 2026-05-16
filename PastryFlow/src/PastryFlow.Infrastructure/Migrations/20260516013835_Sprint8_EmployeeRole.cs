using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint8_EmployeeRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BranchId", "CreatedAt", "DeletedAt", "Email", "FullName", "IsActive", "IsDeleted", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333309"), new Guid("11111111-1111-1111-1111-111111111104"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "kirklareli.calisan@pastryflow.com", "Kırklareli Çalışan", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333310"), new Guid("11111111-1111-1111-1111-111111111105"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "edirne.calisan@pastryflow.com", "Edirne Çalışan", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333311"), new Guid("11111111-1111-1111-1111-111111111106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "luleburgaz.calisan@pastryflow.com", "Lüleburgaz Çalışan", true, false, "$2a$11$NleqkNOCjv3N1YWwrJYYfuHYXvpO5vaE1.It3fUTKLgHrTkXCABWO", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333309"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333310"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333311"));
        }
    }
}
