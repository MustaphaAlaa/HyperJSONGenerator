using System.Diagnostics;
using Metriflow.HyperJSONGenerator.Service;

namespace Metriflow.HyperJSONGenerator;

internal class Program
{
    public static List<String> pages = (new Pages()).GetPages();

    static string projectRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Json-Files/")
    );

    static string GaFileName = Path.Combine(projectRoot, "GA-mock.json");
    static string PsiFileName = Path.Combine(projectRoot, "PSI-mock.json");

    private static async Task Main(string[] args)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        int days = 1;
        int years = 1;
        int hours = 1;

        int duration = days * years * hours;
        
        var GaLst = JsonsGenerator.Generate<GoogleAnalytics>(pages, duration, GaFileName);
        var PsiLst = JsonsGenerator.Generate<GoogleAnalytics>(pages, duration, PsiFileName);
        await Task.WhenAll(GaLst, PsiLst);
        
        stopwatch.Stop();
        System.Console.WriteLine($"All files are created in {stopwatch.ElapsedMilliseconds / 1000} sec");
    }
}