namespace advent_2023;

public class Day9 : Day
{
    public Day9(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private long Part1()
    {
        return Predict();
    }

    private long Part2()
    {
        return Predict(true);
    }

    private long Predict(bool p2=false)
    {
        var result = 0L;
        foreach (var line in Lines)
        {
            var nums = line.Split(" ").Select(long.Parse).ToArray();
            if (p2)
            {
                nums = nums.Reverse().ToArray();
            }
            
            result += PredictHelper(nums, nums[1..], nums[1..].Last());
        }

        return result;
    }
    
    private static long PredictHelper(long[] nums, long[] nextNums, long result)
    {
        while (nextNums.Any(x => x != 0))
        {
            for (var i = 0; i < nextNums.Length; i++)
            {
                nextNums[i] -= nums[i];
            }

            result += nextNums.Last();
            nums = nextNums;
            nextNums = nextNums[1..];
        }
        return result;
    }
}