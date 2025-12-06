using System.Text.Json;

namespace Metriflow.HyperJSONGenerator;

public struct GoogleAnalytics : IAnalyticRecord
{
    public long Date { get; set; }
    public byte Page { get; set; }
    public int Users { get; set; }
    public int Views { get; set; }
    public int Sessions { get; set; }
    public void CreateRandom(long date, byte page, Random random)

    {
        this.Date = date;
        this.Page = page;
        this.Users = random.Next(2_000_000);
        this.Sessions = random.Next(200);
        this.Views = random.Next(2_000_000);
    }

    public void WriteToJson(Utf8JsonWriter writer)
    {
        writer.WriteNumber("Users", Users);
        writer.WriteNumber("Views", Views);
        writer.WriteNumber("Sessions", Sessions);
    }
}