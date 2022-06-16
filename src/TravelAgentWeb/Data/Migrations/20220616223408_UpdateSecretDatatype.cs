using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgentWeb.Data.Migrations
{
    public partial class UpdateSecretDatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Secret",
                table: "SubscriptionSecrets",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Secret",
                table: "SubscriptionSecrets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
