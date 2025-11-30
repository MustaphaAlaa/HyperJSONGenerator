using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator;

public class GoogleAnalytics : IAnalyticRecord
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    public int Users { get; set; }
    public int Views { get; set; }
    public int Sessions { get; set; }

    public bool Reset()
    {
        Date = default;
        Page = null;
        Users = 0;
        Views = 0;
        Sessions = 0;
        return true;
    }

    public void CreateRandom(DateTime date, string page)

    {
        this.Date = date;
        this.Page = page;
        this.Users = Random.Shared.Next(2_000_000);
        this.Sessions = Random.Shared.Next(200);
        this.Views = Random.Shared.Next(2_000_000);
    }

}

    public class DefaultObjectPoolPolicy<T> : PooledObjectPolicy<T> where T : class, IAnalyticRecord, new()
    {
        public override T Create() => new T();

        public override bool Return(T obj)
        {
            if (obj is IAnalyticRecord record)
            {
                record.Reset();
            }
            return true;
        }
    }