namespace advent_2023;

public class Day5 : Day
{
    public Day5(string filename) : base(filename)
    {
    }

    private class Record
    {
        public long Dest { get; set; }
        public long Source { get; set; }
        public long Range { get; set; }
    }

    private class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        // Console.WriteLine(Part2());
    }

    private (int, int) Combined()
    {
        return (-1, -1);
    }  

    private long Part1()
    {
        var seeds = Lines.First().Split(" ").Select(long.Parse);

        var min = long.MaxValue;
        foreach (var s in seeds)
        {
            var seed = s;
            seed = ParseRecords("seeds-to-soil.txt", seed);
            seed = ParseRecords("soil-to-fert.txt", seed);
            seed = ParseRecords("fert-to-water.txt", seed);
            seed = ParseRecords("water-to-light.txt", seed);
            seed = ParseRecords("light-to-temp.txt", seed);
            seed = ParseRecords("temp-to-hum.txt", seed);
            seed = ParseRecords("hum-to-location.txt", seed);
            min = Math.Min(min, seed);
        }

        return min;
    }
    
    // Part 2 does not work! Too slow to do it the brute force way
    private long Part2()
    {
        // This uses a modified version of the seeds list where each pair is on it's own line
        var ranges = Lines.Select(line =>
        {
            var split = line.Split(" ");
            return new Range
            {
                Start = long.Parse(split[0]),
                End = long.Parse(split[1])
            };
        });
        
        var min = long.MaxValue;
        
        foreach (var range in ranges)
        {
            for (var i = 0; i < range.End; i++)
            {
                var seed = range.Start + i;
                seed = ParseRecords("seeds-to-soil.txt", seed);
                seed = ParseRecords("soil-to-fert.txt", seed);
                seed = ParseRecords("fert-to-water.txt", seed);
                seed = ParseRecords("water-to-light.txt", seed);
                seed = ParseRecords("light-to-temp.txt", seed);
                seed = ParseRecords("temp-to-hum.txt", seed);
                seed = ParseRecords("hum-to-location.txt", seed);
                min = Math.Min(min, seed);
            }
        }

        return min;
    }

    private long ParseRecords(string filename, long seed)
    {
        foreach (var line in File.ReadAllLines($"{Base}/{filename}")) 
        {
            var split = line.Split(" ");
            var record = new Record
            {
                Dest = long.Parse(split[0]),
                Source = long.Parse(split[1]),
                Range = long.Parse(split[2])
            };
            
            if (seed >= record.Source && seed < record.Source + record.Range)
            {
                return record.Dest + seed - record.Source;
            }
        }
        return seed;
    }
}