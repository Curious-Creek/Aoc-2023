const string InputPath = "./dayone_input.txt";
var input = File.ReadAllLines(InputPath);

int sum = 0;

foreach (var line in input)
{
    var indexOfFirstDigit = SearchFromStart(line);
    var indexOfLastDigit = SearchFromEnd(line);
    
    if (indexOfFirstDigit == null || indexOfLastDigit == null)
    {
        throw new Exception("No number found in string");
    }
    
    var resultString = line[indexOfFirstDigit.Value].ToString() + line[indexOfLastDigit.Value];
    sum += int.Parse(resultString);
}

Console.WriteLine(sum);

int? SearchFromStart(string line)
{
    for (int i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            return i;
        }
    }

    return null;
}

int? SearchFromEnd(string line)
{
    for (int i = line.Length - 1; i >= 0; i--)
    {
        if (char.IsDigit(line[i]))
        {
            return i;
        }
    }

    return null;
}

