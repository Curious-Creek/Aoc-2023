const string InputPath = "./dayfive_input.txt";
var input = File.ReadAllLines(InputPath);

long[] seeds = input[0]
    .Substring(input[0].IndexOf(":") + 1)
    .Split(" ")
    .Where(x => x != "")
    .Select(long.Parse)
    .ToArray();

List<List<Tuple<Range, Range>>> allRanges = new List<List<Tuple<Range, Range>>>();
var ranges = new List<Tuple<Range, Range>>();

// Collect ranges
for (int i = 2; i < input.Length; i++)
{
    if (input[i].Contains(":"))
    {
        continue;
    }
    
    if (string.IsNullOrWhiteSpace(input[i]) || i == input.Length - 1)
    {
        var temp = new Tuple<Range, Range>[ranges.Count];
        ranges.CopyTo(temp);
        allRanges.Add(temp.ToList());
        ranges.Clear();
        continue;
    }
    
    var rangeNumbers = input[i].Split(" ").Select(long.Parse).ToArray();
    Range sourceRange = new Range(rangeNumbers[1], rangeNumbers[1] + rangeNumbers[2] - 1);
    Range destRange = new Range(rangeNumbers[0], rangeNumbers[0] + rangeNumbers[2] - 1);
    ranges.Add(new Tuple<Range, Range>(sourceRange, destRange));
}

List<long> locationValues = new List<long>();

// Calculate location values for each seed range
for (int i = 0; i < seeds.Length; i++)
{
    List<long> values = new List<long>();
    Range seedRange = new Range(seeds[i], seeds[i] + seeds[++i]);
    
    for(long j = seedRange.Start; j < seedRange.End; j++)
    {
        values.Add(j);
    }

    foreach (var rangeGroup in allRanges)
    {
        for (int j = 0; j < values.Count; j++)
        {
            foreach (var (srcRange, dstRange) in rangeGroup)
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
    }
    
    locationValues.AddRange(values);
}

Console.WriteLine("Result: " + locationValues.Min());

return 1;

// Answer tried: 63627806 (too low)

record Range(long Start, long End);