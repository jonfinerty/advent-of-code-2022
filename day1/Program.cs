var lines = File.ReadLinesAsync("input.txt");


var topThreeTotals = new List<int>(new int[]{0,0,0});
var currentElfTotal = 0;

await foreach (var line in lines) {
    if (string.IsNullOrWhiteSpace(line)) {
        topThreeTotals.Add(currentElfTotal);
        topThreeTotals.Sort();
        topThreeTotals.RemoveAt(0);        
        currentElfTotal = 0;
        continue;
    }
    currentElfTotal += int.Parse(line);
}

Console.WriteLine($"Max input: {topThreeTotals.Last()}");

Console.WriteLine($"Top three values: {topThreeTotals.Sum()}");