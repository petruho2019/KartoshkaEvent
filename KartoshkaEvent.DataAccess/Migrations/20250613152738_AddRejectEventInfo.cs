using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRejectEventInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "Mood",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "PriceOfTicket",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "QuantityOfTickets",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "Time",
                schema: "KartoshkaEvent",
                table: "EventTime",
                newName: "DateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                schema: "KartoshkaEvent",
                table: "EventTime",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Mood",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                type: "text",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "InfoRejectedEvent",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InfoRejectedEvent_pkey", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "EventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAddress_InfoRejectedEvent_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "EventId",
                principalSchema: "KartoshkaEvent",
                principalTable: "InfoRejectedEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAddress_InfoRejectedEvent_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropTable(
                name: "InfoRejectedEvent",
                schema: "KartoshkaEvent");

            migrationBuilder.DropIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                schema: "KartoshkaEvent",
                table: "EventTime");

            migrationBuilder.DropColumn(
                name: "Mood",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "PriceOfTicket",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "QuantityOfTickets",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "KartoshkaEvent",
                table: "EventTime",
                newName: "Time");

            migrationBuilder.AddColumn<string>(
                name: "Mood",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceOfTicket",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "QuantityOfTickets",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "EventId");
        }
    }
}
