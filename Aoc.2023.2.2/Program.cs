const string InputPath = "./daytwo_input.txt";
var input = File.ReadAllLines(InputPath);

var games = new List<Game>();

foreach (var line in input)
{
    var split = line.Split(':');
    var id = split[0].Split(' ')[1];
    var turnStrings = split[1].Split(';');
    var turns = new List<Turn>();

    foreach (var turnStr in turnStrings)
    {
        var cubeStrings = turnStr.Split(',');
        List<Cube> cubes = new List<Cube>();
        
        foreach (var cubeStr in cubeStrings)
        {
            var cubeTrimmed = cubeStr.TrimStart();
            var cubeSplit = cubeTrimmed.Split(' ');
            var number = cubeSplit[0];
            var color = cubeSplit[1];
            cubes.Add(new Cube(int.Parse(number), color));
        }
        
        turns.Add(CreateTurn(cubes));
    }
    
    games.Add(new Game(int.Parse(id), turns));
}

int result = games.Sum(game => game.Power());

Console.WriteLine(result);
return;

Turn CreateTurn(List<Cube> cubes)
{
    var green = cubes.FirstOrDefault(x => x.Color == "green");
    var blue = cubes.FirstOrDefault(x => x.Color == "blue");
    var red = cubes.FirstOrDefault(x => x.Color == "red");
    return new Turn(green?.Count ?? 0, blue?.Count ?? 0, red?.Count ?? 0);
}

internal sealed class Game(int id, IEnumerable<Turn> turns)
{
    public int Id { get; } = id;
    private IEnumerable<Turn> Turns { get; } = turns;

    public int Power()
    {
        var maxRed = Turns.Max(x => x.Red);
        var maxGreen = Turns.Max(x => x.Green);
        var maxBlue = Turns.Max(x => x.Blue);
        return maxRed * maxGreen * maxBlue;
    }
}

internal record Turn(int Green, int Blue, int Red);

internal record Cube(int Count, string Color);