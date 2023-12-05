using System.Text.RegularExpressions;

namespace advent_2023;

// Yeah, I decided to not do part 2, oh well
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
        return -1;
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

    private IEnumerable<int> IndicesOf(string line)
    {
        return line.Select((v, i) => new { v, i })
            .Where(x => x.v == '*')
            .Select(x => x.i);
    }

    [GeneratedRegex("\\d+")]
    private static partial Regex Int();
}