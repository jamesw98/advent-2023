namespace advent_2023;

public class Day11 : Day
{
    public Day11(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        Console.WriteLine(Combined(false));
        Console.WriteLine(Combined(true));
    }

    private class Point
    {
        public long X { get; set; }
        public long Y { get; set; }

        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }
    }
    
    private long Combined(bool p2)
    {
        var expandedX = new Dictionary<long, List<int>>();
        var expandedY = new Dictionary<long, List<int>>();
        
        var points = new List<Point>();
        for (var y = 0; y < Lines.Length; y++)
        {
            expandedY[y] = new();   
            for (var x = 0; x < Lines[y].Length; x++)
            {
                var count = points.Count - 1;
                if (y == 0) expandedX[x] = new();
                
                if (Lines[y][x] == '#')
                {
                    points.Add(new Point(x, y));
                    expandedX[x].Add(count);
                    expandedY[y].Add(count);
                }    
            }
        }
        
        Expand(expandedX, points, true, p2);
        Expand(expandedY, points, false, p2);
        return points.Sum(start => points.Sum(end => ManhattanDistance(start, end))) / 2;
    }

    private static long ManhattanDistance(Point start, Point end)
    {
        return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
    }

    private static void Expand(Dictionary<long, List<int>> indexes, List<Point> points, bool x, bool p2)
    {
        var expand = 0L;
        var num = p2 ? 1000000 - 1 : 1; 
        foreach (var inds in indexes.Values)
        {
            if (inds.Count == 0)
            {
                expand += num;
                continue;
            }

            foreach (var i in inds)
            {
                if (x) points[i].X += expand;
                else points[i].Y += expand;
            }
        }
    }
}