using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Setup1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_internetStatistics_Countries_CountryCode",
                table: "internetStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_internetStatistics",
                table: "internetStatistics");

            migrationBuilder.DropIndex(
                name: "IX_internetStatistics_CountryCode",
                table: "internetStatistics");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "internetStatistics");

            migrationBuilder.RenameTable(
                name: "internetStatistics",
                newName: "InternetStatistics");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "InternetStatistics",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternetStatistics",
                table: "InternetStatistics",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternetStatistics_Countries_Code",
                table: "InternetStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternetStatistics",
                table: "InternetStatistics");

            migrationBuilder.DropIndex(
                name: "IX_InternetStatistics_Code",
                table: "InternetStatistics");

            migrationBuilder.RenameTable(
                name: "InternetStatistics",
                newName: "internetStatistics");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "internetStatistics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "internetStatistics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_internetStatistics",
                table: "internetStatistics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_internetStatistics_CountryCode",
                table: "internetStatistics",
                column: "CountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_internetStatistics_Countries_CountryCode",
                table: "internetStatistics",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "Code");
        }
    }
}
