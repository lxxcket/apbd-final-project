using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBDFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class PaymentConstraintModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments",
                column: "IdCustomer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments",
                column: "IdCustomer",
                unique: true);
        }
    }
}
