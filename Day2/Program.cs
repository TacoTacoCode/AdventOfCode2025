using System.Text;
using Common;

List<Range> inputs = [];
await FileReadHelper.ReadFileAsync("test2.txt", line =>
{
    foreach (var item in line.Split(","))
    {
        var temp = item.Split("-");
        inputs.Add(new Range(long.Parse(temp[0]), long.Parse(temp[1])));
    }
});

CountInvalidIds(inputs);

return;

void CountInvalidIds(List<Range> inputs)
{
    long result1 = 0;
    long result2 = 0;

    foreach (var (begin, end) in inputs)
    {
        for (long i = begin; i <= end; i++)
        {
            // or simply (s + s).Substring(1, 2 * s.Length - 2).Contains(s)
            var (p1, p2) = IsRepeat(i);
            result1 += p1;
            result2 += p2;
        }
    }

    Console.WriteLine(result1);
    Console.WriteLine(result2);
}

(long, long) IsRepeat(long number)
{
    var numberAsString = number.ToString();
    var len = numberAsString.Length;
    var repeatSb = new StringBuilder(len / 2);
    for (int repeatLen = 1; repeatLen <= len / 2; repeatLen++)
    {
        repeatSb.Append(numberAsString.AsSpan(repeatLen - 1, 1));

        var textS = new StringBuilder(len);
        var numberOfRepeat = len / repeatLen;
        for (int i = 1; i <= numberOfRepeat; i++)
        {
            textS.Append(repeatSb);
        }

        if (textS.ToString() == numberAsString)
        {
            if (numberOfRepeat == 2) // part 1
            {
                return (number, number);
            }
            return (0, number);
        }
    }

    return (0, 0);
}

public record Range(long Begin, long End);