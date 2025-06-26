using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KartoshkaEvent");

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Event_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    NickName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventAddress",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    NumberOfhouse = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EventAddress_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventAddress_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "KartoshkaEvent",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventImage",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EventImage_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventImage_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "KartoshkaEvent",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreatedByRemoteIp = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Revoked = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Refreshtoken_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "KartoshkaEvent",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTime",
                schema: "KartoshkaEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EventTime_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTime_EventAddress_Id",
                        column: x => x.Id,
                        principalSchema: "KartoshkaEvent",
                        principalTable: "EventAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventAddress_EventId",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventImage_EventId",
                schema: "KartoshkaEvent",
                table: "EventImage",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_OwnerId",
                schema: "KartoshkaEvent",
                table: "RefreshToken",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventImage",
                schema: "KartoshkaEvent");

            migrationBuilder.DropTable(
                name: "EventTime",
                schema: "KartoshkaEvent");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "KartoshkaEvent");

            migrationBuilder.DropTable(
                name: "EventAddress",
                schema: "KartoshkaEvent");

            migrationBuilder.DropTable(
                name: "User",
                schema: "KartoshkaEvent");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "KartoshkaEvent");
        }
    }
}
