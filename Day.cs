namespace advent_2023;

public class Day
{
    protected string[] Lines;

    protected Day(string filename)
    {
        Lines = File.ReadAllLines($@"F:\dev\advent-2023\inputs\{filename}");
    }
}