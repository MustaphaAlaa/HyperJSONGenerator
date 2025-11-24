namespace Metriflow.HyperJSONGenerator;

public class PageSpeedInsight : IAnalyticRecord
{
    public DateTime Date { get; set; }
    public string Page { get; set; }

    public double PerformanceScore { get; set; }
    public double LCP_MS { get; set; }


    public static IAnalyticRecord CreateRandom(DateTime date, string page)
        => new PageSpeedInsight()
        {
            Date = date,
            Page = page,
            PerformanceScore = Random.Shared.Next(100),
            LCP_MS = Random.Shared.Next(1_800_000)
        };

    public bool TryReset()
    {
        Date = default;
        Page = null;
        PerformanceScore = 0;
        LCP_MS = 0;
        return true;
    }
}