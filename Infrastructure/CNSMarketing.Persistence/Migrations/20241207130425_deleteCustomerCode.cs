using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNSMarketing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteCustomerCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
