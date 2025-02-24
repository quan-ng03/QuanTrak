using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InternetStatistics",
                table: "InternetStatistics");

            migrationBuilder.DropIndex(
                name: "IX_InternetStatistics_CountryCode",
                table: "InternetStatistics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InternetStatistics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternetStatistics",
                table: "InternetStatistics",
                column: "CountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InternetStatistics",
                table: "InternetStatistics");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InternetStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternetStatistics",
                table: "InternetStatistics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InternetStatistics_CountryCode",
                table: "InternetStatistics",
                column: "CountryCode");
        }
    }
}
