using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "InternetStatistics",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "text", nullable: false),
                    PercentWB = table.Column<decimal>(type: "numeric", nullable: true),
                    YearWB = table.Column<int>(type: "integer", nullable: true),
                    PercentITU = table.Column<decimal>(type: "numeric", nullable: true),
                    YearITU = table.Column<int>(type: "integer", nullable: true),
                    PopulationCIA = table.Column<long>(type: "bigint", nullable: true),
                    YearCIA = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetStatistics", x => x.CountryCode);
                    table.ForeignKey(
                        name: "FK_InternetStatistics_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternetStatistics");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
