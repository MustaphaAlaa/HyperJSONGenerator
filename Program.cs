using System.Diagnostics;
using Metriflow.HyperJSONGenerator.Service;

namespace Metriflow.HyperJSONGenerator;

internal class Program
{
    public static List<String> pages = (new Pages()).GetPages();

    static string projectRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Json-Files/")
    );

    static readonly string _googleAnalyticsFileName = Path.Combine(projectRoot, "GA-mock.json");
    static readonly string _pageSpeedInsightFileName = Path.Combine(projectRoot, "PSI-mock.json");

    private static async Task Main(string[] args)
    {
        System.Console.WriteLine("Starting JSON files generation...");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        int days = 365;
        int years = 40;
        int hours = 24;

        int duration = days * years * hours;

        var GoogleAnalyticsTask = JsonsGenerator.Generate<GoogleAnalytics>(pages, duration, _googleAnalyticsFileName);
        var PageSpeedInsightTask =
            JsonsGenerator.Generate<PageSpeedInsight>(pages, duration, _pageSpeedInsightFileName);
        await Task.WhenAll(GoogleAnalyticsTask, PageSpeedInsightTask);

        stopwatch.Stop();
        Console.WriteLine($"All files are created in {stopwatch.ElapsedMilliseconds / 1000} sec");
    }
}