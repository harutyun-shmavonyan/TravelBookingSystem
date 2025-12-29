// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

public class StockInfo
{
    [JsonProperty("s")]
    public string? Symbol { get; set; }

    [JsonProperty("n")]
    public string? Name { get; set; }

    [JsonProperty("marketCap")]
    public long? MarketCapitalization { get; set; }

    [JsonProperty("price")]
    public double? StockPrice { get; set; }

    [JsonProperty("change")]
    public double? PriceChange { get; set; }

    [JsonProperty("industry")]
    public string? IndustrySector { get; set; }

    [JsonProperty("volume")]
    public long? TradingVolume { get; set; }

    [JsonProperty("peRatio")]
    public double? PriceEarningsRatio { get; set; }
}
