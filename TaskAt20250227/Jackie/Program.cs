
List<Season> seasons = new List<Season>();
StreamReader f = File.OpenText("jackie.txt");
string header = f.ReadLine();
while (!f.EndOfStream)
{
    string line = f.ReadLine();
    seasons.Add(new Season(line.Split('\t')));
}

f.Close();

Console.WriteLine($"3. feladat: {seasons.Count}");

var mostRacesInt = seasons.Max(x => x.Races);
var mostRaces = seasons.Find(x => x.Races == mostRacesInt);
Console.WriteLine($"4. feladat: {mostRaces.Year}");

Dictionary<int, int> DecadeToWins = new Dictionary<int, int>();
foreach (var season in seasons)
{
    if (DecadeToWins.ContainsKey(int.Parse(season.Year.ToString()[2].ToString())))
    {
        DecadeToWins[int.Parse(season.Year.ToString()[2].ToString())]+=season.Wins;
    }
    else
    {
        DecadeToWins.Add(int.Parse(season.Year.ToString()[2].ToString()), season.Wins);
    }
}

Console.WriteLine($"5. feladat: ");
foreach (var key in DecadeToWins.Keys)
{
    Console.WriteLine($"\t{key}0-es évek: {DecadeToWins[key]} megnyert verseny! ");
}

class Season
{
    public int Year { get; set; }
    public int Races { get; set; }
    public int Wins { get; set; }
    public int Podiums { get; set; }
    public int Poles { get; set; }
    public int Fastests { get; set; }

    public Season(string[] line)
    {
       this.Year = int.Parse(line[0]); 
       this.Races = int.Parse(line[1]); 
       this.Wins = int.Parse(line[2]); 
       this.Podiums = int.Parse(line[3]); 
       this.Poles = int.Parse(line[4]); 
       this.Fastests = int.Parse(line[5]); 
    }
}
