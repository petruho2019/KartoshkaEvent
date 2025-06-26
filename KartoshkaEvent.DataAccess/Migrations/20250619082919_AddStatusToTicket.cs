using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartoshkaEvent.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "KartoshkaEvent",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "KartoshkaEvent",
                table: "Ticket",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
