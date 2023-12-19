using NetTopologySuite.Geometries;

namespace advent_2023;

public class Day18 : Day
{
    public Day18(string filename) : base(filename)
    {
    }

    public void Solve()
    {
        Console.WriteLine(Combined());
        Console.WriteLine(Combined(true));
    }

    private long Combined(bool p2=false)
    {
        var points = new List<Point>();
        var count = 0L;
        
        var current = new Point(0, 0);
        foreach (var line in Lines)
        {
            var split = line.Split(' ');
            var p2Dir = split[2][2..8].Last().ToString();
            var increment = p2 
                ? Convert.ToInt64(string.Concat(split[2][2..8].Take(5)), 16) 
                : int.Parse(split[1]);
            var direction = p2 
                ? p2Dir 
                : split[0];

            var (x, y) = GetIncrement(direction, increment);

            points.Add(current);
            var newCurr = new Point(current.X + x, current.Y + y);
            current = newCurr;
            points.Add(current);
            count += increment;
        }

        var geoFactory = new GeometryFactory();
        var poly = geoFactory.CreatePolygon(points.Select(p => new Coordinate(p.X, p.Y)).ToArray());
        return Convert.ToInt64(poly.Area + count / 2.0 + 1);
    }
    
    private static (long, long) GetIncrement(string val, long increment)
    {
        return val switch
        {
            "R" or "0" => (increment, 0),
            "D" or "1" => (0, -increment),
            "L" or "2" => (-increment, 0),
            "U" or "3" => (0, increment),
            _ => throw new Exception("Did you enter the wrong input? Or are you just a dingus?")
        };
    }
}