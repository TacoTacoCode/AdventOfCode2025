namespace Common;

public class FileReadHelper
{
    public static async Task ReadLineAsync(string fileName, Action<string> actionByLine)
    {
        using (var stream = new StreamReader(Path.Combine(Environment.CurrentDirectory, fileName)))
        {
            string line;
            while ((line = await stream.ReadLineAsync()) is not null)
            {
                try
                {
                    actionByLine(line);
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed at: {0}", line);
                    throw;
                }
            }
        }
    }

    public static async Task ReadFileAsync(string fileName, Action<string> actionOnContent)
    {
        using (var stream = new StreamReader(Path.Combine(Environment.CurrentDirectory, fileName)))
        {
            var content = await stream.ReadToEndAsync();
            actionOnContent(content);
        }
    }

    public static async Task<int> ReadLineAsync(string fileName, Action<string, int> actionByLine)
    {
        int index = -1;
        using (var stream = new StreamReader(Path.Combine(Environment.CurrentDirectory, fileName)))
        {
            string line;
            while ((line = await stream.ReadLineAsync()) is not null)
            {
                index++;
                try
                {
                    actionByLine(line, index);
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed at: {0}", line);
                    throw;
                }
            }
        }
        return index + 1; // count line
    }
}
