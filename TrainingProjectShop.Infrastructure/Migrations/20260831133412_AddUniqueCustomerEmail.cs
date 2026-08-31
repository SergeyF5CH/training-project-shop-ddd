using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingProjectShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCustomerEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_customers_email",
                table: "customers",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customers_email",
                table: "customers");
        }
    }
}
