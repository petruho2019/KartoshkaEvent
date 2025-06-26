using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceOfTicket",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "QuantityOfTickets",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Ticket_pkey", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EventAddress_Ticket_Id",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "Id",
                principalSchema: "KartoshkaEvent",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAddress_Ticket_Id",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "KartoshkaEvent");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceOfTicket",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "QuantityOfTickets",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
