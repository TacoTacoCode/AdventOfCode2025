using System.Text;
using Common;

List<string> banks = [];
await FileReadHelper.ReadLineAsync("test2.txt", line =>
{
    banks.Add(line);
});

GetResult(2);
GetResult(12);

return;

void GetResult(int len)
{
    long result = 0;
    StringBuilder sb = new StringBuilder(len);
    for (int i = 0; i < banks.Count; i++)
    {
        BuildIndexList(banks[i], len, sb);
        result += long.Parse(sb.ToString());
        sb.Clear();
    }

    Console.WriteLine(value: result);
}

void BuildIndexList(string b, int len, StringBuilder buidler)
{
    // Console.WriteLine($"{b}\t{len}\t{buidler}");
    if (len == 0)
    {
        return;
    }
    // take everything left or nothing left
    if (b.Length == len)
    {
        buidler.Append(b);
        // Console.WriteLine($"{buidler}");   
        return;
    }

    int checkNumber = 9;
    while (checkNumber != 0)
    {
        var indexOfCheckNr = b.IndexOf(checkNumber.ToString());
        if (indexOfCheckNr != -1)
        {
            var lenFromIndex = (b.Length - 1) - indexOfCheckNr + 1;
            if (lenFromIndex >= len)
            {
                // if from that pos, we still have enough number to build, we will take it
                buidler.Append(b[indexOfCheckNr]);
                BuildIndexList(b.Substring(indexOfCheckNr + 1), len - 1, buidler);

                break;
            }
        }

        checkNumber--;
    }
}
/* P1 old way
int GetLargestNumber(string b)
{
    List<int> bank = [.. b.Select(x => int.Parse(x.ToString()))];
    int[] maxNumber = new int[10];

    for (int i = 0; i < bank.Count; i++)
    {
        var firstNumber = bank[i];
        if (maxNumber[firstNumber] != 0 || i == bank.Count - 1)
        {
            continue;
        }

        var secondNumber = bank[i + 1];
        for (var j = i + 2; j < bank.Count; j++)
        {
            if (bank[j] > secondNumber)
            {
                secondNumber = bank[j];
            }
        }
        maxNumber[firstNumber] = firstNumber * 10 + secondNumber;
    }

    for (int i = 9; i > 0; i--)
    {
        if (maxNumber[i] != 0)
        {
            return maxNumber[i];
        }
    }

    Console.WriteLine("Break the fourth wall");
    return -1;
} */