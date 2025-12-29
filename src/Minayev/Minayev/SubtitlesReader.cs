using System.Globalization;
using System.Text.RegularExpressions;

namespace Minayev
{
    public class SubtitlesReader
    {
        public static HttpClient httpClient = new HttpClient();

        public SubtitleSegment[] ParseSubtitles(string url)
        {
            var input = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

            var segments = new List<SubtitleSegment>();
            string pattern = @"(\d{2}:\d{2}:\d{2}\.\d{3}) --> (\d{2}:\d{2}:\d{2}\.\d{3})\n(.*?)\n";

            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.Singleline))
            {
                var segment = new SubtitleSegment
                {
                    Start = TimeSpan.Parse(match.Groups[1].Value),
                    End = TimeSpan.Parse(match.Groups[2].Value),
                    Text = match.Groups[3].Value
                };
                segments.Add(segment);
            }

            return segments.ToArray();
        }
    }

    public class SubtitleSegment
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"Start: {Start}, End: {End}, Text: '{Text}'";
        }
    }
}
