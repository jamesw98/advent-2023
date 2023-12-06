namespace advent_2023;

public class Day
{
    protected readonly string[] Lines;
    protected const string Base = @"F:\dev\advent-2023\inputs\";

    protected Day(string filename)
    {
        Lines = File.ReadAllLines($"{Base}{filename}");
    }
}