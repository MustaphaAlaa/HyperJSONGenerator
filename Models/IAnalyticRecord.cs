using System.Text.Json;
using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator;

public interface IAnalyticRecord //: IResettable
{
    public long Date { get; set; }
    public byte Page { get; set; }
    void CreateRandom(long date, byte page, Random random);
    void WriteToJson(Utf8JsonWriter writer); 
}