using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBDFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class LongInsteadOfIntMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PESEL",
                table: "Individual_Customers",
                type: "bigint",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<long>(
                name: "KRS",
                table: "Business_Customers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PESEL",
                table: "Individual_Customers",
                type: "int",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<int>(
                name: "KRS",
                table: "Business_Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
