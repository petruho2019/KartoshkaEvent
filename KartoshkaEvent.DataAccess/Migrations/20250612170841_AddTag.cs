using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Mood",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Tag_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTag",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    EventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTag", x => new { x.EventsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_EventTag_Event_EventsId",
                        column: x => x.EventsId,
                        principalSchema: "KartoshkaEvent",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalSchema: "KartoshkaEvent",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_OwnerId",
                schema: "KartoshkaEvent",
                table: "Event",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTag_TagsId",
                schema: "KartoshkaEvent",
                table: "EventTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_User_OwnerId",
                schema: "KartoshkaEvent",
                table: "Event",
                column: "OwnerId",
                principalSchema: "KartoshkaEvent",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_User_OwnerId",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.DropTable(
                name: "EventTag",
                schema: "KartoshkaEvent");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "KartoshkaEvent");

            migrationBuilder.DropIndex(
                name: "IX_Event_OwnerId",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Mood",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "OwnerId",
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
        }
    }
}
