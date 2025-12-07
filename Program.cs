using System.Diagnostics;
using Metriflow.HyperJSONGenerator.Service;

namespace Metriflow.HyperJSONGenerator;

internal class Program
{
 
    static string projectRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Json-Files/")
    );

    static readonly string _googleAnalyticsFileName = Path.Combine(projectRoot, "GA-mock.json");
    static readonly string _pageSpeedInsightFileName = Path.Combine(projectRoot, "PSI-mock.json");
        
    private static async Task Main(string[] args)
    { 

        System.Console.WriteLine("Start generating  json files.....");
    
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        int days = 365;
        int years = 20;
        int hours = 24;

        //the number of records for each json file will be duration * number of pages (23)
        int duration = days * years * hours;


        var GoogleAnalyticsTask = JsonsGenerator.Generate<GoogleAnalytics>(duration, _googleAnalyticsFileName);
        var PageSpeedInsightTask =
            JsonsGenerator.Generate<PageSpeedInsight>(duration, _pageSpeedInsightFileName);
        await Task.WhenAll(GoogleAnalyticsTask, PageSpeedInsightTask);
        stopwatch.Stop();

        var lessThanSecond = stopwatch.ElapsedMilliseconds < 1000;
        var time = lessThanSecond ? stopwatch.ElapsedMilliseconds : stopwatch.ElapsedMilliseconds / 1000;
        var timeUnit = lessThanSecond ? "ms" : "sec";
        Console.WriteLine($"All files are created in {time}/{timeUnit}");
    }
}
