using Microsoft.EntityFrameworkCore.Migrations;

namespace Transferencia.Infra.Data.Migrations
{
    public partial class Migration_Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DebitCompleted",
                table: "Transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReverseTransferCompleted",
                table: "Transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebitCompleted",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReverseTransferCompleted",
                table: "Transactions");
        }
    }
}
