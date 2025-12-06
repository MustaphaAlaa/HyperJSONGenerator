using System.Text.Json;

namespace Metriflow.HyperJSONGenerator.Service;

public static class JsonSetting
{
    private static JsonSerializerOptions JsonOptions = null;
    private static JsonWriterOptions _jsonWriterOptions = default;
    public static JsonSerializerOptions Options
    {
        get
        {
            if (JsonOptions == null)
            {
                JsonOptions = new()
                {
                    WriteIndented = false,

                };
            }
            return JsonOptions;
        }
    }
    public static JsonWriterOptions JsonWriterOptions
    {
        get
        {
            _jsonWriterOptions.Indented = false;
            _jsonWriterOptions.SkipValidation = true;
            return _jsonWriterOptions;
        }
    }

}