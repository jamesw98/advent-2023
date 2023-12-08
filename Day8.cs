namespace advent_2023;

public class Day8 : Day
{
    public Day8(string filename) : base(filename)
    {
    }

    private class Node
    {
        public required string Left { get; set; }
        public required string Right { get; set; }
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private long Part1()
    {
        var count = 0;
        var (directions, nodesDict, _) = ParseInput();
        
        var currentLocation = "AAA";
        while (currentLocation != "ZZZ")
        {
            currentLocation = GetNewLocation(currentLocation, nodesDict, directions, count++);
        }
        
        return count;
    }

    private long Part2()
    {
        var result = 1L; 
        var (directions, nodesDict, starts) = ParseInput();

        foreach (var start in starts)
        {
            var count = 0;
            
            var currentLocation = start;
            while (currentLocation.Last() != 'Z')
            {
                currentLocation = GetNewLocation(currentLocation, nodesDict, directions, count++);
            }

            result = Lcm(result, count);
        }
        
        return result;
    }

    private static long Lcm(long x, long y)
    {
        var a = Math.Max(x, y);
        var b = Math.Min(x, y);
        for (var i = 1L; i <= b; i++)
        {
            if (a * i % b == 0)
            {
                return a * i;
            }
        }

        return b;
    }

    private (string Directions, Dictionary<string, Node> Nodes, List<string> Starts) ParseInput()
    {
        var nodesDict = new Dictionary<string, Node>();
        var starts = new List<string>();
        
        var directions = Lines.First();
        foreach (var line in Lines[2..])
        {
            var splitEquals = line.Split(" = (");
            var splitComma = splitEquals[1].TrimEnd(')').Split(", ");

            var loc = splitEquals[0]; 
            if (loc.Last() == 'A')
            {
                starts.Add(loc);
            }
            
            nodesDict.Add(splitEquals[0], new Node
            {
                Left = splitComma[0],
                Right = splitComma[1]
            });
        }

        return (directions, nodesDict, starts);
    }

    private static string GetNewLocation(string curr, Dictionary<string, Node> dict, string directions, int count)
    {
        var node = dict[curr];
        return directions[count % directions.Length] == 'R'
            ? node.Right
            : node.Left;
    }
}