using System.Numerics;
using Common;

// Complex(rowNr, colNr)
List<Complex> splitters = [];
Complex start = new Complex();
int countLine = await FileReadHelper.ReadLineAsync("test2.txt", (line, li) =>
{
    for (int i = 0; i < line.Length; i++)
    {
        if (line[i] == 'S')
        {
            start = new Complex(li, i);
        }
        else if (line[i] == '^')
        {
            splitters.Add(new Complex(li, i));
        }
    }
});

Dictionary<Complex, long> countTimeLineBySpliter = [];

long countTimeLine = StartMove(start, countTimeLineBySpliter);

Console.WriteLine(countTimeLineBySpliter.Keys.Count);
Console.WriteLine(countTimeLine);

return;

long StartMove(Complex begin, Dictionary<Complex, long> countTimeLineBySpliter)
{
    Complex current = begin;
    while (true)
    {
        current = current.MoveDown();
        if (current.Real >= countLine - 1)
        {
            // touch the bottom
            return 1;
        }

        if (splitters.Contains(current))
        {
            if (!countTimeLineBySpliter.TryGetValue(current, out long value))
            {
                value = StartMove(current.MoveLeft(), countTimeLineBySpliter) +
                        StartMove(current.MoveRight(), countTimeLineBySpliter);

                countTimeLineBySpliter.Add(current, value);
            }

            return value;
        }
    }
}