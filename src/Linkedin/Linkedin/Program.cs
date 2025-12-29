using HtmlAgilityPack;
using System.Text.RegularExpressions;

var htmlDocument = new HtmlDocument();
htmlDocument.Load(@"C:\Users\Harutyun\Desktop\ul.txt");

static int RetrieveNumbers(string input)
{
    MatchCollection matches = Regex.Matches(input, @"(\d{1,3}(?:,\d{3})*)");

    string extractedNumbers = "";

    foreach (Match match in matches)
    {
        extractedNumbers += match.Value.Replace(",", "") + " ";
    }

    return int.Parse(extractedNumbers.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last().Trim());
}

var records = htmlDocument.DocumentNode.FirstChild.ChildNodes.Where(n => n.NodeType == HtmlNodeType.Element).Select((postNode, index) =>
{
    var a = postNode.DescendantNodes().Where(n => n.InnerText.Contains("and") && n.InnerText.Contains("others")).ToArray();
    var reactionsText = postNode.DescendantNodes().Where(n => n.GetClasses().Contains("social-details-social-counts__social-proof-text")).FirstOrDefault()?.InnerText;

    if(reactionsText == null)
        return null;

    var reactionsCount = RetrieveNumbers(reactionsText);

    return new Post(reactionsCount, postNode.InnerHtml);
}).Where(r => r != null).ToArray();

var a = records;

public record Post(int Reactions, string Html);
