// See https://aka.ms/new-console-template for more information
using CsvHelper;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class StockAnalysis
{
    public async Task ExecuteAsync()
    {
        var client = new HttpClient();

        var waitingPeriod = TimeSpan.FromSeconds(1);

        string filePath = $"C:\\Users\\Harutyun\\Desktop\\stockanalysis.com\\{DateTime.Now.ToString("yyyy-MMM-dd")}.csv";

        var stockInfos = await FetchStockInfoAsync();
        await Task.Delay(waitingPeriod);

        var dividentInfos = await FetchDataAsync<DividendInfo>("https://stockanalysis.com/api/screener/s/bd/dps+dividendYield+payoutRatio+dividendGrowth+payoutFrequency.json");
        await Task.Delay(waitingPeriod);

        var analystInfos = await FetchDataAsync<AnalystInfo>("https://stockanalysis.com/api/screener/s/bd/analystRatings+analystCount+priceTarget+priceTargetChange.json");
        await Task.Delay(waitingPeriod);

        var performanceMetrics = await FetchDataAsync<PerformanceMetrics>("https://stockanalysis.com/api/screener/s/bd/ch1w+ch1m+ch6m+chYTD+ch1y+ch3y+ch5y.json");
        await Task.Delay(waitingPeriod);

        var financialMetrics = await FetchDataAsync<FinancialMetrics>("https://stockanalysis.com/api/screener/s/bd/revenue+operatingIncome+netIncome+fcf+eps.json");
        await Task.Delay(waitingPeriod);

        var valuationMetrics = await FetchDataAsync<ValuationMetrics>("https://stockanalysis.com/api/screener/s/bd/enterpriseValue+peForward+psRatio+pbRatio+pFcfRatio.json");
        await Task.Delay(waitingPeriod);

        var combinedStockData = stockInfos.Select(stockInfo =>
        {
            var dividentInfo = dividentInfos.GetValueOrDefault(stockInfo.Symbol);
            var analystInfo = analystInfos.GetValueOrDefault(stockInfo.Symbol);
            var performanceMetric = performanceMetrics.GetValueOrDefault(stockInfo.Symbol);
            var financialMetric = financialMetrics.GetValueOrDefault(stockInfo.Symbol);
            var valuationMetric = valuationMetrics.GetValueOrDefault(stockInfo.Symbol);

            return new CombinedStockData(
                stockInfo,
                valuationMetric,
                financialMetric,
                performanceMetric,
                analystInfo,
                dividentInfo);
        }).ToArray();

        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(combinedStockData);
        }

        using (var context = new StockAnalysisContext())
        {
            context.CombinedStockData.AddRange(combinedStockData);
            await context.SaveChangesAsync();
        }

        async Task<Dictionary<string, T>> FetchDataAsync<T>(string url)
        {
            await Console.Out.WriteLineAsync($"Fetching {typeof(T)}");
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(body)["data"]["data"];
            return data.ToObject<Dictionary<string, T>>();
        }

        async Task<StockInfo[]> FetchStockInfoAsync()
        {
            var screenerResponse = await client.GetAsync("https://stockanalysis.com/stocks/screener/");
            var screenerHtml = await screenerResponse.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(screenerHtml);

            var t = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div[1]/div[2]/main/div[3]/div[2]/table/tbody");

            var body = htmlDoc.DocumentNode
                .SelectSingleNode("//script[contains(text(), 'const data')]")
                .InnerText
                .Split("const data = ")[1]
                .Replace("const data = ", "")
                .Split("Promise.all")[0]
                .Split(",data:")[1]
                .Split(",fullWidth:false")[0]
                .Replace(";", "")
                .Replace("s:", @"""s"":")
                .Replace("n:", @"""n"":")
                .Replace("marketCap:", @"""marketCap"":")
                .Replace("price:", @"""price"":")
                .Replace("change:", @"""change"":")
                .Replace("industry:", @"""industry"":")
                .Replace("volume:", @"""volume"":")
                .Replace("peRatio:", @"""peRatio"":")
                .Replace(":-.", ":-0.")
                .Replace(":.", ":0.");
            //.Replace(@"\""", @"""").Replace("\"{\"", @"{""").Replace("}\"", @"}");

            var data = JArray.Parse(body);
            return data.ToObject<StockInfo[]>() ?? new StockInfo[0];
        }
    }
}
