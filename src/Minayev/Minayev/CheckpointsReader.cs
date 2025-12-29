using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Minayev
{
    public class CheckpointsReader
    {
        public static HttpClient httpClient = new HttpClient();

        public Checkpoint[] ReadCheckpoints(string checkpointsUrl)
        {
            var json = httpClient.GetAsync(checkpointsUrl).Result.Content.ReadAsStringAsync().Result;

            var checkpointsArray = (JArray)JObject.Parse(json)["video_popup_data"]["episodes"];

            return checkpointsArray
                .Select(t => new Checkpoint(
                    TimeSpan.ParseExact(t["timeText"].ToString(), new[] { "mm\\:ss", "hh\\:mm\\:ss" }, CultureInfo.InvariantCulture),
                    t["name"].ToString()))
                .ToArray();
        }

        public record Checkpoint(TimeSpan startTime, string name);
    }
}
