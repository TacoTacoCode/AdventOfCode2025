using System.Numerics;
using Common;

// new complex(rowNr, colNr) - a bit differnt to example colNr,rowNr
List<Complex> redTiles = [];
await FileReadHelper.ReadLineAsync("test2.txt", line =>
{
    string[] splits = line.Split(',');
    redTiles.Add(new Complex(int.Parse(splits[1]), int.Parse(splits[0])));
});

List<Pair> pairs = [];
for (int i = 0; i < redTiles.Count - 1; i++)
{
    for (int j = i; j < redTiles.Count; j++)
    {
        pairs.Add(new Pair(redTiles[i], redTiles[j]));
    }
}
Pair pairMaxArea = pairs.MaxBy(p => p.Area);
//Console.WriteLine($"{pairMaxArea}\t{pairMaxArea.Area}");

// p2
List<Complex> greenTiles = [];
BuildGreenTilesList(redTiles, greenTiles);

pairs.OrderByDescending(p => p.Area)
    .Skip(5000)
    .Take(100)
    .ToList().ForEach(pair => Console.WriteLine(pair.Area));
foreach (Pair pair in pairs.OrderByDescending(p => p.Area).ToList())
{
    if (pair.IsIn(greenTiles))
    {
        Console.WriteLine($"{pair}\t{pair.Area}");

        break;
    }
}

return;

void BuildGreenTilesList(List<Complex> redTiles, List<Complex> greenTiles)
{
    foreach (var group in redTiles.GroupBy(t => t.Real, t => t.Imaginary))
    {
        List<double> orderGroup = group.Order().ToList();
        for (double i = orderGroup.First(); i <= orderGroup.Last(); i++)
        {
            greenTiles.Add(new Complex(group.Key, i));
        }
    }

    foreach (var group in redTiles.GroupBy(t => t.Imaginary, t => t.Real))
    {
        List<double> orderGroup = group.Order().ToList();
        for (double i = orderGroup.First(); i <= orderGroup.Last(); i++)
        {
            greenTiles.Add(new Complex(i, group.Key));
        }
    }
}

public record Pair(Complex R1, Complex R2)
{
    public long Area = CalculateArea(R1, R2);

    public bool IsIn(List<Complex> greenTiles)
    {
        Complex corner1 = new Complex(R1.Real, R2.Imaginary);
        Complex corner2 = new Complex(R2.Real, R1.Imaginary);

        bool corner1InGreenTiles =
            greenTiles.Any(t => t.Real >= corner1.Real && t.Imaginary == corner1.Imaginary) &&
            greenTiles.Any(t => t.Real <= corner1.Real && t.Imaginary == corner1.Imaginary) &&
            greenTiles.Any(t => t.Real == corner1.Real && t.Imaginary >= corner1.Imaginary) &&
            greenTiles.Any(t => t.Real == corner1.Real && t.Imaginary <= corner1.Imaginary);

        bool corner2InGreenTiles =
            greenTiles.Any(t => t.Real >= corner2.Real && t.Imaginary == corner2.Imaginary) &&
            greenTiles.Any(t => t.Real <= corner2.Real && t.Imaginary == corner2.Imaginary) &&
            greenTiles.Any(t => t.Real == corner2.Real && t.Imaginary >= corner2.Imaginary) &&
            greenTiles.Any(t => t.Real == corner2.Real && t.Imaginary <= corner2.Imaginary);

        return corner1InGreenTiles && corner2InGreenTiles;
    }

    private static long CalculateArea(Complex x, Complex y)
    {
        long width = (long)Math.Abs(x.Real - y.Real);
        long length = (long)Math.Abs(x.Imaginary - y.Imaginary);
        return (width + 1) * (length + 1);
    }
}