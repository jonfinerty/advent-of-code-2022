var lines = File.ReadLines("input.txt");

Console.WriteLine(lines.Sum(line => BagHalfIntersection(line)));
Console.WriteLine(lines.Chunk(3).Sum(groupOfThree => CalculateValue(groupOfThree)));

static int BagHalfIntersection(string line) {
    var halfLength = line.Length / 2;
    var firstHalf = line.Substring(0, halfLength);
    var secondHalf = line.Substring(halfLength, halfLength);

    var intersectChar = firstHalf.Intersect(secondHalf).First();
    return ToSpecialValue(intersectChar);
}

static int CalculateValue(string[] groupOfThree) {
    var intersectChar = groupOfThree[0]
        .Intersect(groupOfThree[1])
        .Intersect(groupOfThree[2])
        .First();

    return ToSpecialValue(intersectChar);
}

static int ToSpecialValue(char c) {
    if (char.IsAsciiLetterLower(c)) {
        return c - 96;
    } else {
        return c - 38;
    }
}

