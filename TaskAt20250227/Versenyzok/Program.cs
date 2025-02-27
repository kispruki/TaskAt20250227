List<Driver> drivers = new List<Driver>();
StreamReader f = File.OpenText("pilotak.csv");
string header = f.ReadLine();
while (!f.EndOfStream)
{
    string line = f.ReadLine();
    drivers.Add(new Driver(line.Split(";")));
    // Console.WriteLine(string.Join('#', line.Split(';')));
}

Console.WriteLine($"3. feladat: {drivers.Count}");
Console.WriteLine($"4. feladat: {drivers[drivers.Count - 1].Name}");
Console.WriteLine("5. feladat:");
foreach (var driver in drivers.Where(driver => driver.BirthDate.Year < 1901))
{
    Console.WriteLine($"\t{driver.Name} ({driver.BirthDate.ToString("yyyy. MM. dd")})");
}

var min = drivers.Where(x=>x.Id!=null).Min(d =>  d.Id);
var minNumberedDriver = drivers.Find(x=>x.Id==min);
Console.WriteLine($"6. feladat: {minNumberedDriver.Nationality}");

Dictionary<int, int> map = new Dictionary<int, int>();
foreach (var driver in drivers.Where(x=>x.Id!=null))
{
    if (map.ContainsKey((int)driver.Id))
    {
        map[(int)driver.Id]++;
    }
    else
    {
        map.Add((int)driver.Id, 1);
    }
}

var results = new List<int>();
foreach (var key in map.Keys)
{
    if (map[key] > 1)
    {
        results.Add(key);
    }
}

Console.WriteLine($"7. feladat: {string.Join(", ", results)}");

class Driver
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }
    public int? Id { get; set; }

    public Driver(string[] line)
    {
        this.Name = line[0];
        var datesplit = line[1].Split('.');
        this.BirthDate = new DateTime(int.Parse(datesplit[0]), int.Parse(datesplit[1]),int.Parse(datesplit[2]));
        this.Nationality = line[2];
        if (line[3]!= "")
        {
            this.Id = int.Parse(line[3]);
        }
        else
        {
            this.Id = null;
        }
    }
}
