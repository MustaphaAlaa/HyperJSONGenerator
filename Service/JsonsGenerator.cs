using System.Text.Json;
namespace Metriflow.HyperJSONGenerator.Service;

public class JsonsGenerator
{
    public static async Task Generate<T>(int number, string fileName)
        where T : struct, IAnalyticRecord
    {
        var pages = new byte[(int)enPages.count - 1];

        for (byte i = 1; i < (int)enPages.count; i++)
            pages[i - 1] = i;



        await using var stream = new FileStream(
                fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 1024 * 1024,
                useAsync: true
            );
        await using var jsonWriter = new Utf8JsonWriter(stream, JsonSetting.JsonWriterOptions);
        jsonWriter.WriteStartArray();
        jsonWriter.Flush();

        var date = new DateTime(1940, 1, 1);
        var time = date.Ticks;
        var hour = TimeSpan.TicksPerHour;
        var total = 0;


        var obj = new T();

        var random = Random.Shared;
        var threshold = 1 * 1024 * 1024;

        for (int i = 0; i < number; i++, time += hour)
        {
            foreach (var page in pages)
            {
                total++;
                obj.SetRandoms(time, page, random);

                WriteToJson(jsonWriter, obj);

                if (jsonWriter.BytesPending >= threshold)
                    await jsonWriter.FlushAsync();

            }
        }

        if (jsonWriter.BytesPending > 0)
            await jsonWriter.FlushAsync();

        await jsonWriter.FlushAsync();
        jsonWriter.WriteEndArray();
        Console.WriteLine($"Total {typeof(T).Name} objects created {total:N0}");
    }

    private static void WriteToJson<T>(Utf8JsonWriter writer, T obj)
     where T : struct, IAnalyticRecord
    {
        writer.WriteStartObject();
        writer.WriteNumber("Date", obj.Date);
        writer.WriteNumber("Page", obj.Page);

        obj.WriteToJson(writer);

        writer.WriteEndObject();
    }
}