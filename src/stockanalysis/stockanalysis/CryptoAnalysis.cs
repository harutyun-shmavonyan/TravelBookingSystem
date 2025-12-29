// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;
using System.Text;

public class CryptoAnalysis
{
    public async Task ExecuteAsync()
    {
        var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Post, "https://scanner.tradingview.com/coin/scan")
        {
            Content = new StringContent(@"
            {
                ""columns"": [
                    ""base_currency"",
                    ""base_currency_desc"",
                    ""crypto_total_rank"",
                    ""24h_vol_cmc"",
                    ""market_cap_calc"",
                    ""24h_close_change|5"",
                    ""Perf.W"",
                    ""Perf.1M"",
                    ""Perf.3M"",
                    ""Perf.6M"",
                    ""Perf.YTD"",
                    ""Perf.Y"",
                    ""Perf.5Y"",
                    ""Perf.All"",
                    ""Volatility.D""
                ],
                ""ignore_unknown_fields"": false,
                ""options"": {
                    ""lang"": ""en""
                },
                ""range"": [
                    0,
                    1000
                ],
                ""sort"": {
                    ""sortBy"": ""crypto_total_rank"",
                    ""sortOrder"": ""asc"",
                    ""nullsFirst"": false
                },
                ""preset"": ""coin_market_cap_rank""
            }", Encoding.UTF8, "text/plain")
        };

        var response = await client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var arr = (JArray)JObject.Parse(responseString)["data"];

        using (var context = new StockAnalysisContext())
        {
            context.CombinedCryptoData.AddRange(arr.Select(jtoken => new CryptoData()
            {
                BaseCurrency = jtoken["d"][0].ToString(),
                BaseCurrencyDesc = jtoken["d"][1].ToString(),
                CryptoTotalRank = (int?)jtoken["d"][2] ?? int.MaxValue,
                Vol24hCmc = (double?)jtoken["d"][3] ?? default,
                MarketCapCalc = (double?)jtoken["d"][4] ?? default,
                CloseChange24h = (double?)jtoken["d"][5] ?? default,
                PerfW = (double?)jtoken["d"][6] ?? default,
                Perf1M = (double?)jtoken["d"][7] ?? default,
                Perf3M = (double?)jtoken["d"][8] ?? default,
                Perf6M = (double?)jtoken["d"][9] ?? default,
                PerfYTD = (double?)jtoken["d"][10] ?? default,
                PerfY = (double?)jtoken["d"][11] ?? default,
                Perf5Y = (double?)jtoken["d"][12] ?? default,
                PerfAll = (double?)jtoken["d"][13] ?? default,
                VolatilityD = (double?)jtoken["d"][14] ?? default,
                Date = DateTime.Now.Date
            }).DistinctBy(d => d.BaseCurrency));

            await context.SaveChangesAsync();
        }
    }

    public class CryptoData
    {
        public string BaseCurrency { get; set; }
        public string BaseCurrencyDesc { get; set; }
        public int CryptoTotalRank { get; set; }
        public double Vol24hCmc { get; set; }
        public double MarketCapCalc { get; set; }
        public double CloseChange24h { get; set; }
        public double PerfW { get; set; }
        public double Perf1M { get; set; }
        public double Perf3M { get; set; }
        public double Perf6M { get; set; }
        public double PerfYTD { get; set; }
        public double PerfY { get; set; }
        public double Perf5Y { get; set; }
        public double PerfAll { get; set; }
        public double VolatilityD { get; set; }
        public DateTime Date { get; set; }
    }
}
