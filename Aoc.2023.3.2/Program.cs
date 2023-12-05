const string InputPath = "./daythree_input.txt";
var input = File.ReadAllLines(InputPath);

List<int> gearRatios = new();

for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        if (input[i][j] == '*')
        {
            var adjacentNumbers = AdjacentNumbers(j - 1, j + 1, i);

            if (adjacentNumbers.Length > 1)
            {
                gearRatios.Add(adjacentNumbers[0] * adjacentNumbers[1]);
            }
        }
    }
}

var result = gearRatios.Sum();
Console.WriteLine(result);

return 1;

int[] AdjacentNumbers(int left, int right, int y)
{
    List<int> numbers = new();
    var (leftBorder, rightBorder) = CheckXBorder(left, right, y);
    
    // Check left
    if (char.IsDigit(input[y][leftBorder]))
    {
        numbers.Add(AssemblePartNumber(leftBorder, y));
    }
    
    // Check right
    if (char.IsDigit(input[y][rightBorder]))
    {
        numbers.Add(AssemblePartNumber(rightBorder, y));
    }

    numbers.AddRange(GetNumbersFromOtherY(y - 1, leftBorder, rightBorder));
    numbers.AddRange(GetNumbersFromOtherY(y + 1, leftBorder, rightBorder));
    
    return numbers.ToArray();
}

int[] GetNumbersFromOtherY(int y, int leftBorder, int rightBorder)
{
    if (y >= input.Length || y < 0)
    {
        return Array.Empty<int>();
    }

    var hasDigitLeftCorner = char.IsDigit(input[y][leftBorder]);
    var hasDigitRightCorner = char.IsDigit(input[y][rightBorder]);
    
    // Check if both corners have a digit with a . in between
    // This is the case where there are multiple numbers in the same row
    if (hasDigitLeftCorner && hasDigitRightCorner)
    {
        if (!char.IsDigit(input[y][leftBorder + 1]))
        {
            int leftNumber = AssemblePartNumber(leftBorder, y);
            int rightNumber = AssemblePartNumber(rightBorder, y);
            return new[] { leftNumber, rightNumber };
        }

        return new[] { AssemblePartNumber(leftBorder, y) };
    }

    // If only left corner has a digit, return that number
    if (hasDigitLeftCorner)
    {
        return new[] { AssemblePartNumber(leftBorder, y) };
    }

    // If only right corner has a digit, return that number
    if (hasDigitRightCorner)
    {
        return new[] { AssemblePartNumber(rightBorder, y) };
    }

    return Array.Empty<int>();
}

Tuple<int, int> CheckXBorder(int left, int right, int y)
{
    if (left < 0)
    {
        left = 0;
    }
    
    if (right > input[y].Length - 1)
    {
        right = input[y].Length - 1;
    }

    return new Tuple<int, int>(left, right);
}

int AssemblePartNumber(int x, int y)
{
    string partNumber = input[y][x].ToString();
    
    //Check to left of coords
    for (int i = x - 1; i >= 0; i--)
    {
        if (char.IsDigit(input[y][i]))
        {
            partNumber = input[y][i] + partNumber;
        }
        else
        {
            break;
        }
    }

    //Check to right of coords
    for (int i = x + 1; i < input[y].Length; i++)
    {
        if (char.IsDigit(input[y][i]))
        {
            partNumber += input[y][i];
        }
        else
        {
            break;
        }
    }

    return int.Parse(partNumber);
}