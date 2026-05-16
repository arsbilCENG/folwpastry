using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sprint8_CakeOrderPaymentAndReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DepositAmount",
                table: "CustomCakeOrders",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepositCollectedByUserId",
                table: "CustomCakeOrders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepositPaidAt",
                table: "CustomCakeOrders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepositPaymentMethod",
                table: "CustomCakeOrders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPaymentAmount",
                table: "CustomCakeOrders",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FinalPaymentCollectedByUserId",
                table: "CustomCakeOrders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalPaymentMethod",
                table: "CustomCakeOrders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalPaymentPaidAt",
                table: "CustomCakeOrders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_DepositCollectedByUserId",
                table: "CustomCakeOrders",
                column: "DepositCollectedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCakeOrders_FinalPaymentCollectedByUserId",
                table: "CustomCakeOrders",
                column: "FinalPaymentCollectedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCakeOrders_Users_DepositCollectedByUserId",
                table: "CustomCakeOrders",
                column: "DepositCollectedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCakeOrders_Users_FinalPaymentCollectedByUserId",
                table: "CustomCakeOrders",
                column: "FinalPaymentCollectedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomCakeOrders_Users_DepositCollectedByUserId",
                table: "CustomCakeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomCakeOrders_Users_FinalPaymentCollectedByUserId",
                table: "CustomCakeOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomCakeOrders_DepositCollectedByUserId",
                table: "CustomCakeOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomCakeOrders_FinalPaymentCollectedByUserId",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "DepositCollectedByUserId",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "DepositPaidAt",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "DepositPaymentMethod",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "FinalPaymentAmount",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "FinalPaymentCollectedByUserId",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "FinalPaymentMethod",
                table: "CustomCakeOrders");

            migrationBuilder.DropColumn(
                name: "FinalPaymentPaidAt",
                table: "CustomCakeOrders");
        }
    }
}
