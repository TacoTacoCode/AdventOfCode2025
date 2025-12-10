using System.Numerics;
using Common;

// complex(rowNr, colNr)
List<Complex> rollPositionList = [];
await FileReadHelper.ReadLineAsync("test2.txt", (line, li) =>
{
    for (int i = 0; i < line.Length; i++)
    {
        if (line[i] == '@')
        {
            rollPositionList.Add(new(li, i));
        }
    }
});

int result = 0;
foreach (var rollPosition in rollPositionList)
{
    if (IsSurroundedByLessThan4Rolls(rollPosition, rollPositionList))
    {
        result++;
    }
}
Console.WriteLine(result);

List<Complex> removedRolls = [];
while (true)
{
    int numberOfRemovedRoll = 0;
    List<Complex> newRollPositionList = rollPositionList.Except(removedRolls).ToList();
    foreach (Complex rollPosition in newRollPositionList)
    {
        if (IsSurroundedByLessThan4Rolls(rollPosition, newRollPositionList))
        {
            removedRolls.Add(rollPosition);
            numberOfRemovedRoll++;
        }
    }
    if (numberOfRemovedRoll == 0)
    {
        break;
    }
}
Console.WriteLine(removedRolls.Count);

return;

bool IsSurroundedByLessThan4Rolls(Complex rollPosition, List<Complex> rollPositionList)
{
    Func<Complex, int> countRollIn = direction =>
    {
        var newPos = rollPosition + direction;
        return rollPositionList.Contains(newPos) ? 1 : 0;
    };

    var countRoll = countRollIn(new(-1, -1)) +
                        countRollIn(new(-1, 0)) +
                        countRollIn(new(-1, 1)) +
                        countRollIn(new(0, -1)) +
                        countRollIn(new(0, 1)) +
                        countRollIn(new(1, -1)) +
                        countRollIn(new(1, 0)) +
                        countRollIn(new(1, 1));
    return countRoll < 4;
}

