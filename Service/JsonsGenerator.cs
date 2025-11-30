using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator.Service;

public class JsonsGenerator
{

    private static DefaultObjectPool<T> CreatePool<T>() where T : class, IAnalyticRecord, new()
    { return new DefaultObjectPool<T>(new DefaultPooledObjectPolicy<T>()); }


    public static async Task Generate<T>(List<string> pages, int number, string fileName)
        where T : class, IAnalyticRecord, new()
    {
        var pool = CreatePool<T>();


        await using var stream = new FileStream(
    fileName,
    FileMode.Create,
    FileAccess.Write,
    FileShare.None,
    bufferSize: 64 * 1024,
    useAsync: true
);
        await using var writer = new Utf8JsonWriter(stream, JsonSetting.JsonWriterOptions);
        await using var file = new StreamWriter(stream);
        await file.WriteAsync("[");

        var first = true;
        var date = new DateTime(1940, 1, 1);
        var count = 0;
        var batch = new StringBuilder(4096);
        var batchSize = 10000;
        var batchCount = 0;

        for (int i = 0; i < number; i++, date = date.AddHours(1))
        {
            for (int pageIdx = 0; pageIdx < pages.Count; pageIdx++)
            {
                count++;
                var record = pool.Get();
                record.CreateRandom(date, pages[pageIdx]);
                if (!first)
                    batch.Append(",\n");

                string json = JsonSerializer.Serialize(record, JsonSetting.Options);
                batch.Append(json);
                batchCount++;
                if (batchCount >= batchSize)
                {
                    await file.WriteAsync(batch.ToString());
                    batchCount = 0;
                    batch.Clear();
                }

                first = false;
                pool.Return(record);
            }
        }
        if (batchCount > 0)
            await file.WriteAsync(batch.ToString());
        await file.WriteAsync("]");
        System.Console.WriteLine($"Total {typeof(T).Name} objects created {count}");
    }
}
