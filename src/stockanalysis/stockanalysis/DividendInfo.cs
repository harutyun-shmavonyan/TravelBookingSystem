// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

public class DividendInfo
{
    [JsonProperty("dps")]
    public double? DividendPerShare { get; set; }

    [JsonProperty("dividendYield")]
    public double? DividendYield { get; set; }

    [JsonProperty("payoutRatio")]
    public double? PayoutRatio { get; set; }

    [JsonProperty("dividendGrowth")]
    public double? DividendGrowth { get; set; }

    [JsonProperty("payoutFrequency")]
    public string? PayoutFrequency { get; set; }
}
