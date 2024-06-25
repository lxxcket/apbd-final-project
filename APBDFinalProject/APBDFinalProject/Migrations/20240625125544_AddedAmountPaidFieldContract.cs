using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBDFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedAmountPaidFieldContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSigned",
                table: "Contracts");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Contracts");

            migrationBuilder.AddColumn<bool>(
                name: "IsSigned",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
