const string InputPath = "./daytwo_input.txt";
var input = File.ReadAllLines(InputPath);

const int MaxRed = 12;
const int MaxGreen = 13;
const int MaxBlue = 14;

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
    
    games.Add(new Game(int.Parse(id), turns, MaxRed, MaxGreen, MaxBlue));
}

int result = games.Where(game => game.IsValidGame()).Sum(game => game.Id);

Console.WriteLine(result);

Turn CreateTurn(List<Cube> cubes)
{
    var green = cubes.FirstOrDefault(x => x.Color == "green");
    var blue = cubes.FirstOrDefault(x => x.Color == "blue");
    var red = cubes.FirstOrDefault(x => x.Color == "red");
    return new Turn(green?.Count ?? 0, blue?.Count ?? 0, red?.Count ?? 0);
}

internal sealed class Game(int id, List<Turn> turns, int maxRed, int maxGreen, int maxBlue)
{
    public int Id { get; } = id;
    private List<Turn> _turns { get; } = turns;
    private int _maxRed { get; } = maxRed;
    private int _maxBlue { get; } = maxBlue;
    private int _maxGreen { get; } = maxGreen;

    public bool IsValidGame()
    {
        return _turns.All(x => x.Red <= _maxRed && x.Green <= _maxGreen && x.Blue <= _maxBlue);
    }
}
record Turn(int Green, int Blue, int Red);
record Cube(int Count, string Color);