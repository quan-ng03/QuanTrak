using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternetStatistics_Countries_Code",
                table: "InternetStatistics");

            migrationBuilder.DropIndex(
                name: "IX_InternetStatistics_Code",
                table: "InternetStatistics");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "InternetStatistics");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "InternetStatistics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InternetStatistics_CountryCode",
                table: "InternetStatistics",
                column: "CountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_InternetStatistics_Countries_CountryCode",
                table: "InternetStatistics",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternetStatistics_Countries_CountryCode",
                table: "InternetStatistics");

            migrationBuilder.DropIndex(
                name: "IX_InternetStatistics_CountryCode",
                table: "InternetStatistics");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "InternetStatistics");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "InternetStatistics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternetStatistics_Code",
                table: "InternetStatistics",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_InternetStatistics_Countries_Code",
                table: "InternetStatistics",
                column: "Code",
                principalTable: "Countries",
                principalColumn: "Code");
        }
    }
}
