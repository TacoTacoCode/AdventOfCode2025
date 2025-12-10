using Common;

List<Range> freshRanges = [];
List<long> ingredients = [];
await FileReadHelper.ReadLineAsync("test2.txt", line =>
{
    if (line.Contains('-'))
    {
        var splits = line.Split('-', StringSplitOptions.TrimEntries);
        freshRanges.Add(new(long.Parse(splits[0]), long.Parse(splits[1])));
    }
    else if (!string.IsNullOrEmpty(line))
    {
        ingredients.Add(long.Parse(line));
    }
});
Console.WriteLine(ingredients
    .Where(i => freshRanges.Any(r => r.IsContain(i)))
    .Count());

List<Range> freshRangesCheckOverLap = [freshRanges[0]];
foreach (Range r1 in freshRanges.Skip(1))
{
    List<Range> toRemove = [];
    bool preventAdd = false;
    Range combine = r1;
    foreach (Range r2 in freshRangesCheckOverLap)
    {
        if (r2.IsContain(combine))
        {
            preventAdd = true;
            break;
        }
        if (combine.IsContain(r2))
        {
            toRemove.Add(r2);

            continue;
        }
        if (combine.IsOverlap(r2))
        {
            toRemove.Add(r2);
            combine = combine.Combine(r2);

            continue;
        }
    }

    freshRangesCheckOverLap.RemoveAll(toRemove.Contains);
    if (!preventAdd)
    {
        freshRangesCheckOverLap.Add(combine);
    }
}
// freshRangesCheckOverLap.ForEach(r => Console.WriteLine(r));
Console.WriteLine(freshRangesCheckOverLap
    .Aggregate(0L, (curCount, nextRange) => curCount + nextRange.Count()));

public record Range(long Begin, long End)
{
    public long Count() => End - Begin + 1;
    public bool IsContain(long ingredient) => ingredient >= Begin && ingredient <= End;
    public bool IsContain(Range other) => Begin <= other.Begin && End >= other.End;
    public bool IsOverlap(Range other)
    {
        return other.IsContain(End) || this.IsContain(other.End);
    }
    // only use when overlap
    public Range Combine(Range other)
    {
        return Begin <= other.Begin
        ? new Range(Begin, other.End)
        : new Range(other.Begin, End);
    }
}