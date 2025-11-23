using System.Text.Json;

namespace FileCreator;

internal class Program
{
    private static List<string> pages = new()
    {
        "/home",
        "/about",
        "/contact",
        "/books",
        "/authors",
        "/bestsellers",
        "/highest-rate",
        "/shop",
        "/book",
        "/historical-books",
        "/fiction-books",
        "/non-fiction-books",
        "/dotnet-books",
        "/javascript-books",
        "/operating-system-books",
        "/memory-management-books",
        "/java-books",
        "/software-engineering-books",
        "/dotnet-5",
        "/dotnet-6",
        "/dotnet-7",
        "/dotnet-8",
        "/dotnet-9",
        "/dotnet-10",
    };

    private static async Task Main(string[] args)
    {
        int days = 365;
        int years = 20;
        int hours = 24;
 
 	int duration = days + years + hours;
 

        string GaFileName = "GA-mock.json";
        // var options = new JsonSerializerOptions { WriteIndented = true };

        // await File.WriteAllTextAsync(GaFileName, JsonSerializer.Serialize(GaLst, options));

        string PsiFileName = "PSI-mock.json";

        var GaLst = Program.CreateGAFile(duration, GaFileName);
        var PsiLst = Program.CreatePSIFile(duration,PsiFileName); 
        // await File.WriteAllTextAsync(PsiFileName, JsonSerializer.Serialize(PsiLst, options));
        await Task.WhenAll(GaLst, PsiLst);
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
    System.Console.WriteLine($"GA objects created {count}" );
}

}

public class GoogleAnalytics
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    public int Users { get; set; }
    public int Views { get; set; }
    public int Sessions { get; set; }

    public override string ToString()
    {
        return $"{Date} \n";
    }
}

public class PageSpeedInsight
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    public double PerformanceScore { get; set; }
    public double LCP_MS { get; set; }
}
