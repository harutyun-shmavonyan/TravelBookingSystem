using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockanalysis.Migrations
{
    /// <inheritdoc />
    public partial class CryptoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CombinedCryptoData",
                columns: table => new
                {
                    BaseCurrency = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCurrencyDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CryptoTotalRank = table.Column<int>(type: "int", nullable: false),
                    Vol24hCmc = table.Column<double>(type: "float", nullable: false),
                    MarketCapCalc = table.Column<double>(type: "float", nullable: false),
                    CloseChange24h = table.Column<double>(type: "float", nullable: false),
                    PerfW = table.Column<double>(type: "float", nullable: false),
                    Perf1M = table.Column<double>(type: "float", nullable: false),
                    Perf3M = table.Column<double>(type: "float", nullable: false),
                    Perf6M = table.Column<double>(type: "float", nullable: false),
                    PerfYTD = table.Column<double>(type: "float", nullable: false),
                    PerfY = table.Column<double>(type: "float", nullable: false),
                    Perf5Y = table.Column<double>(type: "float", nullable: false),
                    PerfAll = table.Column<double>(type: "float", nullable: false),
                    VolatilityD = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinedCryptoData", x => new { x.BaseCurrency, x.Date });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombinedCryptoData");
        }
    }
}
