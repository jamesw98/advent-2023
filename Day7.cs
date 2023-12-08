namespace advent_2023;

public class Day7 : Day
{
    public Day7(string filename) : base(filename)
    {
    }

    private static string _order = "AKQJT98765432";

    private enum HandType
    {
        Five,
        Four,
        Full,
        Three,
        TwoP,
        OneP,
        High
    }

    private class Hand
    {
        public int Rank { get; set; } = 0;
        public string Cards { get; set; }
        public long Bid { get; set; }
        public HandType HandType { get; set; }
        public IEnumerable<(char, int)> Details { get; set; }
        public int Score { get; set; } = 0;

        public Hand(string c, long b)
        {
            Cards = c;
            Bid = b;
        }

        public override string ToString()
        {
            return $"{Cards} {Bid} {HandType}";
        }

        public int CompareTo(Hand other)
        {
            if (HandType != other.HandType)
            {
                return HandType < other.HandType 
                    ? 1 
                    : -1;
            }
            
            var i = 0;
            foreach (var c in Cards)
            {
                if (c != other.Cards[i])
                {
                    return _order.IndexOf(c) < _order.IndexOf(other.Cards[i])
                        ? 1
                        : -1;
                }

                i++;
            }
            return 0;
        }
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private long Part1()
    {
        var result = 0L;

        var hands = Lines.Select(line =>
        {
            var split = line.Split(" ");
            return new Hand(split[0], long.Parse(split[1]));
        }).ToList();
        
        foreach (var hand in hands)
        {
            var distinctWithCount = hand.Cards
                .GroupBy(x => x)
                .Select(y => (y.Key.ToString()[0], y.Count()));

            hand.HandType = distinctWithCount switch
            {
                _ when distinctWithCount.Any(x => x.Item2 == 5) => HandType.Five,
                _ when distinctWithCount.Any(x => x.Item2 == 4) => HandType.Four,
                _ when distinctWithCount.Any(x => x.Item2 == 3) && distinctWithCount.Any(x => x.Item2 == 2) => HandType.Full,
                _ when distinctWithCount.Any(x => x.Item2 == 3) && distinctWithCount.Count(x => x.Item2 == 1) == 2 => HandType.Three,
                _ when distinctWithCount.Count(x => x.Item2 == 2) == 2 => HandType.TwoP,
                _ when distinctWithCount.Count(x => x.Item2 == 1) == 5 => HandType.High,
                _ => HandType.OneP
            };
        }

        hands.Sort((x, y) => x.CompareTo(y));
        hands.Each((hand, i) =>
        {
            result += (hand.Bid * (i + 1));
        });
        return result;
    }

    private long Part2()
    {
        // var result = 1;
        var result = 0; 
        
        foreach (var line in Lines)
        {
            
        }
        
        return result;
    }
}