using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator;

public interface IAnalyticRecord : IResettable
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    static abstract IAnalyticRecord CreateRandom(DateTime date, string page);
}