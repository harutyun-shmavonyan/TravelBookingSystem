using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockanalysis.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CombinedStockData",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketCapitalization = table.Column<long>(type: "bigint", nullable: false),
                    Change1Week = table.Column<double>(type: "float", nullable: false),
                    Change1Month = table.Column<double>(type: "float", nullable: false),
                    Change6Months = table.Column<double>(type: "float", nullable: false),
                    ChangeYearToDate = table.Column<double>(type: "float", nullable: false),
                    Change1Year = table.Column<double>(type: "float", nullable: false),
                    Change3Years = table.Column<double>(type: "float", nullable: false),
                    Change5Years = table.Column<double>(type: "float", nullable: false),
                    AnalystRatings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalystCount = table.Column<int>(type: "int", nullable: false),
                    PriceTarget = table.Column<double>(type: "float", nullable: false),
                    PriceTargetChange = table.Column<double>(type: "float", nullable: false),
                    StockPrice = table.Column<double>(type: "float", nullable: false),
                    PriceChange = table.Column<double>(type: "float", nullable: false),
                    IndustrySector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradingVolume = table.Column<int>(type: "int", nullable: false),
                    EnterpriseValue = table.Column<long>(type: "bigint", nullable: false),
                    PeForward = table.Column<double>(type: "float", nullable: false),
                    PsRatio = table.Column<double>(type: "float", nullable: false),
                    PbRatio = table.Column<double>(type: "float", nullable: false),
                    PFcfRatio = table.Column<double>(type: "float", nullable: false),
                    Revenue = table.Column<long>(type: "bigint", nullable: false),
                    OperatingIncome = table.Column<long>(type: "bigint", nullable: false),
                    NetIncome = table.Column<long>(type: "bigint", nullable: false),
                    FreeCashFlow = table.Column<long>(type: "bigint", nullable: false),
                    EarningsPerShare = table.Column<double>(type: "float", nullable: false),
                    PriceEarningsRatio = table.Column<double>(type: "float", nullable: false),
                    DividendPerShare = table.Column<double>(type: "float", nullable: false),
                    DividendYield = table.Column<double>(type: "float", nullable: false),
                    PayoutRatio = table.Column<double>(type: "float", nullable: false),
                    DividendGrowth = table.Column<double>(type: "float", nullable: false),
                    PayoutFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinedStockData", x => new { x.Symbol, x.Date });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombinedStockData");
        }
    }
}
