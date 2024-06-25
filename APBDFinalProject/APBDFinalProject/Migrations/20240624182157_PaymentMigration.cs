using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBDFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Softwares",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdCustomer",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments",
                column: "IdCustomer",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_IdCustomer",
                table: "Payments",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_IdCustomer",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "IdCustomer",
                table: "Payments");
        }
    }
}
