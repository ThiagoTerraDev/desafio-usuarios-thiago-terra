using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_rest_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class MakeTokenCreatedAtRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Para registros existentes com TokenCreatedAt NULL, define a data atual
            migrationBuilder.AlterColumn<DateTime>(
                name: "TokenCreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TokenCreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
