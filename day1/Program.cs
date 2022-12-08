var lines = File.ReadLinesAsync("input.txt");

var max = 0;
var maxElfIndex = 0;
var currentElfIndex = 0;
var currentElfTotal = 0;

await foreach (var line in lines) {
    if (string.IsNullOrWhiteSpace(line)) {
        if (currentElfTotal > max) {
            max = currentElfTotal;
            maxElfIndex = currentElfIndex;
        }
        
        currentElfTotal = 0;
        currentElfIndex++;
        continue;
    }
    currentElfTotal += int.Parse(line);
}

Console.WriteLine($"Max input: {max} at Elf Index: {maxElfIndex}");