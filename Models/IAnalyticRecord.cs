using System.Text.Json;

namespace Metriflow.HyperJSONGenerator;

public interface IAnalyticRecord 
{
    public long Date { get; set; }
    public byte Page { get; set; }
    void SetRandoms(long date, byte page, Random random);
    void WriteToJson(Utf8JsonWriter writer); 
}