namespace advent_2023;

public class Day15 : Day
{
    private readonly string _line;
    
    public Day15(string filename) : base(filename)
    {
        _line = Lines.FirstOrDefault()?.Trim()
                ?? throw new Exception("Did you forget to populate the input file?");
    }

    public void Solve()
    {
        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private int Part1()
    {
        return _line.Split(',').Sum(HashAlgo);
    }
    
    private int Part2()
    {
        var boxes = new Dictionary<int, List<string>>();
        FillBoxes(boxes);        

        var result = 0;
        foreach (var (key, value) in boxes)
        {
            var currentSlot = 1;
            result += value.Sum(seq => (key + 1) * currentSlot++ * int.Parse(seq.Split('=')[1]));
        }
        
        return result;
    }

    private static int HashAlgo(string sequence)
    {
        return sequence.Aggregate(0, (current, c) => (current + c) * 17 % 256);
    }

    private void FillBoxes(Dictionary<int, List<string>> boxes)
    {
        foreach (var seq in _line.Split(','))
        {
            var separator = seq.Contains('=') ? '=' : '-';
            var label = seq.Split(separator)[0];
    
            var algoResult = HashAlgo(label);
            boxes.TryAdd(algoResult, new List<string>());

            if (separator == '=')
            {
                var possibleInd = boxes[algoResult].FindIndex(s => s.StartsWith(label));
                if (possibleInd != -1)
                {
                    boxes[algoResult][possibleInd] = seq;
                }
                else
                {
                    boxes[algoResult].Add(seq);
                }
            }
            else
            {
                boxes[algoResult].Remove(boxes[algoResult].Find(x => x.Split('=')[0] == label) ?? string.Empty);
            }
        }
    }
}