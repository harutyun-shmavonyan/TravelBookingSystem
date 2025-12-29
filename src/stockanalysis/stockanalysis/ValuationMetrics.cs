// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

public class ValuationMetrics
{
    [JsonProperty("enterpriseValue")]
    public long? EnterpriseValue { get; set; }

    [JsonProperty("peForward")]
    public double? PeForward { get; set; }

    [JsonProperty("psRatio")]
    public double? PsRatio { get; set; }

    [JsonProperty("pbRatio")]
    public double? PbRatio { get; set; }

    [JsonProperty("pFcfRatio")]
    public double? PFcfRatio { get; set; }
}
