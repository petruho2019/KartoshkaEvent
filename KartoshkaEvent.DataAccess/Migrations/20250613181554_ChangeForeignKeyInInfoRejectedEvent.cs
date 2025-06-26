using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeForeignKeyInInfoRejectedEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAddress_InfoRejectedEvent_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "KartoshkaEvent",
                table: "InfoRejectedEvent",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InfoRejectedEvent_AddressId",
                schema: "KartoshkaEvent",
                table: "InfoRejectedEvent",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoRejectedEvent_EventAddress_AddressId",
                schema: "KartoshkaEvent",
                table: "InfoRejectedEvent",
                column: "AddressId",
                principalSchema: "KartoshkaEvent",
                principalTable: "EventAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRejectedEvent_EventAddress_AddressId",
                schema: "KartoshkaEvent",
                table: "InfoRejectedEvent");

            migrationBuilder.DropIndex(
                name: "IX_InfoRejectedEvent_AddressId",
                schema: "KartoshkaEvent",
                table: "InfoRejectedEvent");

            migrationBuilder.DropIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "KartoshkaEvent",
                table: "InfoRejectedEvent");

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
    }
}
