// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

public class PerformanceMetrics
{
    [JsonProperty("ch1w")]
    public double? Change1Week { get; set; }

    [JsonProperty("ch1m")]
    public double? Change1Month { get; set; }

    [JsonProperty("ch6m")]
    public double? Change6Months { get; set; }

    [JsonProperty("chYTD")]
    public double? ChangeYearToDate { get; set; }

    [JsonProperty("ch1y")]
    public double? Change1Year { get; set; }

    [JsonProperty("ch3y")]
    public double? Change3Years { get; set; }

    [JsonProperty("ch5y")]
    public double? Change5Years { get; set; }
}
