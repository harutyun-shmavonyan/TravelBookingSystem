// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

public class FinancialMetrics
{
    [JsonProperty("revenue")]
    public long? Revenue { get; set; }

    [JsonProperty("operatingIncome")]
    public long? OperatingIncome { get; set; }

    [JsonProperty("netIncome")]
    public long? NetIncome { get; set; }

    [JsonProperty("fcf")]
    public long? FreeCashFlow { get; set; }

    [JsonProperty("eps")]
    public double? EarningsPerShare { get; set; }
}
