using Common;

List<Point> points = [];
await FileReadHelper.ReadLineAsync("test2.txt", line =>
{
    string[] splits = line.Split(',', StringSplitOptions.TrimEntries);
    Point p = new Point(
        int.Parse(splits[0]),
        int.Parse(splits[1]),
        int.Parse(splits[2]),
        'c');// debug only (char)(points.Count + 65));

    points.Add(p);
});

List<Line> lines = [];
for (int i = 0; i < points.Count - 1; i++)
{
    for (int j = i + 1; j < points.Count; j++)
    {
        Line l = new Line(points[i], points[j]);
        lines.Add(l);
    }
}

List<Group> groups = [];
LinkPoints(groups, 1000);
long result1 = groups
    .Select(g => g.Points.Count)
    .OrderDescending()
    .Take(3)
    .Aggregate(1L, (x, y) => x * y);
Console.WriteLine(result1);

// p2
groups.Clear();
Line finshLine = LinkPoints(groups, int.MaxValue);
long result2 = (long)finshLine.P1.X * (long)finshLine.P2.X;
Console.WriteLine(result2);

return;

Line LinkPoints(List<Group> result, int numberOfLink)
{
    List<Line> linesToProcess = lines
                                    .OrderBy(l => l.Length)
                                    .Take(numberOfLink)
                                    .ToList();

    foreach (Line line in linesToProcess)
    {
        List<Group> toRemove = [];
        bool preventAdd = false;
        Group combine = new Group([line.P1, line.P2]);
        foreach (Group group in result)
        {
            if (group.ContainGroup(combine))
            {
                preventAdd = true;

                break;
            }
            if (group.CanCombine(combine))
            {
                toRemove.Add(group);
                combine = group.Combine(combine);

                continue;
            }
        }

        result.RemoveAll(toRemove.Contains);
        if (!preventAdd)
        {
            result.Add(combine);
        }

        if (result.Count == 1 && combine.Points.Count == points.Count)
        {
            return line;
        }
    }

    return linesToProcess.Last();
}
public record Group(List<Point> Points)
{
    public bool ContainPoint(Point p)
    {
        return Points.Contains(p);
    }

    public bool ContainGroup(Group g)
    {
        return Points.All(p => g.ContainPoint(p));
    }

    public bool CanCombine(Group g)
    {
        return Points.Any(p => g.ContainPoint(p));
    }

    public Group Combine(Group g)
    {
        HashSet<Point> set = [.. Points, .. g.Points];
        return new Group(set.ToList());
    }
}
public record Line(Point P1, Point P2)
{
    public double Length = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) +
                                        Math.Pow(P1.Y - P2.Y, 2) +
                                        Math.Pow(P1.Z - P2.Z, 2));

    public override string ToString()
    {
        return $"{P1},{P2}";
    }
}
public record Point(int X, int Y, int Z, char Prepresent)
{
    public override string ToString()
    {
        return Prepresent.ToString(); //$"({X},{Y},{Z})";
    }
}