// See https://aka.ms/new-console-template for more information

using Minayev;
using System.Collections;
using System.Text;
using static Minayev.CheckpointsReader;

Console.WriteLine("Hello, World!");

var metadataReader = new MetadataReader();
var checkpointsReader = new CheckpointsReader();
var subtitlesReader = new SubtitlesReader();

var metadata = metadataReader.Read("C:\\Users\\Harutyun\\Desktop\\Videos.csv");

foreach(var meta in metadata)
{
    var subtitles = subtitlesReader.ParseSubtitles(meta.Subtitles);
    var checkpoints = checkpointsReader.ReadCheckpoints(meta.Checkpoints);

    var textCreator = new TextCreator();
    var text = textCreator.GenerateText(meta.Name, subtitles, checkpoints);

    File.WriteAllText($"C:\\Users\\Harutyun\\Desktop\\{meta.Name}.txt", text, Encoding.UTF8);
}

public class TextCreator()
{
    public string GenerateText(string header, SubtitleSegment[] subtitleSegments, Checkpoint[] checkpoints)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(header);
        stringBuilder.AppendLine();

        var checkpointsQueue = new Stack<Checkpoint>(checkpoints.Reverse());
        var subtitlesQueue = new Stack<SubtitleSegment>(subtitleSegments.Reverse());

        var checkpoint = checkpointsQueue.Pop();
        stringBuilder.AppendLine();
        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"•{checkpoint.name}");

        while (checkpointsQueue.Any())
        {
            checkpoint = checkpointsQueue.Pop();

            while(subtitlesQueue.Any() && subtitlesQueue.Peek().Start <= checkpoint.startTime)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(subtitlesQueue.Pop().Text);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"•{checkpoint.name}");
        }

        foreach(var remainginSubtitleSegment in subtitlesQueue)
        {
                            stringBuilder.Append(" ");
            stringBuilder.AppendLine(remainginSubtitleSegment.Text);
        }

        return stringBuilder.ToString();
    }
}