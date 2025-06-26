using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyEventTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAddress_Ticket_Id",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTime_EventAddress_Id",
                schema: "KartoshkaEvent",
                table: "EventTime");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EventLocationId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                schema: "KartoshkaEvent",
                table: "EventTime",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TicketInfo",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TicketInfo_pkey", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BuyerId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventLocationId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                column: "EventLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTime_LocationId",
                schema: "KartoshkaEvent",
                table: "EventTime",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAddress_TicketInfo_Id",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "Id",
                principalSchema: "KartoshkaEvent",
                principalTable: "TicketInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTime_EventAddress_LocationId",
                schema: "KartoshkaEvent",
                table: "EventTime",
                column: "LocationId",
                principalSchema: "KartoshkaEvent",
                principalTable: "EventAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_EventAddress_EventLocationId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                column: "EventLocationId",
                principalSchema: "KartoshkaEvent",
                principalTable: "EventAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Event_EventId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                column: "EventId",
                principalSchema: "KartoshkaEvent",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_BuyerId",
                schema: "KartoshkaEvent",
                table: "Ticket",
                column: "BuyerId",
                principalSchema: "KartoshkaEvent",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAddress_TicketInfo_Id",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTime_EventAddress_LocationId",
                schema: "KartoshkaEvent",
                table: "EventTime");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_EventAddress_EventLocationId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Event_EventId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_BuyerId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketInfo",
                schema: "KartoshkaEvent");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_BuyerId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_EventId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_EventLocationId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_EventTime_LocationId",
                schema: "KartoshkaEvent",
                table: "EventTime");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EventLocationId",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "KartoshkaEvent",
                table: "EventTime");

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_EventAddress_Ticket_Id",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "Id",
                principalSchema: "KartoshkaEvent",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTime_EventAddress_Id",
                schema: "KartoshkaEvent",
                table: "EventTime",
                column: "Id",
                principalSchema: "KartoshkaEvent",
                principalTable: "EventAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
