// See https://aka.ms/new-console-template for more information
public class CombinedStockData
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public long MarketCapitalization { get; set; }
    public double Change1Week { get; set; }
    public double Change1Month { get; set; }
    public double Change6Months { get; set; }
    public double ChangeYearToDate { get; set; }
    public double Change1Year { get; set; }
    public double Change3Years { get; set; }
    public double Change5Years { get; set; }
    public string? AnalystRatings { get; set; }
    public int AnalystCount { get; set; }
    public double PriceTarget { get; set; }
    public double PriceTargetChange { get; set; }
    public double StockPrice { get; set; }
    public double PriceChange { get; set; }
    public string? IndustrySector { get; set; }
    public long TradingVolume { get; set; }
    public long EnterpriseValue { get; set; }
    public double PeForward { get; set; }
    public double PsRatio { get; set; }
    public double PbRatio { get; set; }
    public double PFcfRatio { get; set; }
    public long Revenue { get; set; }
    public long OperatingIncome { get; set; }
    public long NetIncome { get; set; }
    public long FreeCashFlow { get; set; }
    public double EarningsPerShare { get; set; }
    public double PriceEarningsRatio { get; set; }
    public double DividendPerShare { get; set; }
    public double DividendYield { get; set; }
    public double PayoutRatio { get; set; }
    public double DividendGrowth { get; set; }
    public string? PayoutFrequency { get; set; }

    public DateTime Date { get; set; } = DateTime.Now.Date;

    public CombinedStockData()
    {

    }

    public CombinedStockData(
        StockInfo stockInfo,
        ValuationMetrics valuationMetrics,
        FinancialMetrics financialMetrics,
        PerformanceMetrics performanceMetrics,
        AnalystInfo analystInfo,
        DividendInfo dividendInfo)
    {
        Symbol = stockInfo.Symbol;
        Name = stockInfo.Name;
        MarketCapitalization = stockInfo.MarketCapitalization ?? 0;
        StockPrice = stockInfo?.StockPrice ?? 0;
        PriceChange = stockInfo?.PriceChange ?? 0;
        IndustrySector = stockInfo.IndustrySector;
        TradingVolume = stockInfo.TradingVolume ?? 0;
        PriceEarningsRatio = stockInfo.PriceEarningsRatio ?? default;

        Change1Week = performanceMetrics?.Change1Week ?? default;
        Change1Month = performanceMetrics?.Change1Month ?? default;
        Change6Months = performanceMetrics?.Change6Months ?? default;
        ChangeYearToDate = performanceMetrics?.ChangeYearToDate ?? default;
        Change1Year = performanceMetrics?.Change1Year ?? default;
        Change3Years = performanceMetrics?.Change3Years ?? default;
        Change5Years = performanceMetrics?.Change5Years ?? default;

        EnterpriseValue = valuationMetrics?.EnterpriseValue ?? default;
        PeForward = valuationMetrics?.PeForward ?? default;
        PsRatio = valuationMetrics?.PsRatio ?? default;
        PbRatio = valuationMetrics?.PbRatio ?? default;
        PFcfRatio = valuationMetrics?.PFcfRatio ?? default;

        Revenue = financialMetrics?.Revenue ?? default;
        OperatingIncome = financialMetrics?.OperatingIncome ?? default;
        NetIncome = financialMetrics?.NetIncome ?? default;
        FreeCashFlow = financialMetrics?.FreeCashFlow ?? default;
        EarningsPerShare = financialMetrics?.EarningsPerShare ?? default;

        AnalystRatings = analystInfo?.AnalystRatings ?? default;
        AnalystCount = analystInfo?.AnalystCount ?? default;
        PriceTarget = analystInfo?.PriceTarget ?? default;
        PriceTargetChange = analystInfo?.PriceTargetChange ?? default;

        DividendPerShare = dividendInfo?.DividendPerShare ?? default;
        DividendYield = dividendInfo?.DividendYield ?? default;
        PayoutRatio = dividendInfo?.PayoutRatio ?? default;
        DividendGrowth = dividendInfo?.DividendGrowth ?? default;
        PayoutFrequency = dividendInfo?.PayoutFrequency ?? default;
    }
}