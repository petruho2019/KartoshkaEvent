using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "KartoshkaEvent",
                table: "Ticket",
                newName: "TotalQuantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "KartoshkaEvent",
                table: "Ticket",
                newName: "PriceByTicket");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                schema: "KartoshkaEvent",
                table: "Ticket",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "PriceByTicket",
                schema: "KartoshkaEvent",
                table: "Ticket",
                newName: "Price");
        }
    }
}
