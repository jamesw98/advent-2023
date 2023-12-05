using System.Text.RegularExpressions;

namespace advent_2023;

// Note: I stripped 'Card ###: ' from the input   
public partial class Day4 : Day
{
    private const string Pipe = " | ";
    private const string Space = " ";
    
    public Day4(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        var (p1, p2) = Combined();
        Console.WriteLine(p1);
        Console.WriteLine(p2);
    }

    private (int, int) Combined()
    {
        // Build a dictionary of <index, count> 
        var cardsProduced = new Dictionary<int, int>();
        Lines.Each((_, ind) => cardsProduced.Add(ind, 1));
        
        var total = 0;
        Lines.Each((line, ind) =>
        {
            var (winners, card) = GetWinnersAndCard(line);

            var score = 0;
            var numberOfWins = 0;
            // Check each number on a card to see if it's a winner
            foreach (var num in card)
            {
                if (winners.Contains(num))
                {
                    // Calculations for part 1
                    if (score == 0) score = 1;
                    else score *= 2;
                    // For part 2
                    numberOfWins++;
                }
            }
            
            // Calculate part 2 related things
            for (var i = 1; i <= numberOfWins; i++)
            {
                var copyInd = ind + i;
                if (copyInd >= Lines.Length) break;
                cardsProduced[copyInd] += cardsProduced[ind];
            }

            total += score;
        });
        
        return (total, cardsProduced.Values.Sum());
    }

    private static (string[], string[]) GetWinnersAndCard(string line)
    {
        // Clean up the line
        var fixedLine = Int().Replace(line, Space);
        
        var winners = fixedLine.Split(Pipe)[0]
            .Trim()
            .Split(Space);
    
        var card = fixedLine.Split(Pipe)[1]
            .Trim()
            .Split(Space);

        return (winners, card);
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex Int();
}