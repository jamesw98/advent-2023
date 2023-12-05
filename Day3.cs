using System.Text.RegularExpressions;

namespace advent_2023;

public partial class Day3 : Day
{
    private static readonly char[] Symbols = { '*', '#', '/', '@', '$', '%', '+', '-', '&', '=' };

    public Day3(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private int Part1()
    {
        var sum = 0;
        Lines.Each((line, lineIndex) => 
        {
            var matches = Int().Matches(line);
            foreach (var match in matches.Select(m => m))
            {
                var startIndex = match.Index;
                var endIndex = startIndex + match.Length - 1;
                
                // check lineIndex - 1
                if (lineIndex != 0)
                {
                    sum += CheckNextOrPrevLine(startIndex, endIndex, Lines.ElementAt(lineIndex - 1), match.Value);
                }
                
                // check current index
                sum += CheckCurrentLine(startIndex, endIndex, line, match.Value);

                // check lineIndex + 1
                if (lineIndex != Lines.Length - 1)
                {
                    sum += CheckNextOrPrevLine(startIndex, endIndex, Lines.ElementAt(lineIndex + 1), match.Value);
                }
            }
        });

        return sum;
    }
    
    private int Part2()
    {
        var sum = 0;
        Lines.Each((line, lineIndex) =>
        {
            foreach (var i in StarInds(line))
            {
                var adjStart = i == 0 ? 0 : i - 1;
                var adjEnd = i == line.Length - 1 ? line.Length - 1 : i + 1;
    
                var allMatches = new List<Match>();
                
                // check prev line
                if (lineIndex != 0)
                {
                    GetMatchesInRange(Lines[lineIndex - 1], adjStart, adjEnd, allMatches);
                }
                
                // check curr line
                GetMatchesInRange(line, adjStart, adjEnd, allMatches);
                
                // check next line
                if (lineIndex != Lines.Length - 1)
                {
                    GetMatchesInRange(Lines[lineIndex + 1], adjStart, adjEnd, allMatches);
                }

                if (allMatches.Count == 2)
                {
                    sum += int.Parse(allMatches.First().Value) * int.Parse(allMatches.Last().Value);
                }
            }
        });
        return sum;
    }

    private static int CheckNextOrPrevLine(int startIndex, int endIndex, string line, string match)
    {
        // ensure we don't go negative
        if (startIndex > 0)
        {
            startIndex -= 1;
        }

        // ensure we don't go over the last index
        if (endIndex > line.Length - 2)
        {
            endIndex = line.Length - 2;
        }

        return line[(startIndex)..(endIndex + 2)].Any(c => Symbols.Contains(c)) 
            ? int.Parse(match) 
            : 0;
    }
    
    private static int CheckCurrentLine(int startIndex, int endIndex, string line, string match)
    {
        // check in directly left and right of the matched number
        // case like ...$123... or ...123$...
        if (startIndex != 0 && Symbols.Contains(line[startIndex - 1]) ||
            endIndex != line.Length - 1 && Symbols.Contains(line[endIndex + 1]))
        {
            return int.Parse(match);
        }    
        
        return 0;
    }

    private static IEnumerable<int> StarInds(string line)
    {
        return line.Select((v, i) => new { v, i })
            .Where(x => x.v == '*')
            .Select(x => x.i);
    }

    private static void GetMatchesInRange(string line, int adjStart, int adjEnd, ICollection<Match> allMatches)
    {
        // This could be one giant Linq statement, but what is generated is gross, even to me, a Linq lover:
        // allMatches.AddRange(from m in Int().Matches(line).Select(x => x) let r = Enumerable.Range(m.Index, m.Length) where r.Contains(adjStart) || r.Contains(adjEnd) select m);
        foreach (var m in Int().Matches(line).Select(x => x))
        {
            var r = Enumerable.Range(m.Index, m.Length);
            if (r.Contains(adjStart) || r.Contains(adjEnd))
            {
                allMatches.Add(m);
            }
        }
    }

    [GeneratedRegex("\\d+")]
    private static partial Regex Int();
}