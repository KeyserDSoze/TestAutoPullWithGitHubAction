// See https://aka.ms/new-console-template for more information
using TestAutoPullWithGitHubAction;

public static class Program
{
    public static void Main(string[] args)
    {
        var x = args.ToDictionary(x => x.Split('=')[0], x =>
        {
            var c = x.Split('=');
            if (c.Length > 1)
                return c[1];
            else
                return null;
        });
        foreach (var kvp in x)
            Console.WriteLine($"{kvp.Key}={kvp.Value}");
        var path = x["path"];
        var newFile = $"{path}/TestAutoPullWithGitHubAction/newFile.txt";

        var fileInfo = new FileInfo(newFile);
        if (fileInfo.Exists)
            fileInfo.Delete();
        using (var stream = fileInfo.CreateText())
        {
            stream.Write(Guid.NewGuid().ToString());
        }
        new Composer().CommitAndPushAsync(path, x["v"], x["t"]).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}