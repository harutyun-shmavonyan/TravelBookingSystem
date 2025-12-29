// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

public class AnalystInfo
{
    [JsonProperty("analystRatings")]
    public string? AnalystRatings { get; set; }

    [JsonProperty("analystCount")]
    public int AnalystCount { get; set; }

    [JsonProperty("priceTarget")]
    public double? PriceTarget { get; set; }

    [JsonProperty("priceTargetChange")]
    public double? PriceTargetChange { get; set; }
}
