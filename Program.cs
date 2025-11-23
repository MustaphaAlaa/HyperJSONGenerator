using System.Diagnostics;
using System.Text.Json;

namespace FileCreator;

internal class Program
{
    public static List<String> pages = (new Pages()).GetPages();

    private static async Task Main(string[] args)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        int days = 365;
        int years = 20;
        int hours = 24;

        int duration = days * years * hours;


        string projectRoot = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..","Json-Files/")
        );
      
        var GaFileName = Path.Combine(projectRoot, "GA-mock.json");
        var PsiFileName = Path.Combine(projectRoot, "PSI-mock.json");
       
  


        var GaLst = Program.CreateGAFile(duration, GaFileName);
        var PsiLst = Program.CreatePSIFile(duration, PsiFileName);
        // await File.WriteAllTextAsync(PsiFileName, JsonSerializer.Serialize(PsiLst, options));
        await Task.WhenAll(GaLst, PsiLst);
        stopwatch.Stop();
        System.Console.WriteLine($"All files are created in {stopwatch.ElapsedMilliseconds / 1000} sec");
    }


    public static async Task CreatePSIFile(int number, string fileName)
    {
        await using var file = File.CreateText(fileName);
        await file.WriteAsync("[");

        var first = true;
        var date = new DateTime(1995, 1, 1);
        var count = 0;
        for (int i = 0; i < number; i++, date = date.AddHours(1))
        {
            for (int pageIdx = 0; pageIdx < pages.Count; pageIdx++)
            {
                count++;
                var obj = new PageSpeedInsight
                {
                    Date = date,
                    Page = pages[pageIdx],
                    PerformanceScore = Random.Shared.Next(100),
                    LCP_MS = Random.Shared.Next(1_800_000)
                };

                if (!first)
                    await file.WriteAsync(",\n");

                string json = JsonSerializer.Serialize(obj);
                await file.WriteAsync(json);

                first = false;
            }
        }

        await file.WriteAsync("]");
        System.Console.WriteLine($"PSI objects created {count}");
    }

    public static async Task CreateGAFile(int number, string fileName)
    {
        await using var file = File.CreateText(fileName);
        await file.WriteAsync("["); // Start JSON array

        var first = true;
        var date = new DateTime(1995, 1, 1);
        var count = 0;

        for (int i = 0; i < number; i++, date = date.AddHours(1))
        {
            for (int pageIdx = 0; pageIdx < pages.Count; pageIdx++)
            {
                count++;
                var obj = new GoogleAnalytics
                {
                    Date = date,
                    Users = Random.Shared.Next(int.MaxValue - 5),
                    Sessions = Random.Shared.Next(),
                    Views = Random.Shared.Next(),
                    Page = pages[pageIdx]
                };

                if (!first)
                    await file.WriteAsync(",\n");

                string json = JsonSerializer.Serialize(obj);
                await file.WriteAsync(json);

                first = false;
            }
        }

        await file.WriteAsync("]");
        System.Console.WriteLine($"GA objects created {count}");
    }
}