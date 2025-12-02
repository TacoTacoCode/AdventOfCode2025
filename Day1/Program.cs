using Common;

await Solver("./test1.txt", false);
await Solver("./test1.txt", true);
await Solver("./test2.txt", false);
await Solver("./test2.txt", true);
return;

async Task Solver(string fileName, bool includePassover)
{
    List<int> instruction = [];
    await FileReadHelper.ReadLineAsync(fileName, line =>
    {
        if (line.StartsWith('L'))
        {
            instruction.Add(int.Parse(line[1..]) * -1);
        }
        else if (line.StartsWith('R'))
        {
            instruction.Add(int.Parse(line[1..]));
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    });

    int result = 0;
    int curPos = 50;
    foreach (var item in instruction)
    {
        var virtualPos = curPos + item;
        var realPos = virtualPos % 100;

        // stop at 0 case (..., -200, -100, 0, 100, 200...)
        if (realPos == 0)
        {
            result++;
        }

        // part 2
        if (includePassover)
        {
            result += Math.Abs(virtualPos / 100);
            // cross 0 case
            if (curPos * virtualPos < 0)
            {
                result += 1;
            }

            if (virtualPos != 0 && realPos == 0)
            {
                result -= 1; //at it already covered in stop at 0 case above
            }
        }

        if (realPos >= 0)
        {
            curPos = realPos;
        }
        else
        {
            curPos = 100 + realPos;
        }

        //Console.WriteLine($"{item}\t{virtualPos}\t{curPos}\t{result}");
    }

    Console.WriteLine(result);
}