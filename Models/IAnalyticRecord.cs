namespace Metriflow.HyperJSONGenerator;

public interface IAnalyticRecord
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    static abstract IAnalyticRecord CreateRandom(DateTime date, string page);
}