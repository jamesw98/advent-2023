using System.Text.RegularExpressions;

namespace advent_2023;

public partial class Day1 : Day
{
    public Day1(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private int Part1()
    {
        return Lines
            .Select(l => NonInt().Replace(l, ""))
            .Select(l => int.Parse($"{l.First()}{l.Last()}"))
            .Sum();
    }

    private int Part2()
    {
        var strToInt = new Dictionary<string, string>
        {
            { "one", "o1e" },
            { "two", "t2o" },
            { "three", "t3e" },
            { "four", "f4r" },
            { "five", "f5e" },
            { "six", "s6x" },
            { "seven", "s7n" },
            { "eight", "e8t" },
            { "nine", "n9e" },
        };

        return Lines
            .Select(l => strToInt.Keys.Aggregate(l, (c, k) => c.Replace(k, strToInt[k])))
            .Select(l => NonInt().Replace(l, ""))
            .Select(l => int.Parse($"{l.First()}{l.Last()}"))
            .Sum();
    }

    [GeneratedRegex("[^0-9]")]
    private static partial Regex NonInt();
}