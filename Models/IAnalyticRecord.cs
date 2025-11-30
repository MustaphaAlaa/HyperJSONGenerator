namespace Metriflow.HyperJSONGenerator;

public interface IAnalyticRecord
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    void CreateRandom(DateTime date, string page);
    public bool Reset();
}