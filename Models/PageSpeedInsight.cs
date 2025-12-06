using System.Text.Json;

namespace Metriflow.HyperJSONGenerator;

public struct PageSpeedInsight : IAnalyticRecord
{
    public long Date { get; set; }
    public byte Page { get; set; }

    public double PerformanceScore { get; set; }
    public double LCP_MS { get; set; } 

    public   void SetRandoms(long date, byte page, Random random)     
        {
            Date = date;
            Page = page;
            PerformanceScore = random.Next(100);
            LCP_MS = random.Next(1,1_800_000);
        }

    public void WriteToJson(Utf8JsonWriter writer)
    {
        writer.WriteNumber("PerformanceScore", PerformanceScore);
        writer.WriteNumber("LCP_MS", LCP_MS);
    } 
}