using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

VocabularyRecord[] records;

using (var reader = new StreamReader("C:\\Users\\Harutyun\\Desktop\\English.csv", Encoding.UTF8))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    records = csv.GetRecords<VocabularyRecord>().ToArray();

    var todaysDay = records.Max(record => record.DayNumber);
    var todaysRecords = records.Where(record => record.DayNumber == todaysDay);

    var pastRecords = records.Where(record =>
        record.DayNumber == todaysDay - 1 ||
        record.DayNumber == todaysDay - 2 ||
        record.DayNumber == todaysDay - 3 ||
        record.DayNumber == todaysDay - 7 ||
        record.DayNumber == todaysDay - 14 ||
        record.DayNumber == todaysDay - 28 ||
        record.DayNumber == todaysDay - 100 ||
        record.DayNumber == todaysDay - 350 ||
        record.Repeat == true);

    var random = new Random();
    var pastWords = pastRecords
        .GroupBy(record => record.EnglishWord)
        .ToDictionary(group => group.Key, group => group.Where(r => r.Repeat == false).OrderBy(r => r.Repeat ? int.MinValue : random.Next()).Take(2).ToArray());

    var todaysWords = todaysRecords
        .GroupBy(record => record.EnglishWord)
        .ToDictionary(group => group.Key, group => group.OrderBy(r => random.Next()).Take(3).ToArray());

    var allRecords = pastWords.Values.SelectMany(r => r).Take(30).Concat(todaysWords.Values.SelectMany(r => r)).OrderBy(w => random.Next()).ToArray();

    int i = 0;
    foreach (var record in allRecords)
    {
        Console.WriteLine($"Sentence ({++i}/{allRecords.Length}): {record.Translation}");
        Console.ReadKey();
        Console.WriteLine($"Answer: {record.EnglishSentence}");
        Console.WriteLine($"Was the answer correct?");
        if (Console.ReadKey(true).KeyChar != 'y')
        {
            record.Repeat = true;
        }

        Console.WriteLine();
    }
}

using var writer = new StreamWriter("C:\\Users\\Harutyun\\Desktop\\English.csv", false, Encoding.UTF8);
using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
{
    csvWriter.WriteRecords(records);
}

public class VocabularyRecord
{
    public string EnglishWord {get; set;}
    public string EnglishSentence {get; set;}
    public string Translation {get; set;}
    public int DayNumber {get; set;}
    public bool Repeat {get; set;}
}