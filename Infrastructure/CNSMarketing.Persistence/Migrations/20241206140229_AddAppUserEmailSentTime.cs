using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNSMarketing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserEmailSentTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEmailSentTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEmailSentTime",
                table: "AspNetUsers");
        }
    }
}
