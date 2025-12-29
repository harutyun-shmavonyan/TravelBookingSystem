using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minayev
{
    public class MetadataReader
    {
        public Metadata[] Read(string fileFullName)
        {
            using var reader = new StreamReader(fileFullName, Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<Metadata>().ToArray();
        }
    }

    public class Metadata
    {
        public string Name { get; set; }
        public string Checkpoints { get; set; }
        public string Subtitles { get; set; }
    }
}
