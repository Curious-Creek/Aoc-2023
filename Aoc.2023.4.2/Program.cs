const string InputPath = "./dayfour_input.txt";
var input = File.ReadAllLines(InputPath);

int[] numberOfCards = new int[input.Length];

for (int i = 0; i < input.Length; i++)
{
    var inputNumbers = input[i].Substring(input[i].IndexOf(":") + 2);
    var split = inputNumbers.Split(" | ");
    var cardNumbers = split[0].Split(" ").Where(x => x != "").Select(int.Parse).ToArray();
    var winningNumbers = split[1].Split(" ").Where(x => x != "").Select(int.Parse);
    numberOfCards[i] = 1;
    
    for (int j = 0; j < cardNumbers.Length; j++)
    {
        if (winningNumbers.Contains(cardNumbers[i]))
        {
            numberOfCards[i + 1]++;
        }
    }
}