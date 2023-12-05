const string InputPath = "./daythree_input.txt";
var input = File.ReadAllLines(InputPath);

List<int> partNumbers = new();

for (int y = 0; y < input.Length; y++)
{
    string partNumber = string.Empty;
    int firstDigit = -1;
    int lastDigit = -1;
    
    for (int x = 0; x < input[y].ToCharArray().Length; x++)
    {
        if (char.IsDigit(input[y][x]))
        {
            if (firstDigit == -1)
                firstDigit = x;
            
            partNumber += input[y][x];
            
            if (x == input[y].ToCharArray().Length - 1)
            {
                lastDigit = x;
            }
        } else if (partNumber != string.Empty)
        {
            lastDigit = x - 1;
        }
        
        if (firstDigit != -1 && lastDigit != -1)
        {
            int leftBorder = firstDigit - 1;
            int rightBorder = lastDigit + 1;

            if (leftBorder < 0)
            {
                leftBorder = 0;
            } else
            {
                // Check left
                if (IsNotDigitOrDot(input[y][leftBorder]))
                {
                    partNumbers.Add(int.Parse(partNumber));
                    partNumber = string.Empty;
                    firstDigit = -1;
                    lastDigit = -1;
                    continue;
                }
            }

            if (rightBorder > input[y].Length - 1)
            {
                rightBorder = input[y].Length - 1;
            } else
            {
                // Check right
                if (IsNotDigitOrDot(input[y][rightBorder]))
                {
                    partNumbers.Add(int.Parse(partNumber));
                    partNumber = string.Empty;
                    firstDigit = -1;
                    lastDigit = -1;
                    continue;
                }
            }

            for (int i = leftBorder; i <= rightBorder; i++)
            {
                // Check upper row
                if (y != 0)
                {
                    if (IsNotDigitOrDot(input[y - 1][i]))
                    {
                        partNumbers.Add(int.Parse(partNumber));
                        break;
                    }
                }

                // Check lower row
                if (y != input.Length - 1)
                {
                    if (IsNotDigitOrDot(input[y + 1][i]))
                    {
                        partNumbers.Add(int.Parse(partNumber));
                        break;
                    }
                }
            }

            partNumber = string.Empty;
            firstDigit = -1;
            lastDigit = -1;
        }
    }
}

var result = partNumbers.Sum();
Console.WriteLine(result);

return 1;

bool IsNotDigitOrDot(char c)
{
    return !char.IsDigit(c) && c != '.';
}