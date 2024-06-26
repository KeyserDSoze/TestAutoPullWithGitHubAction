using System.Diagnostics;

namespace TestAutoPullWithGitHubAction
{
    internal class Composer
    {
        public async Task CommitAndPushAsync(string path, string newVersion, string? githubToken)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    WorkingDirectory = path
                },
            };

            process.Start();
            using (var writer = process.StandardInput)
            {
                writer.WriteLine("git init");
                writer.WriteLine($"git remote set-url origin https://KeyserDSoze:{githubToken}@github.com/KeyserDSoze/TestAutoPullWithGitHubAction.git");
                writer.WriteLine("git add .");
                writer.WriteLine($"git commit --author=\"alessandro rapiti <alessandro.rapiti44@gmail.com>\" -m \"new version v.{newVersion}\"");
                writer.WriteLine("git push --set-upstream origin master");
            }
            await process.WaitForExitAsync();
        }
    }
}
