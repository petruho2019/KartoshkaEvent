using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                schema: "KartoshkaEvent",
                table: "Event");

            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                schema: "KartoshkaEvent",
                table: "EventAddress",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                schema: "KartoshkaEvent",
                table: "EventAddress");

            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                schema: "KartoshkaEvent",
                table: "Event",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
