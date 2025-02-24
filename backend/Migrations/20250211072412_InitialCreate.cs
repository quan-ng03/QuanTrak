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
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "internetStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearWB = table.Column<int>(type: "int", nullable: true),
                    PercentWB = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    YearITU = table.Column<int>(type: "int", nullable: true),
                    PercentITU = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    YearCIA = table.Column<int>(type: "int", nullable: true),
                    PopulationCIA = table.Column<long>(type: "bigint", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_internetStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_internetStatistics_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_internetStatistics_CountryCode",
                table: "internetStatistics",
                column: "CountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "internetStatistics");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
