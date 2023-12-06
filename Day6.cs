namespace advent_2023;

public class Day6 : Day
{
    // yeah, having a class for this is overkill, but making it helped when understanding the problem
    private class Race
    {
        public long Time { get; set; }
        public long Distance { get; set; }

        public Race(long t, long d)
        {
            Time = t;
            Distance = d;
        }
    }

    // you'll just have to trust that i instantiated these, you're not supposed to include inputs in repos for AoC
    // i just manually created them in the constructor
    private List<Race> _example;
    private List<Race> _data;
    private Race _big;
    
    public Day6(string filename) : base(filename)
    {
        // inputs here
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private long Part1()
    {
        var result = 1L;
        foreach (var race in _data)
        {
            var winners = 0L;
            for (var heldTime = 1; heldTime < race.Time - 1; heldTime++)
            {
                if (heldTime * (race.Time - heldTime) > race.Distance)
                {
                    winners++;
                }
            }

            result *= winners;
        }

        return result;
    }
    
    private long Part2()
    {
        var winners = 0;
        for (var heldTime = 1; heldTime < _big.Time - 1; heldTime++)
        {
            if (heldTime * (_big.Time - heldTime) > _big.Distance)
            {
                winners++;
            }
        }

        return winners;
    }
}