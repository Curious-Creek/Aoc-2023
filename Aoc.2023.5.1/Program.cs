const string InputPath = "./dayfive_input.txt";
var input = File.ReadAllLines(InputPath);

long[] seeds = input[0]
    .Substring(input[0].IndexOf(":") + 1)
    .Split(" ")
    .Where(x => x != "")
    .Select(long.Parse)
    .ToArray();

Console.Write($"Seeds: ");
foreach (var seed in seeds)
{
    Console.Write(seed + " ");
}
Console.WriteLine();

long[] values = seeds;
var ranges =  new List<Tuple<Range, Range>>();

for (int i = 1; i < input.Length; i++)
{
    if (input[i].Contains(":"))
    {
        continue;
    }
    
    if (string.IsNullOrWhiteSpace(input[i]) || i == input.Length - 1)
    {
        if (ranges.Count == 0)
        {
            continue;
        }
        
        for (int j = 0; j < values.Length; j++)
        {
            foreach (var (srcRange, dstRange) in ranges)
            {
                if (values[j] >= srcRange.Start && values[j] <= srcRange.End)
                {
                    long placesFromStart = values[j] - srcRange.Start;
                    long mapValue = dstRange.Start + placesFromStart;
                    values[j] = mapValue;
                    break;
                }
            }
        }

        Console.Write($"Digits after calculation: ");
        foreach (var value in values)
        {
            Console.Write(value + " ");
        }
        Console.WriteLine();
        ranges.Clear();
        continue;
    }
    
    var rangeNumbers = input[i].Split(" ").Select(long.Parse).ToArray();
    Range sourceRange = new Range(rangeNumbers[1], rangeNumbers[1] + rangeNumbers[2] - 1);
    Range destRange = new Range(rangeNumbers[0], rangeNumbers[0] + rangeNumbers[2] - 1);
    ranges.Add(new Tuple<Range, Range>(sourceRange, destRange));
}

Console.WriteLine("Result: " + values.Min());

return 1;

record Range(long Start, long End);