using System.Text.Json;
using Metriflow.HyperJSONGenerator.CustomPolicies;
using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator.Service;

public class JsonsGenerator
{
    public static async Task Generate<T>(List<string> pages, int number, string fileName)
        where T : class, IAnalyticRecord, new()
    {
        await using var file = File.CreateText(fileName);
        await file.WriteAsync("[");

        var first = true;
        var date = new DateTime(1995, 1, 1);
        var count = 0;
        var pool = RecordPool<T>.Pool;
        for (int i = 0; i < number; i++, date = date.AddHours(1))
        {
            for (int pageIdx = 0; pageIdx < pages.Count; pageIdx++)
            {
                count++;

                // var record = (T)T.CreateRandom(date, pages[pageIdx]);
                var record = pool.Get();

                if (!first)
                    await file.WriteAsync(",\n");

                string json = JsonSerializer.Serialize(record);
                await file.WriteAsync(json);

                first = false;
                pool.Return(record);
            }
        }

        await file.WriteAsync("]");
        System.Console.WriteLine($"{typeof(T).Name} objects created {count}");
    }
}