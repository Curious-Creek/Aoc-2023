const string InputPath = "./dayone_input.txt";
var input = File.ReadAllLines(InputPath);

int sum = 0;
List<SpelledOutDigit> spelledOutDigits = new List<SpelledOutDigit>
{
    new("zero", 0),
    new("one", 1),
    new("two", 2),
    new("three", 3),
    new("four", 4),
    new("five", 5),
    new("six", 6),
    new("seven", 7),
    new("eight", 8),
    new("nine", 9)
};

foreach (var line in input)
{
    var firstDigit = SearchFromStart(line);
    var lastDigit = SearchFromEnd(line);
    
    if (firstDigit == null || lastDigit == null)
    {
        throw new Exception("No number found in string");
    }

    var resultString = $"{firstDigit}{lastDigit}";
    sum += int.Parse(resultString);
}

Console.WriteLine(sum);

int? SearchFromStart(string line)
{
    var previousChars = string.Empty;
    
    for (int i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            return int.Parse(line[i].ToString());
        }
        
        previousChars += line[i];

        var spelledOutDigit = spelledOutDigits.FirstOrDefault(x => previousChars.Contains(x.spelled));

        if (spelledOutDigit != null)
        {
            return spelledOutDigit.digit;
        }
    }

    return null;
}

int? SearchFromEnd(string line)
{
    var previousChars = string.Empty;
    
    for (int i = line.Length - 1; i >= 0; i--)
    {
        if (char.IsDigit(line[i]))
        {
            return int.Parse(line[i].ToString());
        }

        previousChars = line[i] + previousChars; 

        var spelledOutDigit = spelledOutDigits.FirstOrDefault(x => previousChars.Contains(x.spelled));

        if (spelledOutDigit != null)
        {
            return spelledOutDigit.digit;
        }
    }

    return null;
}

internal record SpelledOutDigit(string spelled, int digit);