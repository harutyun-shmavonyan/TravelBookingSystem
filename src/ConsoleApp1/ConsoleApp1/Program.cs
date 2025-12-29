using System.Linq;

var skills = File.ReadAllLines(@"C:\Users\Harutyun\Desktop\New folder (4)\Angular.txt");

var data = skills.Where(s => s != "").GroupBy(x => x).Select(x => (skill: x.Key, count: x.Count())).OrderByDescending(x => x.count);

var csv = string.Join(Environment.NewLine, data.Select(d => $"{d.skill},{d.count}"));

File.WriteAllText(@"C:\Users\Harutyun\Desktop\New folder (4)\data.csv", csv);