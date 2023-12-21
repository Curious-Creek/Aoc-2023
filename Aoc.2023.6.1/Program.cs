const string InputPath = "./daysix_input.txt";
var input = File.ReadAllLines(InputPath);

var times = ParseNumbers(input[0].Substring(input[0].IndexOf(":") + 1).TrimStart()).ToList();
var distances = ParseNumbers(input[1].Substring(input[1].IndexOf(":") + 1).TrimStart()).ToList();

int totalPossibleSpeeds = 1;

for (int i = 0; i < times.Count; i++)
{
    int possibleSpeeds = 0;
    
    for (int j = 0; j < times[i]; j++)
    {
        var distance = j * (times[i] - j);

        if (distance > distances[i])
        {
            possibleSpeeds++;
        }
    }

    totalPossibleSpeeds *= possibleSpeeds;
}

Console.WriteLine(totalPossibleSpeeds);

return 1;

IEnumerable<int> ParseNumbers(string line)
{
    string digit = string.Empty;
    List<int> numbers = new List<int>();
    
    for (int i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            digit += line[i];
        } else if (digit.Length > 0)
        {
            numbers.Add(int.Parse(digit));
            digit = string.Empty;
        }
    }

    if (digit.Length > 0)
    {
        numbers.Add(int.Parse(digit));
    }

    return numbers;
}