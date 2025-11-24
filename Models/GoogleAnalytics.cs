namespace Metriflow.HyperJSONGenerator;

public class GoogleAnalytics : IAnalyticRecord
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    public int Users { get; set; }
    public int Views { get; set; }
    public int Sessions { get; set; }

    public void Reset()
    {
        Date = default;
        Page = null;
        Users = 0;
        Views = 0;
        Sessions = 0;
    }

    public static IAnalyticRecord CreateRandom(DateTime date, string page)
        => new GoogleAnalytics()
        {
            Date = date,
            Page = page,
            Users = Random.Shared.Next(2_000_000),
            Sessions = Random.Shared.Next(200),
            Views = Random.Shared.Next(2_000_000)
        };
}