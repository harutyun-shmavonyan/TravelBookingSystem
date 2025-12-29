// See https://aka.ms/new-console-template for more information
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;

public class Program
{
    public static async Task Main()
    {
        //await new CryptoAnalysis().ExecuteAsync();
        await new StockAnalysis().ExecuteAsync();
    }
}
