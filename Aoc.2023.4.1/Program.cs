const string InputPath = "./dayfour_input.txt";
var input = File.ReadAllLines(InputPath);

var pointsSum = 0;

foreach (var line in input)
{
    var inputNumbers = line.Substring(line.IndexOf(":") + 2);
    var split = inputNumbers.Split(" | ");
    var cardNumbers = split[0].Split(" ").Where(x => x != "").Select(int.Parse);
    var winningNumbers = split[1].Split(" ").Where(x => x != "").Select(int.Parse);

    var points = 0;

    foreach (var cardNumber in cardNumbers)
    {
        if (winningNumbers.Contains(cardNumber))
        {
            if (points == 0)
            {
                points = 1;
            }
            else
            {
                points *= 2;
            }
        }
    }

    pointsSum += points;
}

Console.WriteLine(pointsSum);
return 1;



