using System.Security.Cryptography.X509Certificates;

List<Rent> rents = new List<Rent>();

StreamReader f = File.OpenText("kolcsonzesek.txt");
string header = f.ReadLine();
while (!f.EndOfStream)
{
    string line = f.ReadLine();
    string[] parts = line.Split(';');

    rents.Add(new Rent(parts[0], Convert.ToChar(parts[1]),
        int.Parse(parts[2]), int.Parse(parts[3]),
        int.Parse(parts[4]), int.Parse(parts[5])));
}
f.Close();
Console.WriteLine($"5. feladat: Napi kölcsönzések száma: {rents.Count}");
Console.Write($"6. feladat: Kérek egy nevet: ");
string uiName = Console.ReadLine();

List<Rent> results = rents.FindAll(x => x.Name == uiName);
Console.WriteLine($"\t{uiName} kölcsönzései");
if (results.Count > 0)
{
    foreach (Rent rent in results)
    {
        Console.WriteLine($"\t{rent.StartTime.ToString()} - {rent.EndTime.ToString()}");
    }
}
else
{
    Console.WriteLine("\tNem volt ilyen nevü kölcsönző!");
}

Console.Write($"7.feladat: Adjon meg egy időpontot óra:perc alakban: ");
string uiTimeStr = Console.ReadLine();
Time uiTime = new Time(int.Parse(uiTimeStr.Split(":")[0]),int.Parse(uiTimeStr.Split(":")[1]));

results = rents.FindAll(x=> new TimeSpan(x.StartTime, x.EndTime).isInside(uiTime));
Console.WriteLine("\tVizen lévő járművek: ");
foreach (var rent in results)   
{
    Console.WriteLine($"\t{rent.StartTime.ToString()} - {rent.EndTime.ToString()} : {rent.Name}");
}

Console.Write("8.Feladat: ");
double result = 0;
foreach (var rent in rents) 
{
    var ts = new TimeSpan(rent.StartTime, rent.EndTime);
    result += Math.Ceiling((double)ts.DurationMinutes / 30.0) * rent.Price;
}

Console.WriteLine($"A napi bevétel: {result} Ft");

StreamWriter f1 = File.CreateText("F.txt");
results = rents.FindAll(x=> x.Id == 'F');
results.ForEach(x=> f1.WriteLine($"{x.StartTime} - {x.EndTime}: {x.Name}"));
f1.Close();

Dictionary<char, int> map = new Dictionary<char, int>();
map.Add('A', 0);
map.Add('B', 0);
map.Add('C', 0);
map.Add('D', 0);
map.Add('E', 0);
map.Add('F', 0);
map.Add('G', 0);
foreach (var rent in rents)
{
    map[rent.Id]++;
}

foreach (var key in map.Keys)
{
    Console.WriteLine($"\t{key} - {map[key]}");
}

class Rent
{
    public string Name { get; set; }
    public char Id { get; set; }
    public Time StartTime { get; set; }
    public Time EndTime { get; set; }
    public int Price { get; set; }

    public Rent(string name, char id, int startHour, int startMinute, int endHour, int endMinute)
    {
        this.Name = name;
        this.Id = id;
        this.StartTime = new Time(startHour, startMinute);
        this.EndTime = new Time(endHour, endMinute);
        this.Price = 2400;
    }
}

class Time
{
    public int Hour { get; set; }
    public int Minute { get; set; }

    public Time(int hour, int minute)
    {
        this.Hour = hour;
        this.Minute = minute;
    }

    public override string ToString()
    {
        return this.Minute > 9 ? $"{this.Hour}:{this.Minute}" : $"{this.Hour}:0{this.Minute}";
    }
}

class TimeSpan
{
    public int StartMinutes { get; set; }
    public int EndMinutes { get; set; }
    public int DurationMinutes { get; set; }
    public Time DurationTime { get; set; }

    public TimeSpan(Time start, Time end)
    {
        this.StartMinutes = start.Hour * 60 + start.Minute;
        this.EndMinutes = end.Hour * 60 + end.Minute;
        this.DurationMinutes = this.EndMinutes - this.StartMinutes;
        this.DurationTime = new Time(DurationMinutes / 60, DurationMinutes % 60);
    }

    public bool isInside(Time moment)
    {
        int momentMinutes = moment.Hour * 60 + moment.Minute;
        return this.StartMinutes <= momentMinutes && momentMinutes <= this.EndMinutes;
    }
}