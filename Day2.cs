namespace advent_2023;

public class Day2 : Day
{
    private const string Red = "red";
    private const string Green = "green";
    private const string Blue = "blue";
    
    public Day2(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private int Part1()
    {
        const int maxRed = 12;
        const int maxGreen = 13;
        const int maxBlue = 14;

        var sum = 0;
        foreach (var line in Lines)
        {
            var impossible = false;
            var index = int.Parse(line.Split(": ")[0]);

            foreach (var pull in GetPulls(line))
            {
                foreach (var block in GetBlocks(pull))
                {
                    var (num, color) = GetNumAndColor(block);
                    impossible = color switch
                    {
                        Red => num > maxRed,
                        Green => num > maxGreen,
                        Blue => num > maxBlue,
                        _ => throw new Exception($"Unknown color {color}")
                    };
                }
            }

            if (!impossible)
            {
                sum += index;
            }
        }

        return sum;
    }
    
    private int Part2()
    {
        var sum = 0;
        foreach (var line in Lines)
        {
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;
            
            foreach (var pull in GetPulls(line))
            {
                foreach (var block in GetBlocks(pull))
                {
                    var (num, color) = GetNumAndColor(block);
                    switch (color)
                    {
                        case Red:
                            if (num > maxRed) maxRed = num;
                            break;
                        case Green:
                            if (num > maxGreen) maxGreen = num;
                            break;
                        case Blue:
                            if (num > maxBlue) maxBlue = num;
                            break;
                        default:
                            throw new Exception($"Unknown color {color}");
                    }
                }
            }

            sum += maxRed * maxGreen * maxBlue;
        }

        return sum;
    }

    private static IEnumerable<string> GetPulls(string line)
    {
        return line.Split(": ")[1].Split("; ");
    }

    private static IEnumerable<string> GetBlocks(string pull)
    {
        return pull.Split(", ");
    }

    private static (int, string) GetNumAndColor(string block)
    {
        return (int.Parse(block.Split(" ")[0]), block.Split(" ")[1]);
    }
}